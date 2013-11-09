using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public class World
    {
        int generation;
        List<Tuple<int, int>> livingCells = new List<Tuple<int, int>>();

        public void Print(IPrinter printer)
        {
            printer.PrintRow("Generation: " + generation);
            if (livingCells.None())
            {
                printer.PrintRow("-");
            }
            else
            {
                var rowNumbers = livingCells.Select(x => x.Item1);
                var columnNumbers = livingCells.Select(x => x.Item2);
                for (var row = rowNumbers.Min() - 1; row <= rowNumbers.Max() + 1; row++)
                {
                    var cellsToPrint = new StringBuilder();
                    for (var column = columnNumbers.Min() - 1; column <= columnNumbers.Max() + 1; column++)
                    {
                        cellsToPrint.Append(livingCells.Any(x => x.Item1 == row && x.Item2 == column) ? "O" : "-");
                    }
                    printer.PrintRow(cellsToPrint.ToString());
                }
            }
        }

        public void AddLivingCell(int row, int column)
        {
            livingCells.Add(new Tuple<int, int>(row, column));
        }

        public void Evolve()
        {
            generation++;
            var newWorld = new List<Tuple<int, int>>();
            AddCellsThatWillContinueToLive(newWorld);
            AddCellsThatWillComeToLife(newWorld);
            livingCells = newWorld;
        }

        void AddCellsThatWillComeToLife(List<Tuple<int, int>> newWorld)
        {
            if (!livingCells.Any())
            {
                return;
            }
            var rowNumbers = livingCells.Select(x => x.Item1);
            var columnNumbers = livingCells.Select(x => x.Item2);
            for (var row = rowNumbers.Min() - 1; row <= rowNumbers.Max() + 1; row++)
            {
                for (var column = columnNumbers.Min() - 1; column <= columnNumbers.Max() + 1; column++)
                {
                    if (TheCellIsDeadAndShouldLive(row, column))
                    {
                        newWorld.Add(new Tuple<int, int>(row, column));
                    }
                }
            }
        }

        bool TheCellIsDeadAndShouldLive(int row, int column)
        {
            if (livingCells.Any(x => x.Item1 == row && x.Item2 == column)) return false;
            var livingNeighbors = livingCells.Count(x => x.Item1 >= (row - 1) &&
                                                         x.Item1 <= (row + 1) &&
                                                         x.Item2 >= (column - 1) &&
                                                         x.Item2 <= (column + 1));
            return livingNeighbors == 3;
        }

        void AddCellsThatWillContinueToLive(List<Tuple<int, int>> newWorld)
        {
            foreach (var livingCell in livingCells)
            {
                var livingNeighbors =
                    livingCells.Count(x => x.Item1 >= (livingCell.Item1 - 1) &&
                                           x.Item1 <= (livingCell.Item1 + 1) &&
                                           x.Item2 >= (livingCell.Item2 - 1) &&
                                           x.Item2 <= (livingCell.Item2 + 1)) - 1;
                if (livingNeighbors == 2 || livingNeighbors == 3)
                {
                    newWorld.Add(livingCell);
                }
            }
        }
    }
}