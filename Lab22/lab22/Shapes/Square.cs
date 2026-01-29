namespace lab22.Shapes
{
    // Square реалізує IShape безпосередньо (правильна модель)
    public class Square : IShape
    {
        public int Size { get; set; }

        public int Area() => Size * Size;
    }
}
