using System;
using log4net;
using log4net.Config;

namespace ClientApp.Other
{
    public static class Logger
    {
        private static ILog _log = LogManager.GetLogger("MyBaseLogger");

        static Logger()
        {
            XmlConfigurator.Configure();
        }

        public static void Info(string message)
        {
            _log.Info(message);
        }

        public static void Warning(string message)
        {
            _log.Warn(message);
        }

        public static void Error(string message, Exception e)
        {
            _log.Error(message, e);

            Console.Error.WriteLine($"{message}. {e.Message}");
        }
    }
}
