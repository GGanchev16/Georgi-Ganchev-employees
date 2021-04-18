

namespace Georgi_Ganchev_employees.IO.Contracts
{
    interface IWriter
    {
        void Write(string message);

        void WriteLine(string message);
    }
}
