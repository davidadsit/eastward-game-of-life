using System;
using GameOfLife;

namespace GoL
{
    class Program
    {
        static void Main(string[] args)
        {
            var world = new World();
            world.AddLivingCell(-20, -20);
            world.AddLivingCell(-20, -21);
            world.AddLivingCell(-21, -20);
            world.AddLivingCell(-21, -21);
            world.AddLivingCell(20, 20);
            world.AddLivingCell(20, 21);
            world.AddLivingCell(21, 20);
            world.AddLivingCell(21, 21);
            world.AddLivingCell(0, 0);
            world.AddLivingCell(0, 1);
            world.AddLivingCell(0, 2);
            world.AddLivingCell(-1, 2);
            world.AddLivingCell(-2, 1);
            while (true)
            {
                world.Print(new Printer());
                world.Evolve();
                Console.Out.WriteLine("");
                Console.In.ReadLine();
            }
        }
    }

    class Printer : IPrinter
    {
        public void PrintRow(string row)
        {
            Console.WriteLine(row);
        }
    }
}