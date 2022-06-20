using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clases;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Clases
{
    public class MainMenuGAme
    {
        cButtons cPlay;
        cButtons cExit;

        public MainMenuGAme()
        {

        }
        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content, GraphicsDeviceManager _graphics)
        {


            this.cPlay = new cButtons(content.Load<Texture2D>("assets/play"), _graphics.GraphicsDevice);
            this.cExit = new cButtons(content.Load<Texture2D>("assets/exit"), _graphics.GraphicsDevice);

            this.cPlay.setPosition(new Vector2(350, 300));

            this.cExit.setPosition(new Vector2(600, 300));
        }

        public cButtons GetButtonPlay() { return cPlay; }

        public cButtons GetButtonExit() { return cExit; }



    }
}