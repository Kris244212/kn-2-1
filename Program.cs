<<<<<<< HEAD
﻿using lab23.Interfaces;
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
=======
using System;
using lab21.Strategies;
using lab21.Factories;
using lab21.Services;

namespace lab21
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Введіть тип доставки (Standard, Express, International, Night):");
            string? type = Console.ReadLine();

            Console.WriteLine("Введіть відстань (км):");
            decimal distance = decimal.Parse(Console.ReadLine() ?? "0");

            Console.WriteLine("Введіть вагу (кг):");
            decimal weight = decimal.Parse(Console.ReadLine() ?? "0");

            try
            {
                IShippingStrategy strategy = ShippingStrategyFactory.CreateStrategy(type ?? "Standard");
                DeliveryService service = new DeliveryService();
                decimal cost = service.CalculateDeliveryCost(distance, weight, strategy);

                Console.WriteLine($"Вартість доставки ({type}) = {cost} грн");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
>>>>>>> 4551988f51c48701a0709e940a04f4e14774a586
        }
    }
}
