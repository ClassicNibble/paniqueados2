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


namespace paniqueados2 {

    public class Game1 : Game
    {

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _textura;
        static int LimitX = 1000;
        static int LimitY = 700;

        // Cambiar los tamaños aquí para ver reflejados
        static int tam = 10;
        static Tablero tablero = new Tablero(tam, LimitX, LimitY, 500);
        Jugador cursorJugador = tablero.getJugadores()[0];


        Vector2 posicionPlayer  = new Vector2(0, 0);

        float time;
        int contador = 0;

        public List<Vector2> pixelScreen = new List<Vector2>();

        //Rastroo
        SpriteFont font;

        

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
                cursorJugador.setX(LimitX-cursorJugador.getTam());
            }
            if (cursorJugador.getY() < 0)
            {
                res = true;
                cursorJugador.setY(0);
            }
            else if (cursorJugador.getY() >= LimitY)
            {
                res = true;
                cursorJugador.setY(LimitY-cursorJugador.getTam());
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
            
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            posicionPlayer  = new Vector2(cursorJugador.getX(), cursorJugador.getY());
            contador++;
            time = contador / 1000;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            


            
            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D) )
            {
                cursorJugador.setX( cursorJugador.getX() + cursorJugador.getTrazo().getTam());
                if (!LimitMap()) { cursorJugador.getTrazo().nuevaDireccion("D"); }

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A))
            {
                cursorJugador.setX( cursorJugador.getX() - cursorJugador.getTrazo().getTam());
                if (!LimitMap()) { cursorJugador.getTrazo().nuevaDireccion("A"); }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W))
            {
                cursorJugador.setY( cursorJugador.getY() - cursorJugador.getTrazo().getTam());
                if (!LimitMap()) { cursorJugador.getTrazo().nuevaDireccion("W"); }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S) )
            {
                cursorJugador.setY( cursorJugador.getY() + cursorJugador.getTrazo().getTam());
                if (!LimitMap()) { cursorJugador.getTrazo().nuevaDireccion("S"); }
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Space)) {
                char ultimaDir = cursorJugador.getTrazo().backtrack();
                switch(ultimaDir) {
                    case 'W' :
                        cursorJugador.setY( cursorJugador.getY() + cursorJugador.getTrazo().getTam());
                        break;
                    case 'A' :
                        cursorJugador.setX( cursorJugador.getX() + cursorJugador.getTrazo().getTam());
                        break;
                    case 'S' :
                        cursorJugador.setY( cursorJugador.getY() - cursorJugador.getTrazo().getTam());
                        break;
                    case 'D' :
                        cursorJugador.setX( cursorJugador.getX() - cursorJugador.getTrazo().getTam());
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

            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            
            
            ///Texto
            playerX = new StringBuilder().Append(posicionPlayer.X).ToString();
            playerY = new StringBuilder().Append(posicionPlayer.Y).ToString();
            _spriteBatch.DrawString(font, "ESTADO: " + tablero.getEstado() +"X:" + playerX + " Y:" + playerY + " Path:" + cursorJugador.getTrazo().getPath(), position2, Color.White, 0, textMiddlePoint, 1.0f, SpriteEffects.None, 0.5f);

            ///PUNTO

            /// TRAZADO
            Trazo trazo = cursorJugador.getTrazo();
            
            tablero.trazoATableros(trazo);
            
            List<char[]> matriz = tablero.getMatriz();

            for (int i = 0; i < matriz.Count; i++) {
                for(int j = 0; j < matriz[i].Length; j++) {
                    if(matriz[i][j] =='A') {
                        _spriteBatch.Draw(pixel, new Rectangle(j * tablero.getTam(), i * tablero.getTam(), cursorJugador.getTrazo().getTam(), cursorJugador.getTrazo().getTam()), Color.Red);
                    }
                    if(matriz[i][j] =='C') {
                        _spriteBatch.Draw(pixel, new Rectangle(j * tablero.getTam(), i * tablero.getTam(), cursorJugador.getTrazo().getTam(), cursorJugador.getTrazo().getTam()), Color.Blue);
                    }
                    if(matriz[i][j] =='1') {
                        _spriteBatch.Draw(pixel, new Rectangle(j * tablero.getTam(), i * tablero.getTam(), cursorJugador.getTrazo().getTam(), cursorJugador.getTrazo().getTam()), Color.Green);
                    }
                }
            }

            _spriteBatch.Draw(_textura, new Rectangle(cursorJugador.getX(), cursorJugador.getY(), cursorJugador.getTam(), cursorJugador.getTam()), Color.White);
            _spriteBatch.End();


            base.Draw(gameTime);
        }

    }

}
