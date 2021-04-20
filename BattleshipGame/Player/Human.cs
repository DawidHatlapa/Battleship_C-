namespace BattleshipGame.Player
{
    public class Human : Player
    {
        protected override (int, int) GetPlayerCoordinates()
        {
            var shotCoordinates = Input.GetCoordinates(BoardSize);
            return shotCoordinates;
        }
    }
}