using System;
using System.Text;
using BattleshipGame.BoardFolder;
using static System.Console;

namespace BattleshipGame.GetShow
{
    public class Display
    {
        private int messageTime = 1500;

        public string MainMenu()
        {
            WriteLine("Battleship is starting...");
            string prompt = @" 

▀█████████▄     ▄████████     ███         ███      ▄█          ▄████████    ▄████████    ▄█    █▄     ▄█     ▄███████▄ 
  ███    ███   ███    ███ ▀█████████▄ ▀█████████▄ ███         ███    ███   ███    ███   ███    ███   ███    ███    ███ 
  ███    ███   ███    ███    ▀███▀▀██    ▀███▀▀██ ███         ███    █▀    ███    █▀    ███    ███   ███▌   ███    ███ 
 ▄███▄▄▄██▀    ███    ███     ███   ▀     ███   ▀ ███        ▄███▄▄▄       ███         ▄███▄▄▄▄███▄▄ ███▌   ███    ███ 
▀▀███▀▀▀██▄  ▀███████████     ███         ███     ███       ▀▀███▀▀▀     ▀███████████ ▀▀███▀▀▀▀███▀  ███▌ ▀█████████▀  
  ███    ██▄   ███    ███     ███         ███     ███         ███    █▄           ███   ███    ███   ███    ███        
  ███    ███   ███    ███     ███         ███     ███▌    ▄   ███    ███    ▄█    ███   ███    ███   ███    ███        
▄█████████▀    ███    █▀     ▄████▀      ▄████▀   █████▄▄██   ██████████  ▄████████▀    ███    █▀    █▀    ▄████▀      
                                                  ▀                                                                    
   
                                                                           
";
            ResetColor();
            return prompt;
        }


        public void Exit()
        {
            WriteLine("\nPress any key to exit...");
        }


