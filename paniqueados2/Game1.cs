using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Clases;


namespace paniqueados2
{

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _textura;
        static int LimitX = 1000;
        static int LimitY = 700;

        // Cambiar los tamaños aquí para ver reflejados
        static int tam = 10;
        static Tablero tablero = new Tablero(tam, LimitX, LimitY, 500, 1);
        List<Jugador> cursoresJugadores = tablero.getJugadores();
        MenuPaused menuPaused = new MenuPaused();

        Vector2 posicionPlayer = new Vector2(0, 0);

        float time;
        int contador = 0;

        public List<Vector2> pixelScreen = new List<Vector2>();
        
        ///////////////////
        //Rastroo

        SpriteFont font;
        Texture2D pixel;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            pixelScreen.Add(posicionPlayer);
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[1] { Color.White });
            _graphics.PreferredBackBufferWidth = LimitX;
            _graphics.PreferredBackBufferHeight = LimitY;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();
            base.Initialize();
        }
        MainMenuGAme mainmenu = new MainMenuGAme();

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("assets/File");
            _textura = Content.Load<Texture2D>("assets/puntito");

            mainmenu.LoadContent(this.Content, _graphics);
        }

        protected override void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();
            menuPaused.PressButtonPaused();

            switch (Globals.CurrentGameState)
            {
                case Globals.GameStates.MainMenu:
                    if (mainmenu.GetButtonPlay().isClicked == true)
                    {
                        Globals.CurrentGameState = Globals.GameStates.InGame;
                    }
                    else if (mainmenu.GetButtonExit().isClicked == true)
                    {
                        this.Exit();
                    }
                    mainmenu.GetButtonExit().Update(mouse);
                    mainmenu.GetButtonPlay().Update(mouse);
                    break;

                case Globals.GameStates.Exit:
                    this.Exit();
                    break;

                case Globals.GameStates.InGame:
                    contador++;
                    time = contador / 1000;
                    for (int i = 0; i < cursoresJugadores.Count; i++) { cursoresJugadores[i].update(tablero, LimitX, LimitY); }
                    break;

            }
            base.Update(gameTime);



        }

        protected override void Draw(GameTime gameTime)
        {

            List<Trazo> trazos = new List<Trazo>();
            for (int i = 0; i < cursoresJugadores.Count; i++) { trazos.Add(cursoresJugadores[i].getTrazo()); }


            GraphicsDevice.Clear(Color.CornflowerBlue);
            //EMPEZAR A DIBUJAR
            _spriteBatch.Begin();
            /// MENU
            switch (Globals.CurrentGameState)
            {
                case Globals.GameStates.MainMenu:
                    mainmenu.GetButtonPlay().Draw(_spriteBatch);
                    mainmenu.GetButtonExit().Draw(_spriteBatch);
                    break;
                ///IN GAME
                case Globals.GameStates.InGame:
                    /// TRAZADO
                    for (int i = 0; i < trazos.Count; i++) { trazos[i].Draw(_spriteBatch, tablero, pixel, cursoresJugadores[i]); }
                    //   JUGADOR
                    for (int i = 0; i < cursoresJugadores.Count; i++) { _spriteBatch.Draw(_textura, new Rectangle(cursoresJugadores[i].getX(), cursoresJugadores[i].getY(), cursoresJugadores[i].getTam(), cursoresJugadores[i].getTam()), Color.White); }

                    break;
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
