using System;
using static System.Console;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using BattleshipGame.BoardFolder;
using BattleshipGame.GetShow;
using BattleshipGame.Player;

namespace BattleshipGame.Game
{
    public class Game
    {
        private Display Display = new Display();
        private Input Input = new Input();
        private List<Player.Player> ListOfPlayers = new List<Player.Player>();
        private int BoardSize;

        public void Round()
        {
            BoardSize = Input.BoardSize();

            CreatePlayers(2);

            var turnCounter = 0;

            Player.Player player1 = ListOfPlayers[0];
            Player.Player player2 = ListOfPlayers[1];
            Player.Player currentPLayer = player1;
            Player.Player enemyPlayer = player2;

            //TODO intro
            Display.DisplayIntro();

           
            while (player1.CheckIfIsAlive() & player2.CheckIfIsAlive())
            {
                
                currentPLayer = turnCounter % 2 == 0 ? player1 : player2;
                enemyPlayer = turnCounter % 2 == 1 ? player1 : player2;
                
                Display.Message($"{player1.NameOfPlayer} board below");
                Display.ShowBoard(player1.PlayerBoard);
                Display.Message($"\n{player2.NameOfPlayer} board below");
                Display.ShowBoard(player2.PlayerBoard);
                WriteLine();
                Display.Message($"{currentPLayer.NameOfPlayer} turn!");
                var changePlayer = currentPLayer.MakeShot(enemyPlayer.ListOfShips);
                turnCounter = changePlayer ? turnCounter + 1 : turnCounter; 
                Clear();
            }
            Display.Win(turnCounter, currentPLayer.NameOfPlayer);
            Display.AskToPlayAgain();
            Display.DisplayOutro();
        }
        
        
        private void ShipPlacement(Player.Player player)
        {
            BoardFactory factory = new BoardFactory(player);
            WriteLine();
            var prompt = $"{player.NameOfPlayer} choose method to place your ships:";
            string[] option = {"Random", "Manual"};

            var selectedIndex = Input.CreateMenu(prompt, option);
            switch (selectedIndex)
            {
                case 0:
                    factory.RandomPlacement();
                    break;
                case 1:
                    factory.ManualPlacement();
                    break;
            }
        }

        private void CreatePlayers(int numberOfPlayers)
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                WriteLine();
                // ReSharper disable once HeapView.BoxingAllocation
                Display.Message($"Please, provide name for player {i + 1}");
                var name = Input.GetNickname();

                var prompt = $"Choose type of player {name}";
                string[] option = {"Human", "Computer easy", "Computer normal", "Computer hard"};
                var index = Input.CreateMenu(prompt, option);
                
                switch (index)
                {
                    case 0:
                        Human human = new Human();
                        human.CreatePlayer(name, BoardSize);
                        ShipPlacement(human);
                        ListOfPlayers.Add(human);
                        break;
                    case 1:
                        ComputerEasy computerEasy = new ComputerEasy();
                        CreateAI(computerEasy, name);
                        break;
                    case 2:
                        ComputerNormal computerNormal = new ComputerNormal();
                        CreateAI(computerNormal, name);
                        break;
                    case 3:
                        ComputerHard computerHard = new ComputerHard();
                        CreateAI(computerHard, name);
                        break;
                }
            }
        }

        private void CreateAI(Player.Player computer, string name)
        {
            computer.CreatePlayer(name, BoardSize);
            new BoardFactory(computer).RandomPlacement();
            ListOfPlayers.Add(computer);
        }
    }
}