        public void InfoAbout()
        {
            Clear();
            WriteLine();
            WriteLine("======================");
            WriteLine("Battleship Game Rules");
            WriteLine("======================");

            WriteLine(@"                ___
                                       |___|
                                  ______|_|
                           _   __|_________|  _
            _        =====| | |            | | |==== _
      =====| |        .---------------------------. | |====
<-----------------'   .  .  .  .  .  .  .  .   '------------/
  \                                                        /
   \                                                      /
    \____________________________________________________/
");
            WriteLine(
                "You have 5 available ships : Carrier (occupies 5 spaces), Battleship (4), Cruiser (3), Submarine (3), and Destroyer (2)");
            WriteLine(
                @"Each ship must be placed horizontally or vertically across grid spaces—not diagonally—and the ships can't hang off the grid.
 Ships can touch each other, but they can't occupy the same grid space. You cannot change the position of the ships after the game begins.");
            WriteLine("\nPress any key to return to main menu");
        }

        public void ShowBoard(Square[,] seeBoard)
        {
            StringBuilder sb = new StringBuilder();
            var line = string.Empty;
            var alfa = string.Empty;
            for (int i = 0; i <= seeBoard.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= seeBoard.GetUpperBound(1); j++)
                {
                    if (i == 0)
                    {
                        if (i == 0 && j == 0)
                        {
                            alfa += $"   {(Alfa) j} ";
                        }
                        else
                        {
                            alfa += $"{(Alfa) j} ";
                        }

                        if (j == seeBoard.GetUpperBound(1))
                        {
                            alfa += '\n';
                        }
                    }

                    if (j == 0)
                    {
                        var len = (i + 1).ToString().Length;
                        if (len == 1)
                        {
                            line += $"{i + 1}  {seeBoard[i, j].Character} ";
                        }
                        else
                        {
                            line += $"{i + 1} {seeBoard[i, j].Character} ";
                        }
                    }
                    else
                    {
                        line += $"{seeBoard[i, j].Character} ";
                    }
                }

                sb.AppendLine(line);
                line = String.Empty;
            }

            sb.Insert(0, alfa);
            // Console.Clear();
            Write(sb);
        }


        public void Message(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{message}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Alert(string alert)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{alert}");
            Console.ForegroundColor = ConsoleColor.White;
            System.Threading.Thread.Sleep(messageTime);
        }


        public void Hit()
        {
            WriteLine(@"  
           _ ._  _ , _ ._
        (_ ' ( `  )_  .__)
      ( (  (    )   `)  ) _)
     (__ (_   (_ . _) _) ,__)
         `~~`\ ' . /`~~`
              ;   ;
              /   \
_____________/_ __ \_____________");

            // method lets the player to know ship was hit
            // turnCounter += 1;
            ForegroundColor = ConsoleColor.DarkMagenta;
            WriteLine("Boom.... Aaaaaaaaaaaaaaaaaa.........You got me!!!! ");
            // display a Score?
            // WriteLine($"Score: {turnCounter}"); 
            System.Threading.Thread.Sleep(messageTime);
        }


        public void Sunk()
        {
            WriteLine(@"
           ___
          /`  _\
          |  / 0|--.
     -   / \_|0`/ /.`'._/)
 - ~ -^_| /-_~ ^- ~_` - -~ _
 -  ~  -| |   - ~ -  ~  -
         \ \, ~   -   ~
         \_|
              
");
            // method lets the player to know ship was sunk
            // turnCounter += 1;
            ForegroundColor = ConsoleColor.DarkRed;
            WriteLine("Congratulation you sunk the ship!!!! ");
            WriteLine("I hope they can swim.....");
            // display a Score?
            // WriteLine($"Score: {turnCounter}"); 
            System.Threading.Thread.Sleep(messageTime);
        }


        public void Win(int turnCounter, string currentPlayer)
        {
            WriteLine(@"
                                   .''.       
       .''.      .        *''*    :_\/_:     . 
      :_\/_:   _\(/_  .:.*_\/_*   : /\ :  .'.:.'.
  .''.: /\ :   ./)\   ':'* /\ * :  '..'.  -=:o:=-
 :_\/_:'.:::.    ' *''*    * '.\'/.' _\(/_'.':'.'
 : /\ : :::::     *_\/_*     -= o =-  /)\    '  *
  '..'  ':::'     * /\ *     .'/.\'.   '
      *            *..*         :
         *
        *       

");
            // method lets the player to know who won
            turnCounter += 1;
            ForegroundColor = ConsoleColor.Green;
            WriteLine($"Congratulation {currentPlayer}, You won!\n");
            // display a Score?
            WriteLine($"You've won in: {turnCounter} turn!");
            System.Threading.Thread.Sleep(messageTime);
        }

        public void Miss()
        {
            // method lets player to know they lost
            ForegroundColor = ConsoleColor.Red;
            WriteLine("Miss..Better luck next time.. ");
            System.Threading.Thread.Sleep(messageTime);
        }

        public void Lose(int turnCounter)
        {
            // method lets player to know they lost
            ForegroundColor = ConsoleColor.Red;
            WriteLine("You lost.... ");
            System.Threading.Thread.Sleep(messageTime);;
        }

        public void AskToPlayAgain()
        {
            // method that ask teh player if they want to play again
            WriteLine("Would you like to play again? (yes/no)");
            string playResponse = ReadLine().Trim().ToLower();
            if (playResponse == "yes")
            {
                var game = new Game.Game();
                game.Round();
            }
            else
            {
                WriteLine("Had enough? '\n'-Ok. See you later");
            }
        }

        public void DisplayIntro()

        {
            Clear();
            ForegroundColor = ConsoleColor.Blue;
            WriteLine("\n========================");
            WriteLine("Welcome to Battleship ...");
            WriteLine("The Game designed,developed,tested by: ");
            WriteLine();
            WriteLine(">> Dawid Hatlapa <<");
            WriteLine("==========================");
            WriteLine("\nPress any key to continue");
            ResetColor();
            ReadKey();
        }

        public void DisplayOutro()
        {
            ForegroundColor = ConsoleColor.Red;
            WriteLine("\n¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬");
            WriteLine("Thank you for playing...");
            WriteLine("Hopefully see you again...");
            WriteLine("\n¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬");
            WriteLine("Press any key to continue");
            ResetColor();
            ReadKey();
        }
    }
}