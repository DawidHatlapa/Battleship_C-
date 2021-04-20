using System;
using System.Text;
using static System.Console;

namespace BattleshipGame.GetShow
{
    public class Input
    {
        private Display Display = new Display();
       

        public (int, int) GetCoordinates(int boardSize)
        {
            int x = -1;
            int y = -1;
            bool ready = false;

            do
            {
                Display.Message("Enter coordinates (e.q. A1)");
                string userInput = ReadLine()?.ToUpper();
                if (userInput != null & userInput != "")
                {
                    char column = userInput[0];

                    if (column < 65 ^ column > 90)
                    {
                        WriteLine();
                        Display.Alert("First position should be letter from A to Z!");
                        WriteLine();
                    }
                    else
                    {
                        if (int.TryParse(userInput.Substring(1), out y))
                        {
                            x = column - 65;
                            if (x > boardSize - 1 || y > boardSize || y < 0)
                            {
                                WriteLine();
                                Display.Alert("Coordinates out of range!");
                                WriteLine();
                            }
                            else
                            {
                                ready = true;
                            }
                        }
                        else
                        {
                            WriteLine();
                            Display.Alert("Second position should be number!");
                            WriteLine();
                        }
                    }
                }
            } while (ready != true);

            return (y - 1, x);
        }

        public string GetNickname()
        {
            bool ready = false;
            string nickname;
                
            do
            {
                nickname = ReadLine();
                
                if (nickname != null && nickname.Length > 25)
                {
                    WriteLine();
                    Display.Alert("Your nickname length should be less than 25!");
                    
                }
                else
                {
                    ready = true;
                }
            } while (ready != true);

            return nickname;
        }
        
        public int BoardSize()
        {
            int size;
            while (true)
            {
                WriteLine();
                Display.Message("Please, provide size of board between 10 - 25");
               
                string userInput = ReadLine();
                if (int.TryParse(userInput, out size))
                {
                    if (size < 10 ^ size > 25)
                    {
                        WriteLine();
                        Display.Alert("Size should be between 10 and 25!");
                       
                    }
                    else
                    {
                        return size;
                    }
                }
                else
                {
                    WriteLine();
                    Display.Alert("Enter only numbers!");
                }
                System.Threading.Thread.Sleep(20);
            }
        }

        public ((int x, int y), ConsoleKey) GetShipPosition((int x, int y) shipCore)
        {
            WriteLine();
            Display.Message(@"Press space bar to change ship position.
Enter - to place ship.
Arrows - to move ship. ");
            WriteLine();
            ConsoleKeyInfo keyInfo = ReadKey(true);
            ConsoleKey keyPressed = keyInfo.Key;

            if (keyPressed == ConsoleKey.UpArrow)
            {
                --shipCore.x;
            }

            if (keyPressed == ConsoleKey.DownArrow)
            {
                ++shipCore.x;
            }

            if (keyPressed == ConsoleKey.LeftArrow)
            {
                --shipCore.y;
            }

            if (keyPressed == ConsoleKey.RightArrow)
            {
                ++shipCore.y;
            }

            return (shipCore, keyPressed);
        }

        public int CreateMenu(string prompt, string[] option)
        {
            GameMenu optionMenu = new GameMenu(prompt, option);
            int selectedIndex = optionMenu.Run();
            return selectedIndex;
        }
    }
}