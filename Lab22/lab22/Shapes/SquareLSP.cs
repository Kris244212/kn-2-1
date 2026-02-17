namespace lab22.Shapes
{
    // Square реалізує інтерфейс IShape
    public class SquareLSP : IShape
    {
        public int Size { get; set; }

        public int Area() => Size * Size;
    }
}
