namespace lab22.Shapes
{
    // Похідний клас, який порушує LSP
    public class Square : Rectangle
    {
        private int size;

        public override int Width
        {
            get => size;
            set { size = value; base.Width = size; base.Height = size; }
        }

        public override int Height
        {
            get => size;
            set { size = value; base.Width = size; base.Height = size; }
        }
    }
}
