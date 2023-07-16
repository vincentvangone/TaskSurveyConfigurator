using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ErrorLogger
{
    public static class Logger
    {
        public static void WriteLog(string LogMessage) {

            string LogPath = ConfigurationManager.AppSettings["LogPath"];
            using (StreamWriter Writer = File.AppendText(LogPath))
            {
                TextWriter w = Writer;
                Writer.Write("\r\nLog Entry : ");
                Writer.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                Writer.WriteLine("  :");
                Writer.WriteLine($"  :{LogMessage}");
                Writer.WriteLine("-------------------------------");
            }
            MessageBox.Show(LogMessage);
        }
    }
}
