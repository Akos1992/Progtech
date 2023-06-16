using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tesztpizza
{
    public abstract class Logger
    {
        protected StreamWriter writer;

        public bool AutoClose { get; set; }

        public Logger(bool autoClose)
        {
            AutoClose = autoClose;
        }

        public void Log(string message)
        {
            writer.WriteLine(message);
            if (AutoClose)
            {
                Close();
            }
        }

        public void Close()
        {
            writer?.Close();
        }
    }
}
