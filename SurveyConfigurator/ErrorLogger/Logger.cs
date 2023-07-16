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
        public static void WriteLog(string LogMessage, string ExtraInfo="Warning",[CallerMemberName] string callingMethod = "",
        [CallerFilePath] string callingFilePath = "",
        [CallerLineNumber] int callingFileLineNumber = 0)
        {

            string LogPath = ConfigurationManager.AppSettings["LogPath"];
            using (StreamWriter Writer = File.AppendText(LogPath))
            {
                TextWriter w = Writer;
                Writer.Write("\r\nLog Entry : ");
                Writer.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                Writer.WriteLine("Displayed message :");
                Writer.WriteLine($"{LogMessage}");
                Writer.WriteLine();
                Writer.WriteLine("Extra Info :");
                Writer.WriteLine($"{ExtraInfo}");
                Writer.WriteLine();
                Writer.WriteLine("callingMethod :");
                Writer.WriteLine($"{callingMethod}");
                Writer.WriteLine();
                Writer.WriteLine("callingFilePath :");
                Writer.WriteLine($"{callingFilePath}");
                Writer.WriteLine();
                Writer.WriteLine("callingFileLineNumber :");
                Writer.WriteLine($"{callingFileLineNumber}");
                Writer.WriteLine("------------------------------------------------------------");

            }
            MessageBox.Show(LogMessage);
        }
    }
}
