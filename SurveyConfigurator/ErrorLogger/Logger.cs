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
            try
            {
                string LogPath = ConfigurationManager.AppSettings["LogPath"];
                //when the file reaches 1 mega -> delete
                if ((LogPath.Length) / (1024 * 1024) >= 1) File.Delete(LogPath);

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
                    if (LogMessage!="")
                        Writer.WriteLine($"{LogMessage}");
                    else
                        Writer.WriteLine("None");
                    Writer.WriteLine();
                    Writer.WriteLine("Extra Info :");
                    Writer.WriteLine($"{ExtraInfo}");
                    Writer.WriteLine();
                    Writer.WriteLine("Call Stack Trace :");
                    Writer.WriteLine($"{Environment.StackTrace}");
                    Writer.WriteLine();
                    Writer.WriteLine("------------------------------------------------------------");
                }
                
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }
    }
}
