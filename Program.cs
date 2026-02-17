using lab23.Interfaces;
using lab23.Modules;

namespace lab23
{
    class Program
    {
        static void Main(string[] args)
        {
            IPrinter printer = new PrinterModule();
            IScanner scanner = new ScannerModule();
            IFax fax = new FaxModule();

            SmartMachine machine = new SmartMachine(printer, scanner, fax);

            machine.Print("Report.pdf");
            machine.Scan();
            machine.Fax("+380123456789");
        }
    }
}
