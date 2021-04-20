using System;
using System.Collections.Generic;
using System.Diagnostics;
using BattleshipGame.Game;
using BattleshipGame.GetShow;
using static System.Console;

namespace BattleshipGame.BoardFolder
{
    public class BoardFactory
    {
        private Display Display = new Display();
        private Input Input = new Input();
        private int BoardSize;
        private List<Ship> ListOfShips;
        private Square[,] ManualPlacementBoard;
        private List<(int, int)> UsedSquare = new List<(int, int)>();

        public BoardFactory(Player.Player player)
        {
            BoardSize = player.PlayerBoard.GetUpperBound(0);
            ListOfShips = player.ListOfShips;
            ManualPlacementBoard = new Board(BoardSize + 1).GetBoard();
        }

        public void RandomPlacement()
        {
            var random = new Random();
            foreach (var ship in ListOfShips)
            {
                var shipLenght = ship.GetShipLength();
                var ready = false;
                do
                {
                    (int x, int y) shipCore;
                    shipCore.x = random.Next(0, BoardSize + 1);
                    shipCore.y = random.Next(0,BoardSize + 1);
                    bool isVertical = random.Next(0, 2) == 1;
                    var shipProposedPosition = GenerateShipPositions(shipCore, isVertical, shipLenght);
                    
                    ready = PlaceShip(shipCore, isVertical, shipLenght, shipProposedPosition, ship, false);
                } while (ready == false);
            }
        }

        public void ManualPlacement()
        {
            foreach (var ship in ListOfShips)
            {
                (int x, int y) shipCore = (0, 0);
                bool isVertical = true;
                bool ready = false;
                var shipLenght = ship.GetShipLength();

                do
                {
                    var shipProposedPosition = GenerateShipPositions(shipCore, isVertical, shipLenght);
                    GenerateView(shipProposedPosition, true);

                    ConsoleKey keyPressed;
                    (shipCore, keyPressed) = Input.GetShipPosition(shipCore);

                    if (keyPressed == ConsoleKey.Spacebar)
                    {
                        isVertical = isVertical != true;
                    }

                    if (keyPressed == ConsoleKey.Enter)
                    {
                        ready = PlaceShip(shipCore, isVertical, shipLenght, shipProposedPosition, ship, true);
                    }
                } while (ready == false);
            }
        }

        private bool TryToPlaceShip((int x, int y) shipCore, bool isVertical, int shipLenght,
            List<(int, int)> shipProposedPosition, bool manual)
        {
            if (CheckBoundaries(shipProposedPosition, manual) & CheckSpot(shipProposedPosition, manual))
            {
                return true;
            }

            return false;
        }

        private List<(int x, int y)> GenerateShipPositions((int x, int y) shipCore, bool isVertical, int shipLenght)
        {
            List<(int x, int y)> shipSquares = new List<(int x, int y)>();
            for (int i = 0; i < shipLenght; i++)
            {
                if (isVertical)
                {
                    shipSquares.Add(shipCore);
                    ++shipCore.x;
                }
                else
                {
                    shipSquares.Add(shipCore);
                    ++shipCore.y;
                }
            }

            return shipSquares;
        }

        private bool CheckBoundaries(List<(int x, int y)> shipProposedPosition, bool manual)
        {
            foreach (var square in shipProposedPosition)
            {
                if (square.x > BoardSize ^ square.x < 0 ^ square.y > BoardSize ^ square.y < 0)
                {
                    if (manual)
                    {
                        WriteLine();
                        Display.Alert("Ship should be placed over a board!");
                        ConsoleKeyInfo keyInfo = ReadKey(true);
                    }

                    return false;
                }
            }

            return true;
        }

        private bool CheckSpot(List<(int x, int y)> shipProposedPosition, bool manual)
        {
            foreach (var square in shipProposedPosition)
            {
                if (UsedSquare.Contains(square))
                {
                    if (manual)
                    {
                        WriteLine();
                        Display.Alert("You can't place ship on another ship!");
                        ConsoleKeyInfo keyInfo = ReadKey(true);
                    }

                    return false;
                }
            }

            return true;
        }

        private void GenerateView(List<(int x, int y)> shipProposedPosition, bool manual)
        {
            var viewBoard = ManualPlacementBoard.Clone() as Square[,];

            if (CheckBoundaries(shipProposedPosition, manual))
                foreach (var square in shipProposedPosition)
                {
                    viewBoard[square.x, square.y] = new Square(square.x, square.y, SquareStatus.TESTING);
                }

            Clear();
            Display.ShowBoard(viewBoard);
        }

        private bool PlaceShip((int, int) shipCore, bool isVertical, int shipLenght,
            List<(int x, int y)> shipProposedPosition, Ship ship, bool manual)
        {
            var isAllowed = TryToPlaceShip(shipCore, isVertical, shipLenght, shipProposedPosition, manual);
            if (isAllowed)
            {
                foreach (var square in shipProposedPosition)
                {
                    var testingSquare = ship.SquareForShip(square.x, square.y);
                    ManualPlacementBoard[square.x, square.y] = testingSquare;
                    UsedSquare.Add(square);
                }

                return true;
            }

            if (manual)
            {
                WriteLine();
                Display.Alert("Place not allowed!");
                ConsoleKeyInfo keyInfo = ReadKey(true);  
            }
            
            return false;
        }
    }
}