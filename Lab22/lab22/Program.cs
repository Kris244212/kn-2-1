using System;
using lab22.Shapes;

namespace lab22
{
    class Program
    {
        static void PrintArea(IShape shape)
        {
            Console.WriteLine($"Area = {shape.Area()}");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("=== LSP Сумісне рішення ===");

            RectangleLSP rect = new RectangleLSP { Width = 5, Height = 10 };
            SquareLSP sq = new SquareLSP { Size = 7 };

            PrintArea(rect);  // Area = 50
            PrintArea(sq);    // Area = 49
        }
    }
}
