using System.Collections.Generic;
using System.Linq;
using BattleshipGame.BoardFolder;
using BattleshipGame.Game;

namespace BattleshipGame.Player
{
    public class ComputerHard : ComputerNormal
    {
        private List<(int, int)> HighestOrderCoordinates;

        protected override (int, int) GetPlayerCoordinates()
        {
            HighestOrderCoordinates = new List<(int, int)>();
            HighOrderCoordinates = new List<(int, int)>();
            AnalyzeBoard();
            AnalyzeCells();

            foreach (var cell in HighestOrderCoordinates)
            {
                if (!UsedCoordinates.Contains(cell))
                {
                    UsedCoordinates.Add(cell);
                    return cell;
                }
            }

            foreach (var cell in HighOrderCoordinates)
            {
                if (!UsedCoordinates.Contains(cell))
                {
                    UsedCoordinates.Add(cell);
                    return cell;
                }
            }

            return GetRandomCoordinates();
        }

        private void AnalyzeCells()
        {
            if (!HighOrderCoordinates.Any()) return;

            List<(int, int)> hitCells = new List<(int, int)>();
            foreach (var cell in HighOrderCoordinates)
            {
                if (PlayerBoard[cell.Item1, cell.Item2].Status == SquareStatus.HIT)
                {
                    hitCells.Add(cell);
                }
            }

            if (hitCells.Count >= 2)
            {
                var direction = (hitCells[0].Item1 - hitCells[1].Item1, hitCells[0].Item2 - hitCells[1].Item2);
                foreach (var cell in hitCells)
                {
                    var newCell1 = (cell.Item1 + direction.Item1, cell.Item2 + direction.Item2);
                    var newCell2 = (cell.Item1 - direction.Item1, cell.Item2 - direction.Item2);
                    TryToAddNewHighestOrderCord(newCell1);
                    TryToAddNewHighestOrderCord(newCell2);
                }
            }
        }

        private void TryToAddNewHighestOrderCord((int x, int y) cell)
        {
            if (cell.x >= 0 || cell.x < BoardSize || cell.y >= 0 || cell.y < BoardSize)
            {
                HighestOrderCoordinates.Add(cell);
            }
        }
    }
}