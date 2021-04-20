using System.Collections.Generic;
using System.Linq;
using BattleshipGame.BoardFolder;

namespace BattleshipGame.Player
{
    public class ComputerNormal : ComputerEasy
    {
        protected List<(int, int)> HighOrderCoordinates;


        protected override (int, int) GetPlayerCoordinates()
        {
            HighOrderCoordinates = new List<(int, int)>();
            AnalyzeBoard();
            if (HighOrderCoordinates.Any())
            {
                foreach (var cell in HighOrderCoordinates)
                {
                    if (!UsedCoordinates.Contains(cell))
                    {
                        UsedCoordinates.Add(cell);
                        return cell;
                    }
                }
            }

            return GetRandomCoordinates();
        }

        protected void AnalyzeBoard()
        {
            foreach (var cell in PlayerBoard)
            {
                if (cell.Status == SquareStatus.HIT)
                {
                    var generatedCells = GenerateCells(cell.Position);
                    foreach (var candidateCell in generatedCells)
                    {
                        HighOrderCoordinates.Add(candidateCell);
                    }
                }
            }
        }

        protected List<(int, int)> GenerateCells((int x, int y) core)
        {
            List<(int, int)> newCells = new List<(int, int)>();

            if (core.x + 1 < BoardSize)
            {
                newCells.Add((core.x + 1, core.y));
            }

            if (core.x - 1 >= 0)
            {
                newCells.Add((core.x - 1, core.y));
            }

            if (core.y + 1 < BoardSize)
            {
                newCells.Add((core.x, core.y + 1));
            }

            if (core.y - 1 >= 0)
            {
                newCells.Add((core.x, core.y - 1));
            }

            return newCells;
        }
    }
}