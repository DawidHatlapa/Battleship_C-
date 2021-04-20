using System;
using System.Collections.Generic;
using BattleshipGame.BoardFolder;
using BattleshipGame.GetShow;
using static System.Console;

namespace BattleshipGame.Game
{
    public class Ship
    {
        private string Owner;
        public ShipType type;
        private bool Hit;
        private bool Sunk;
        public List<Square> fields;
        private Display Display = new Display();

        public Ship(ShipType typeOfShip, string owner)
        {
            this.type = typeOfShip;
            fields = new List<Square>(GetShipLength());
            this.Owner = owner;
        }


        public int GetShipLength()
        {
            return (int) this.type;
        }


        public Square SquareForShip(int x, int y)
        {
            var oneField = new Square(x, y, true);
            fields.Add(oneField);
            return oneField;
        }

        public void TryToSunkShip()
        {
            if (fields.TrueForAll(IsHit))
            {
                foreach (var field in fields)
                {
                    field.Status = SquareStatus.SUNK;
                }
                Display.Sunk();
            }
        }

        private bool IsHit(Square field)
        {
            return field.Status == SquareStatus.HIT;
            
        }
        
    }
}