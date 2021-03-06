﻿using System;
using System.IO;
using System.Reflection;

namespace FFTriadBuddy
{
    public class Logger
    {
        private static StreamWriter logWriter;
        private static StreamWriter logWriterDefault;

        public static void Initialize(string[] Args)
        {
            foreach (string cmdArg in Args)
            {
                if (cmdArg == "-log")
                {
                    logWriter = new StreamWriter("debugLog.txt");
                }
            }

            try
            {
                string appName = Assembly.GetEntryAssembly().GetName().Name;
                string settingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), appName);
                Directory.CreateDirectory(settingsPath);

                logWriterDefault = new StreamWriter(Path.Combine(settingsPath, "outputLog.txt"));
            }
            catch (Exception) { }
        }

        public static void Close()
        {
            logWriterDefault.Close();
        }

        public static bool IsActive()
        {
            return logWriter != null;
        }

        public static void WriteLine(string str)
        {
            Console.WriteLine(str);
            
            if (logWriter != null)
            {
                logWriter.WriteLine(str);
                logWriter.Flush();
            }

            if (logWriterDefault != null)
            {
                logWriterDefault.WriteLine(str);
            }
        }
    }
}
