using System;
using lab23.Interfaces;

namespace lab23.Modules
{
    public class FaxModule : IFax
    {
        public void Fax(string number)
        {
            Console.WriteLine($"Sending fax to {number}");
        }
    }
}
