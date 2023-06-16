using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tesztpizza
{
    public class FileLogger : Logger
    {
        public FileLogger(string filePath): base(true)
        {
            writer = new StreamWriter(new FileStream(filePath, FileMode.Append));
        }
    }
}
