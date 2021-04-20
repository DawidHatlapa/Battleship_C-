using System;

namespace BattleshipGame.BoardFolder
{
    public class Square
    {
        public (int i, int j) Position { get; }
        public SquareStatus Status { get; set; }

        public Square(int i, int j, bool isShip = false)
        {
            this.Position = (i, j);
            Status = isShip ? SquareStatus.SHIP : SquareStatus.EMPTY;
        }

        public Square(int i, int j, SquareStatus status)
        {
            this.Position = (i, j);
            Status = status;
        }


        public char Character => Status switch
        {
            SquareStatus.HIT => 'H',
            SquareStatus.SHIP => 'S',
            SquareStatus.EMPTY => '_',
            SquareStatus.MISSED => '~',
            SquareStatus.TESTING => '#',
            SquareStatus.SUNK => 'X',
            _ => '\0'
        };


        // public (int i, int j) GetPosition()
        // {
        //     return this.Position;
        // }
        //
    }
}