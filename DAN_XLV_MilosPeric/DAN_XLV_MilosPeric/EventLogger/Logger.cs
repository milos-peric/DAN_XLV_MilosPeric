using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_XLV_MilosPeric.EventLogger
{
    internal class Logger
    {
        private const string _logFilePath = @"..\..\Log.txt";
        public static string logMessage = "";

        public static void LogToFile()
        {
            try
            {
                DateTime currentTime = DateTime.Now;
                using (StreamWriter streamWriter = new StreamWriter(_logFilePath, append: true))
                {
                    streamWriter.Write(currentTime.ToString() + " - " + logMessage + "\n");
                }
                logMessage = "";
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: cannot write log to file.");
                Debug.WriteLine(e.Message);
            }
        }
    }
}
