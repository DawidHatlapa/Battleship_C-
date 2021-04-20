namespace BattleshipGame.BoardFolder
{
    public class Board
    {
        private Square[,] ocean;
        

        public Board(int size)
        {
            ocean = new Square[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Square oneSquare = new Square(i, j);
                    ocean[i, j] = oneSquare;
                }
            }
        }
        
        public Square[,] GetBoard()
        {
            return ocean;
        }
        
    }
}