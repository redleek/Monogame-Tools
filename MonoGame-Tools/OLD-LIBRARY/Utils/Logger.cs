using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.ExceptionServices;

namespace MonoGame_Tools
{
    public class Logger : TextWriter
    {

        static StreamWriter m_memoryStream;
        static bool ThrowAll = false;

        static Logger()
        {
            m_memoryStream = new StreamWriter("logs.txt");
        }

        public static void Close()
        {
            m_memoryStream.Close();
            m_memoryStream.Dispose();
        }

        public static void LogRaw(string message)
        {
            m_memoryStream.WriteLine(message);
            m_memoryStream.Flush();
        }

        public override void WriteLine(string value)
        {
            LogRaw(value);
        }

        public static void LogMessage(string message, params object[] arguments)
        {
            LogMessage(LogMessageType.Info, message, arguments);
        }

        public static void LogMessage(LogMessageType type, string message, params object[] arguments)
        {
            string outmsg = string.Format(message, arguments);

            if (!string.IsNullOrWhiteSpace(outmsg))
                LogRaw(string.Format("[{0}] - {1}", type, outmsg));
        }

        public static void LogException(Exception e, bool isFatal)
        {
            LogMessage(LogMessageType.Exception, "{0} has occured: {1}", e.GetType(), e.Message);
            LogRaw(e.StackTrace);

            if (isFatal || ThrowAll)
                ExceptionDispatchInfo.Capture(e).Throw();
        }

        public override Encoding Encoding
        {
            get { return Encoding.ASCII; }
        }
    }

    public enum LogMessageType
    {
        Info,
        Warning,
        Error,
        Exception,
        Script
    }
}