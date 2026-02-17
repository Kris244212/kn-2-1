namespace lab22.Shapes
{
    // Rectangle реалізує інтерфейс IShape
    public class RectangleLSP : IShape
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public int Area() => Width * Height;
    }
}
