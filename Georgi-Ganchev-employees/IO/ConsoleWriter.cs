
using Georgi_Ganchev_employees.IO.Contracts;
using System;

namespace Georgi_Ganchev_employees.IO
{
    class ConsoleWriter : IWriter
    {
        public void Write(string message)
        {
            Console.Write(message);
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}
