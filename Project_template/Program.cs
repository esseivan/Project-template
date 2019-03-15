using EsseivaN.Tools;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Project_template
{
    static class Program
    {
        public static Logger LogManager { get; set; }

        public static void WriteLog(string Text)
        {
            WriteLog(Text, Logger.Log_level.Debug);
        }

        public static void WriteLog(string Text, Logger.Log_level logLevel)
        {
            if (LogManager == null)
            {
                return;
            }

            LogManager.WriteLog(Text, logLevel);
        }

        public static Logger Initialize(string Path, Logger.SaveFileMode SaveMode, Logger.WriteMode WriteMode, Logger.Suffix_mode SuffixMode)
        {
            LogManager = new Logger()
            {
                LogToFile_FilePath = Path,
                LogToFile_Mode = SaveMode,
                LogToFile_WriteMode = WriteMode,
                LogToFile_SuffixMode = SuffixMode,
            };
            return LogManager;
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string logPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"{MANUFACTURER}\{PRODUCTNAME}", "log.log");
            Initialize(logPath, Logger.SaveFileMode.FileName_LastPrevious, Logger.WriteMode.Write, Logger.Suffix_mode.RunTime);

            // Get command line arguments
            var args = Environment.GetCommandLineArgs().ToList();
            // Remove run pah
            args.RemoveAt(0);

            if (args.Contains("-installer"))
            {
                WriteLog("Installer argument detected", Logger.Log_level.Info);
                // Remove installer argument
                args.Remove("-installer");

                // Generate new arguments
                string args_line = (args.Count > 0) ? string.Join(" ", args) : string.Empty;

                if (args_line == string.Empty)
                {
                    WriteLog("Running app with no arguments", Logger.Log_level.Info);
                }
                else
                {
                    WriteLog("Running with new arguments : " + args_line, Logger.Log_level.Info);
                }

                // Restart the exe without the "-installer" argument
                Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location, args_line);
                return;
            }

            // Run the winforms app
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain(LogManager));
        }
    }
}
