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
        private Rectangle _rectangule;
        int LimitX = 1000;
        int LimitY = 700;

        Jugador cursorJugador = new Jugador(new Trazo(), 0, 0);
        Vector2 posicionPlayer  = new Vector2(0, 0);

        float time;
        int contador = 0;

        public List<Vector2> pixelScreen = new List<Vector2>();

        //Rastroo
        private Texture2D _texturaRastro;
        private Rectangle _rectanguleRastro;
        SpriteFont font;

        public int[] generarCoords(int LimitX, int LimitY) {
            int randX = 1;
            int randY = 1;
            while (randX % 5 != 0 || randY % 5 != 0) {
                Random r = new Random();
                randX = r.Next(0,LimitX-10);
                randY = r.Next(0,LimitY-10);
            }

            int[] coords = new int[] { randX, randY };
             
            return coords;
        }

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
                cursorJugador.setX(LimitX-10);
            }
            if (cursorJugador.getY() < 0)
            {
                res = true;
                cursorJugador.setY(0);
            }
            else if (cursorJugador.getY() >= LimitY)
            {
                res = true;
                cursorJugador.setY(LimitY-10);
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
            int total = LimitX * LimitY;
            int[] coords = generarCoords(LimitX, LimitY);

            cursorJugador.setX(coords[0]);
            cursorJugador.setX(coords[1]);
            cursorJugador.getTrazo().setXInicial(cursorJugador.getX());
            cursorJugador.getTrazo().setYInicial(cursorJugador.getY());

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("File");

            _textura = Content.Load<Texture2D>("puntito");
            _texturaRastro = Content.Load<Texture2D>("rastro");
            
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            posicionPlayer  = new Vector2(cursorJugador.getX(), cursorJugador.getY());
            contador++;
            time = contador / 1000;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            


            _rectangule = new Rectangle((int)posicionPlayer.X, (int)posicionPlayer.Y, 10, 10);
            _rectanguleRastro = new Rectangle((int)posicionPlayer.X, (int)posicionPlayer.Y, 10, 10);
       
            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D) )
            {
                cursorJugador.setX( cursorJugador.getX() + 10);
                if (!LimitMap()) { cursorJugador.getTrazo().nuevaDireccion("D"); }

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A))
            {
                cursorJugador.setX( cursorJugador.getX() - 10);
                if (!LimitMap()) { cursorJugador.getTrazo().nuevaDireccion("A"); }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W))
            {
                cursorJugador.setY( cursorJugador.getY() - 10);
                if (!LimitMap()) { cursorJugador.getTrazo().nuevaDireccion("W"); }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S) )
            {
                cursorJugador.setY( cursorJugador.getY() + 10);
                if (!LimitMap()) { cursorJugador.getTrazo().nuevaDireccion("S"); }
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Space)) {
                char ultimaDir = cursorJugador.getTrazo().backtrack();
                switch(ultimaDir) {
                    case 'W' :
                        cursorJugador.setY( cursorJugador.getY() + 10);
                        break;
                    case 'A' :
                        cursorJugador.setX( cursorJugador.getX() + 10);
                        break;
                    case 'S' :
                        cursorJugador.setY( cursorJugador.getY() - 10);
                        break;
                    case 'D' :
                        cursorJugador.setX( cursorJugador.getX() - 10);
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
            _spriteBatch.DrawString(font, "X:" + playerX + " Y:" + playerY + " Path:" + cursorJugador.getTrazo().getPath(), position2, Color.White, 0, textMiddlePoint, 1.0f, SpriteEffects.None, 0.5f);

            ///PUNTO

            /// TRAZADO
            Trazo trazo = cursorJugador.getTrazo();

            string camino = trazo.getPath();
            int x = trazo.getX();
            int y = trazo.getY();

            for(int i = 0; i < camino.Length; i++) {
                switch (camino[i]) {
                    case 'X':
                    break;

                    case 'W':
                        y -= 10;
                    break;

                    case 'A':
                        x -= 10;
                    break;

                    case 'S':
                        y += 10;
                    break;

                    case 'D':
                        x += 10;
                    break;
                }
                if(x >= LimitX) x = LimitX - 10;
                if(y >= LimitY) y = LimitY - 10;
                if(x <= 0) x = 0;
                if(y <= 0) y = 0;
                _spriteBatch.Draw(pixel, new Rectangle(x, y, 10, 10), Color.Red);
            }

            _spriteBatch.Draw(_textura, _rectangule, Color.White);
            _spriteBatch.End();


            base.Draw(gameTime);
        }

    }

}
