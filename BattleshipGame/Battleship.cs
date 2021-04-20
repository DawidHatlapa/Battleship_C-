using static System.Console;
using System;
using BattleshipGame.Game;
using BattleshipGame.GetShow;

namespace BattleshipGame
{
    public class Battleship
    {
        private Display Display = new Display();
        private int HighScore;
        

        public void Start()
        {   
            RunMainMenu();
            
        }

        private void RunMainMenu()
        {
            var prompt = Display.MainMenu();
            string[] options = {"Play", "About", "Exit"};
            GameMenu mainGameMenu = new GameMenu(prompt, options);
            int selectedIndex = mainGameMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    RunGame();
                    break;
                case 1:
                    DisplayAboutInfo();
                    break;
                case 2:
                    ExitGame();
                    break;
            }

        }

        
            private void ExitGame()
            {
                Display.Exit();
                ReadKey(true);
                Environment.Exit(0);
            }

            private void DisplayAboutInfo()
            {
                Display.InfoAbout();
                ReadKey(true);
                RunMainMenu();

            }

            private void RunGame()

            {
                Game.Game newRound = new Game.Game();
                newRound.Round();
                ReadKey(true);
                ExitGame();
            }
        
    }
}



