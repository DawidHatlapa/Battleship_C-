using System.Collections.Generic;
using BattleshipGame.BoardFolder;
using BattleshipGame.Game;
using BattleshipGame.GetShow;

namespace BattleshipGame.Player
{
    public abstract class Player
    {
        public List<Ship> ListOfShips = new List<Ship>();
        private bool IsAlive { get; set; }
        public string NameOfPlayer;
        public Square[,] PlayerBoard;
        protected int BoardSize;
        private Display Display = new Display();
        protected Input Input = new Input();


        public void CreatePlayer(string name, int boardSize)
        {
            NameOfPlayer = name;
            IsAlive = true;
            BoardSize = boardSize;
            PlayerBoard = new BoardFolder.Board(boardSize).GetBoard();
            SetShipCollection();
        }

        private void SetShipCollection()
        {
            Ship carrier = new Ship(ShipType.Carrier, NameOfPlayer);
            Ship battleship = new Ship(ShipType.Battleship, NameOfPlayer);
            Ship cruiser = new Ship(ShipType.Cruiser, NameOfPlayer);
            Ship submarine = new Ship(ShipType.Submarine, NameOfPlayer);
            Ship destroyer = new Ship(ShipType.Destroyer, NameOfPlayer);
            ListOfShips.Add(carrier);
            ListOfShips.Add(battleship);
            ListOfShips.Add(cruiser);
            ListOfShips.Add(submarine);
            ListOfShips.Add(destroyer);
        }


        public bool MakeShot(List<Ship> enemyShips)
        {
            var shotCoordinates = GetPlayerCoordinates();
            // var ships = this.ListOfShips;
            if (PlayerBoard[shotCoordinates.Item1, shotCoordinates.Item2].Status == SquareStatus.EMPTY)
            {
                foreach (var ship in enemyShips)
                {
                    var shipFields = ship.fields;
                    foreach (var field in shipFields)
                    {
                        if (field.Position == shotCoordinates)
                        {
                            field.Status = SquareStatus.HIT;
                            ship.TryToSunkShip();
                            PlayerBoard[shotCoordinates.Item1, shotCoordinates.Item2] = field;
                            Display.Hit();
                            return false;
                        }
                    }
                }

                PlayerBoard[shotCoordinates.Item1, shotCoordinates.Item2].Status = SquareStatus.MISSED;
                Display.Miss();
            }
            else
            {
                Display.Alert("You've used this coordinates before!");
            }

            return true;

        }

        protected abstract (int, int) GetPlayerCoordinates();


        public bool CheckIfIsAlive()
        {
            AllShipsSunk(ListOfShips);
            return IsAlive;
        }


        private void AllShipsSunk(List<Ship> ships)
        {
            foreach (var ship in ships)
            {
                foreach (var field in ship.fields)
                {
                    if (field.Status != SquareStatus.SUNK)
                        return;
                }
            }
            IsAlive = false;
        }
    }
}