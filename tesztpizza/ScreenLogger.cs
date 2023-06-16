using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tesztpizza
{
    public class ScreenLogger : Logger
    {
        public ScreenLogger(): base(false)
        {
            writer = new StreamWriter(Console.OpenStandardOutput());
            writer.AutoFlush = true;
        }
    }
}
