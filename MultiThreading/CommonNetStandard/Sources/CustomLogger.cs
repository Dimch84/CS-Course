using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public static class CustomLogger
    {
        public static void LogMessage(String text, params object[] args)
        {
            LogTabMessage(0, text, args);
        }

        public static void LogTabMessage(Int32 tab, String text, params object[] args)
        {
            String preamble = FormatPreamble(tab);
            StringBuilder logMsg = new StringBuilder();
            logMsg.AppendFormat(preamble);
            logMsg.AppendFormat(text, args);

            Console.WriteLine("{0}", logMsg);
        }

        public static void LogException(Exception exc)
        {
            Console.WriteLine(exc);
        }

        public static void LogStartExample(String exampleName)
        {
            Console.WriteLine();
            Console.WriteLine("====={0}=====", exampleName);
        }

        public static void LogEndExample()
        {
            Console.WriteLine("=====End=====");
        }

        private static String FormatPreamble(Int32 tab)
        {
            StringBuilder preamble = new StringBuilder();
            preamble.AppendFormat("[{0}] <Thread#{1:D2}> <Task#{2:D2}> ",
                DateTime.Now.ToString("HH:mm:ss.fff"),
                Thread.CurrentThread.ManagedThreadId,
                Task.CurrentId);

            for (int i = 0; i < tab; ++i)
                preamble.AppendFormat("    ");

            return preamble.ToString();
        }
    }
}
