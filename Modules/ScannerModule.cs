using System;
using lab23.Interfaces;

namespace lab23.Modules
{
    public class ScannerModule : IScanner
    {
        public void Scan()
        {
            Console.WriteLine("Scanning document...");
        }
    }
}
