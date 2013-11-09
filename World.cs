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
            if (livingCells.Count < 3)
            {
                livingCells = new List<Tuple<int, int>>();
            }
        }
    }
}