using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Clases;


namespace paniqueados2
{

    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _textura;
        static int LimitX = 1000;
        static int LimitY = 700;

        // Cambiar los tamaños aquí para ver reflejados
        static int tam = 10;
        static Tablero tablero = new Tablero(tam, LimitX, LimitY, 500, 1);
        List<Jugador> cursoresJugadores = tablero.getJugadores();

        Vector2 posicionPlayer = new Vector2(0, 0);

        float time;
        int contador = 0;

        public List<Vector2> pixelScreen = new List<Vector2>();
        enum GameStates {
            MainMenu,
            InGame,
            Options,
            Exit
        }

        GameStates CurrentGameState = GameStates.MainMenu;

        cButtons cPlay;
        cButtons cExit;
        ///////////////////
        //Rastroo

        SpriteFont font;
        Texture2D pixel;

        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            pixelScreen.Add(posicionPlayer);
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[1] { Color.White });
            _graphics.PreferredBackBufferWidth = LimitX;
            _graphics.PreferredBackBufferHeight = LimitY;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("assets/File");
            _textura = Content.Load<Texture2D>("assets/puntito");

            cPlay = new cButtons(Content.Load<Texture2D>("assets/play"), _graphics.GraphicsDevice);
            cExit = new cButtons(Content.Load<Texture2D>("assets/exit"), _graphics.GraphicsDevice);
            cPlay.setPosition(new Vector2(350, 300));
            cExit.setPosition(new Vector2(600, 300));
        }

        protected override void Update(GameTime gameTime) {
            MouseState mouse = Mouse.GetState();
            switch (CurrentGameState) {
                case GameStates.MainMenu:
                    if (cPlay.isClicked == true) CurrentGameState = GameStates.InGame;
                    else if (cExit.isClicked == true) this.Exit();
                    cExit.Update(mouse);
                    cPlay.Update(mouse);
                    break;

                case GameStates.Exit:
                    this.Exit();
                    break;

                case GameStates.InGame:
                    contador++;
                    time = contador / 1000;
                    for(int i = 0; i < cursoresJugadores.Count; i++) { cursoresJugadores[i].update(tablero, LimitX, LimitY); }
                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)  {
            
            List<Trazo> trazos = new List<Trazo>();
            for(int i = 0; i < cursoresJugadores.Count; i++) { trazos.Add(cursoresJugadores[i].getTrazo()); }
            

            GraphicsDevice.Clear(Color.CornflowerBlue);
            //EMPEZAR A DIBUJAR
            _spriteBatch.Begin();
            /// MENU
            switch (CurrentGameState) {
                case GameStates.MainMenu:
                    cPlay.Draw(_spriteBatch);
                    cExit.Draw(_spriteBatch);
                    break;
                ///IN GAME
                case GameStates.InGame:
                    /// TRAZADO
                    for(int i = 0; i < trazos.Count; i++) { trazos[i].Draw(_spriteBatch, tablero, pixel, cursoresJugadores[i]); }
                    //   JUGADOR
                    for(int i = 0; i < cursoresJugadores.Count; i++) {_spriteBatch.Draw(_textura, new Rectangle(cursoresJugadores[i].getX(), cursoresJugadores[i].getY(), cursoresJugadores[i].getTam(), cursoresJugadores[i].getTam()), Color.White); }
                    
                    break;
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
