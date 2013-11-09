using Moq;
using NUnit.Framework;

namespace GameOfLife
{
    [TestFixture]
    public class WorldSpecs
    {
        [SetUp]
        public void SetUp()
        {
            World = new World();
            Printer = new Mock<IPrinter>();
        }

        World World;
        Mock<IPrinter> Printer;

        [Test]
        public void When_an_new_world_is_print()
        {
            World.Print(Printer.Object);

            Printer.Verify(x => x.PrintRow("Generation: 0"));
            Printer.Verify(x => x.PrintRow("-"));
        }

        [Test]
        public void When_printing_a_world_with_a_living_cell()
        {
            World.AddLivingCell(0, 0);
            World.Print(Printer.Object);

            Printer.Verify(x => x.PrintRow("Generation: 0"));
            Printer.Verify(x => x.PrintRow("---"), Times.Exactly(2));
            Printer.Verify(x => x.PrintRow("-O-"), Times.Once);
        }

        [Test]
        public void When_printing_a_world_with_two_living_cells()
        {
            World.AddLivingCell(0, 0);
            World.AddLivingCell(0, 1);
            World.Print(Printer.Object);

            Printer.Verify(x => x.PrintRow("Generation: 0"));
            Printer.Verify(x => x.PrintRow("----"), Times.Exactly(2));
            Printer.Verify(x => x.PrintRow("-OO-"), Times.Once);
        }

        [Test]
        public void When_a_world_with_a_cell_with_no_living_neighbors_evolves()
        {
            World.AddLivingCell(0,0);

            World.Evolve();
            World.Print(Printer.Object);

            Printer.Verify(x => x.PrintRow("Generation: 1"));
            Printer.Verify(x => x.PrintRow("-"));
        }

        [Test]
        public void When_a_world_with_a_cell_with_one_living_neighbors_evolves()
        {
            World.AddLivingCell(0,0);
            World.AddLivingCell(0,1);

            World.Evolve();
            World.Print(Printer.Object);

            Printer.Verify(x => x.PrintRow("Generation: 1"));
            Printer.Verify(x => x.PrintRow("-"));
        }

        [Test]
        public void When_a_world_with_a_cell_with_two_living_neighbors_evolves()
        {
            World.AddLivingCell(0,0);
            World.AddLivingCell(0,1);
            World.AddLivingCell(1,0);
            World.AddLivingCell(1,1);

            World.Evolve();
            World.Print(Printer.Object);

            Printer.Verify(x => x.PrintRow("Generation: 1"));
            Printer.Verify(x => x.PrintRow("----"), Times.Exactly(2));
            Printer.Verify(x => x.PrintRow("-OO-"), Times.Exactly(2));
        }
    }
}