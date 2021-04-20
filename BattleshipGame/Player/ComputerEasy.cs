using System;
using System.Collections.Generic;

namespace BattleshipGame.Player
{
    public class ComputerEasy : Player

    {
        protected List<(int, int)> UsedCoordinates = new List<(int, int)>();
        

        protected override (int, int) GetPlayerCoordinates()
        {
            return GetRandomCoordinates();
        }
        
        protected (int, int) GetRandomCoordinates()
        {
            Random random = new Random();
            while (true)
            {
                int x = random.Next(0, BoardSize);
                int y = random.Next(0, BoardSize);

                if (!UsedCoordinates.Contains((x, y)))
                {
                    UsedCoordinates.Add((x, y));
                    return (x, y);
                }
            }
        }
    }
}