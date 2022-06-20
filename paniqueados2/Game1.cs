using System.Reflection.Metadata.Ecma335;
using System.Collections.Generic;
using System.Text;
using System;
using System.Text.RegularExpressions;
using System.Diagnostics;
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
        static Tablero tablero = new Tablero(tam, LimitX, LimitY, 100);
        Jugador cursorJugador = tablero.getJugadores()[0];


        Vector2 posicionPlayer = new Vector2(0, 0);

        float time;
        int contador = 0;

        public List<Vector2> pixelScreen = new List<Vector2>();

        //Rastroo

        SpriteFont font;
        enum GameStates
        {
            MainMenu,
            InGame,
            Options,
            Exit
        }
        GameStates CurrentGameState = GameStates.MainMenu;
        cButtons cPlay;
        cButtons cExit;


        public bool LimitMap()
        {
            bool res = false;

            if (cursorJugador.getX() < 0)
            {
                res = true;
                cursorJugador.setX(0);
            }
            else if (cursorJugador.getX() >= LimitX)
            {
                res = true;
                cursorJugador.setX(LimitX - cursorJugador.getTam());
            }
            if (cursorJugador.getY() < 0)
            {
                res = true;
                cursorJugador.setY(0);
            }
            else if (cursorJugador.getY() >= LimitY)
            {
                res = true;
                cursorJugador.setY(LimitY - cursorJugador.getTam());
            }

            return res;
        }



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
            pixel.SetData<Color>(new Color[1] { Color.White });        // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = LimitX;
            _graphics.PreferredBackBufferHeight = LimitY;
            _graphics.IsFullScreen = false;

            _graphics.ApplyChanges();
            base.Initialize();

        }

        protected override void LoadContent()
        {

            cursorJugador.getTrazo().setXInicial(cursorJugador.getX());
            cursorJugador.getTrazo().setYInicial(cursorJugador.getY());

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("File");
            _textura = Content.Load<Texture2D>("puntito");


            cPlay = new cButtons(Content.Load<Texture2D>("assets/play"), _graphics.GraphicsDevice);
            cExit = new cButtons(Content.Load<Texture2D>("assets/exit"), _graphics.GraphicsDevice);

            cPlay.setPosition(new Vector2(350, 300));

            cExit.setPosition(new Vector2(600, 300));

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {


            MouseState mouse = Mouse.GetState();

            switch (CurrentGameState)
            {
                case GameStates.MainMenu:
                    if (cPlay.isClicked == true)
                    {
                        CurrentGameState = GameStates.InGame;
                    }
                    else if (cExit.isClicked == true)
                    {
                        this.Exit();
                    }
                    cExit.Update(mouse);
                    cPlay.Update(mouse);
                    break;

                case GameStates.Exit:

                    break;

                case GameStates.InGame:
                    break;
            }




            posicionPlayer = new Vector2(cursorJugador.getX(), cursorJugador.getY());
            contador++;
            time = contador / 1000;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();





            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                cursorJugador.setX(cursorJugador.getX() + cursorJugador.getTrazo().getTam());
                if (!LimitMap()) { cursorJugador.getTrazo().nuevaDireccion("D"); }

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A))
            {
                cursorJugador.setX(cursorJugador.getX() - cursorJugador.getTrazo().getTam());
                if (!LimitMap()) { cursorJugador.getTrazo().nuevaDireccion("A"); }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W))
            {
                cursorJugador.setY(cursorJugador.getY() - cursorJugador.getTrazo().getTam());
                if (!LimitMap()) { cursorJugador.getTrazo().nuevaDireccion("W"); }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S))
            {
                cursorJugador.setY(cursorJugador.getY() + cursorJugador.getTrazo().getTam());
                if (!LimitMap()) { cursorJugador.getTrazo().nuevaDireccion("S"); }
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                char ultimaDir = cursorJugador.getTrazo().backtrack();

                switch (ultimaDir)
                {
                    case 'W':
                        cursorJugador.setY(cursorJugador.getY() + cursorJugador.getTrazo().getTam());
                        break;
                    case 'A':
                        cursorJugador.setX(cursorJugador.getX() + cursorJugador.getTrazo().getTam());
                        break;
                    case 'S':
                        cursorJugador.setY(cursorJugador.getY() - cursorJugador.getTrazo().getTam());
                        break;
                    case 'D':
                        cursorJugador.setX(cursorJugador.getX() - cursorJugador.getTrazo().getTam());
                        break;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {




            string playerX = "";
            string playerY = "";
            Vector2 position2 = new Vector2(10, 10);
            Vector2 textMiddlePoint = font.MeasureString("text") / 2;
            Trazo trazo = cursorJugador.getTrazo();

            GraphicsDevice.Clear(Color.CornflowerBlue);
            //EMPEZAR A DIBUJAR
            _spriteBatch.Begin();

            /// MENU
            switch (CurrentGameState)
            {
                case GameStates.MainMenu:
                    cPlay.Draw(_spriteBatch);
                    cExit.Draw(_spriteBatch);
                    break;


                ///IN GAME

                case GameStates.InGame:
                    playerX = new StringBuilder().Append(posicionPlayer.X).ToString();
                    playerY = new StringBuilder().Append(posicionPlayer.Y).ToString();
                    _spriteBatch.DrawString(font, "X:" + playerX + " Y:" + playerY + " Path:" + cursorJugador.getTrazo().getPath(), position2, Color.White, 0, textMiddlePoint, 1.0f, SpriteEffects.None, 0.5f);
                    /// TRAZADO
                    trazo.Draw(_spriteBatch, tablero, pixel, cursorJugador);
                    //   JUGADOR
                    _spriteBatch.Draw(_textura, new Rectangle(cursorJugador.getX(), cursorJugador.getY(), cursorJugador.getTam(), cursorJugador.getTam()), Color.White);
                    _spriteBatch.End();
                    base.Draw(gameTime);
                    break;
            }
            /// TEXTO

        }

    }

}
