using System;
using System.IO;
using System.Threading;

namespace Elements.Logger
{
    class Logging
    {
        private static readonly ReaderWriterLockSlim rwl = new ReaderWriterLockSlim();

        public static void LogMessage(string logName, string logMessage, string logType, string msgSource)
        {
            string dirPath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\logs\\";
            string filePath = dirPath + logName + "_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";

            Directory.CreateDirectory(dirPath);
            try
            {
                rwl.EnterWriteLock();
                switch (logType)
                {
                    case "log":
                        using (StreamWriter sw = File.AppendText(filePath))
                        {
                            sw.WriteLine(string.Format("{0:G} ({2}): {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffff"), logMessage, msgSource));
                        }
                        break;
                    case "error":
                        using (StreamWriter sw = File.AppendText(filePath))
                        {
                            sw.WriteLine(string.Format("{0:G} ({2}): Exception: {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffff"), logMessage, msgSource));
                        }
                        break;
                }
            }
            finally
            {
                rwl.ExitWriteLock();
            }
        }

        public static void LogCleanup(int purgeBeforeDays)
        {
            LogMessage("System", "Deleting log files more than " + purgeBeforeDays + " days old", "log", "LogCleanup");
            foreach (string f in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\logs\\"))
            {
                if ((DateTime.Now.Date - File.GetCreationTime(f).Date).TotalDays > purgeBeforeDays && !Path.GetFileNameWithoutExtension(f).StartsWith("Error"))
                {
                    File.Delete(f);
                    LogMessage("System", "Deleted '" + Path.GetFileName(f) + "'.", "log", "LogCleanup");
                }
            }
            LogMessage("System", "Deleted log files more than " + purgeBeforeDays + " days old", "log", "PurgeOldData");
        }
    }
}