﻿using System;
using System.IO;
using System.Text;

namespace ImageToRaw
{
    public class Logger
    {
        private static readonly string LOG_FORMAT = "{0} {1} {2}";
        private static readonly string DATETIME_FORMAT = "yyyy/MM/dd HH:mm:ss.fff";
        private StreamWriter stream = null;
        private readonly bool consoleOut;

        private static Logger singletonInstance = null;
        public static Logger GetInstance(string logFilePath, bool consoleOut = false)
        {
            if (singletonInstance == null)
            {
                singletonInstance = new Logger(logFilePath, consoleOut);
            }
            return singletonInstance;
        }

        public static void Init(string logFilePath, bool consoleOut = false)
        {
            singletonInstance = new Logger(logFilePath, consoleOut);
        }

        private Logger(string logFilePath, bool consoleOut)
        {
            if (string.IsNullOrWhiteSpace(logFilePath))
            {
                throw new Exception("logFilePath is empty.");
            }

            var logFile = new FileInfo(logFilePath);
            if (!Directory.Exists(logFile.DirectoryName))
            {
                Directory.CreateDirectory(logFile.DirectoryName);
            }

            stream = new StreamWriter(logFile.FullName, true, Encoding.Default);
            stream.AutoFlush = true;
            this.consoleOut = consoleOut;
        }

        private void write(Level level, string text)
        {
            string log = string.Format(LOG_FORMAT, DateTime.Now.ToString(DATETIME_FORMAT), level.ToString(), text);
            stream.WriteLine(log);
            if (consoleOut)
            {
                Console.WriteLine(log);
            }
        }

        public void Error(string text)
        {
            write(Level.ERROR, text);
        }

        public void Error(Exception ex)
        {
            write(Level.ERROR, ex.Message + Environment.NewLine + ex.StackTrace);
        }

        public void Error(string format, object arg)
        {
            Error(string.Format(format, arg));
        }

        public void Error(string format, params object[] args)
        {
            Error(string.Format(format, args));
        }

        public void Warn(string text)
        {
            write(Level.WARN, text);
        }

        public void Warn(string format, object arg)
        {
            Warn(string.Format(format, arg));
        }

        public void Warn(string format, params object[] args)
        {
            Warn(string.Format(format, args));
        }

        public void Info(string text)
        {
            write(Level.INFO, text);
        }

        public void Info(string format, object arg)
        {
            Info(string.Format(format, arg));
        }

        public void Info(string format, params object[] args)
        {
            Info(string.Format(format, args));
        }

        public void Debug(string text)
        {
            write(Level.DEBUG, text);
        }

        public void Debug(string format, object arg)
        {
            Debug(string.Format(format, arg));
        }

        public void Debug(string format, params object[] args)
        {
            Debug(string.Format(format, args));
        }

        public void Trace(string text)
        {
            write(Level.TRACE, text);
        }

        public void Trace(string format, object arg)
        {
            Trace(string.Format(format, arg));
        }

        public void Trace(string format, params object[] args)
        {
            Trace(string.Format(format, args));
        }

        private enum Level
        {
            ERROR,
            WARN,
            INFO,
            DEBUG,
            TRACE
        }
    }
}
