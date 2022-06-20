
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Clases
{
    public class MenuPaused
    {
        cButtons cResumen;
        cButtons cMenu;
        Color colour = new Color(255, 255, 255, 255);

        Texture2D Fondo;
        Rectangle rectangle;

        public MenuPaused()
        {
        }

        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content, Microsoft.Xna.Framework.GraphicsDeviceManager _graphics)
        {

            this.Fondo = content.Load<Texture2D>("assets/BackgroundPaused");
            this.cResumen = new cButtons(content.Load<Texture2D>("assets/play"), _graphics.GraphicsDevice);
            this.cMenu = new cButtons(content.Load<Texture2D>("assets/exit"), _graphics.GraphicsDevice);

            this.cResumen.setPosition(new Vector2(500, 200));

            this.cMenu.setPosition(new Vector2(500, 400));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            rectangle = new Rectangle(200, 30, 600, 600);
            spriteBatch.Draw(Fondo, rectangle, colour);


        }
        public void PressButtonPaused(MouseState mouse)
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
            if (Globals.pause == true)
            {
                if (this.GetButtonResumen().isClicked == true)
                {
                    Globals.PausekeyPress = false;
                    Globals.pause = !Globals.pause;
                    this.GetButtonResumen().isClicked = false;

                }
                else if (this.GetButtonMenu().isClicked == true)
                {
                    Globals.CurrentGameState = Globals.GameStates.MainMenu;


                }

                this.GetButtonResumen().Update(mouse);
                this.GetButtonMenu().Update(mouse);
            }

        }
        public cButtons GetButtonResumen() { return cResumen; }

        public cButtons GetButtonMenu() { return cMenu; }
    }
}