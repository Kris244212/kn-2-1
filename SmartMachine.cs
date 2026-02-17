using lab23.Interfaces;

namespace lab23
{
    public class SmartMachine
    {
        private readonly IPrinter _printer;
        private readonly IScanner _scanner;
        private readonly IFax _fax;

        public SmartMachine(IPrinter printer, IScanner scanner, IFax fax)
        {
            _printer = printer;
            _scanner = scanner;
            _fax = fax;
        }

        public void Print(string document)
        {
            _printer.Print(document);
        }

        public void Scan()
        {
            _scanner.Scan();
        }

        public void Fax(string number)
        {
            _fax.Fax(number);
        }
    }
}
