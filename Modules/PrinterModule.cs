using System;
using lab23.Interfaces;

namespace lab23.Modules
{
    public class PrinterModule : IPrinter
    {
        public void Print(string document)
        {
            Console.WriteLine($"Printing: {document}");
        }
    }
}
