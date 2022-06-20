using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clases
{
    public class Globals
    {


        public static bool pause = true;
        public static bool PausekeyPress = false;

        public enum GameStates
        {
            MainMenu,
            InGame,
            Options,
            Exit
        }
        public static GameStates CurrentGameState = GameStates.MainMenu;


    };

}
