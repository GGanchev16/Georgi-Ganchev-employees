
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Georgi_Ganchev_employees.IO
{
    public class FileReaderExampleInputFile
    {
        private string exampleInputFilePath = Path.Combine(@"..\..\..\ExampleInputFile.txt");
        public List<string> ReadAllLines()
        {
            return File.ReadAllLines(exampleInputFilePath).ToList();
        }
    }
}
