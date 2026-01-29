namespace lab22.Shapes
{
    // Базовий клас Rectangle, який реалізує IShape
    public class Rectangle : IShape
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public int Area() => Width * Height;
    }
}
