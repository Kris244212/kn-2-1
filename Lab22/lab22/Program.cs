using System;
using System.Linq;
using lab22.Shapes;
using lab22.Data;
using lab22.Models;

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
            Console.WriteLine("=== LSP Сумісне рішення ===\n");

            // Створення фігур
            RectangleLSP rect = new RectangleLSP { Width = 5, Height = 10 };
            SquareLSP sq = new SquareLSP { Size = 7 };

            Console.WriteLine("Обчислені площі:");
            PrintArea(rect);  // Area = 50
            PrintArea(sq);    // Area = 49

            // Робота з БД
            Console.WriteLine("\n=== Збереження у базу даних ===\n");
            SaveShapesToDatabase(rect, sq);

            // Читання з БД
            Console.WriteLine("\n=== Дані з бази даних ===\n");
            DisplayAllShapesFromDatabase();
        }

        static void SaveShapesToDatabase(RectangleLSP rect, SquareLSP sq)
        {
            using (var context = new ShapeDbContext())
            {
                // Видалення старих даних (опціонально)
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                // Додавання Rectangle
                var rectRecord = new ShapeRecord
                {
                    ShapeType = "Rectangle",
                    Dimension1 = rect.Width,
                    Dimension2 = rect.Height,
                    CalculatedArea = rect.Area()
                };

                // Додавання Square
                var sqRecord = new ShapeRecord
                {
                    ShapeType = "Square",
                    Dimension1 = sq.Size,
                    Dimension2 = null,
                    CalculatedArea = sq.Area()
                };

                context.Shapes.Add(rectRecord);
                context.Shapes.Add(sqRecord);
                context.SaveChanges();

                Console.WriteLine("✓ Rectangle збережено в БД");
                Console.WriteLine("✓ Square збережено в БД");
            }
        }

        static void DisplayAllShapesFromDatabase()
        {
            using (var context = new ShapeDbContext())
            {
                var shapes = context.Shapes.OrderBy(s => s.CreatedAt).ToList();

                if (shapes.Count == 0)
                {
                    Console.WriteLine("БД порожня");
                    return;
                }

                foreach (var shape in shapes)
                {
                    Console.WriteLine(shape);
                }

                Console.WriteLine($"\nВсього фігур в БД: {shapes.Count}");
                Console.WriteLine($"Сумарна площа: {shapes.Sum(s => s.CalculatedArea)}");
            }
        }
    }
}
