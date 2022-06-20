
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Clases
{
    public class MenuPaused
    {
        cButtons cResumen;
        cButtons cMenu;
        Texture2D Fondo;

        public MenuPaused()
        {
        }

        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content,Microsoft.Xna.Framework.GraphicsDeviceManager _graphics)
        {
            



        }



        public void PressButtonPaused()
        {
            if (Globals.PausekeyPress == false)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    Globals.pause = !Globals.pause;

                    Globals.PausekeyPress = true;
                }

            }
            if (Keyboard.GetState().IsKeyUp(Keys.Escape))
            {
                Globals.PausekeyPress = false;
            }

        }
    }
}