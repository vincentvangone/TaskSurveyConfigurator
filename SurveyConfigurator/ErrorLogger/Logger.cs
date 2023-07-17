using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ErrorLogger
{
    public static class Logger
    {
        public static void WriteLog(string LogMessage, string Type, string ExtraInfo = "Warning")
        {
            
            using (StreamWriter Writer = File.AppendText(LogPath))
            {
                //TextWriter w = Writer;
                Writer.Write("\r\nLog Entry : ");
                Writer.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                Writer.WriteLine();
                Writer.WriteLine("Log Type :");
                Writer.WriteLine($"{Type}");
                Writer.WriteLine();
                Writer.WriteLine("Displayed message :");
                Writer.WriteLine($"{LogMessage}");
                Writer.WriteLine();
                Writer.WriteLine("Extra Info :");
                Writer.WriteLine($"{ExtraInfo}");
                Writer.WriteLine();
                Writer.WriteLine("Call Stack Trace :");
                Writer.WriteLine($"{Environment.StackTrace}");
                Writer.WriteLine();
                Writer.WriteLine("------------------------------------------------------------");

            }
            if(LogMessage != null) { 
                MessageBox.Show(LogMessage);
            }
        }
    }
}
