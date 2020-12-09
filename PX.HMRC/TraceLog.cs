using System.Diagnostics;

namespace PX.HMRC
{
    internal static class Trace
    {
        public static void WriteError(string message)
        {
            Debug.WriteLine(message);
        }

        public static void WriteError(object message)
        {
            Debug.WriteLine(message);
        }
        public static void WriteInformation(string message)
        {
            Debug.WriteLine(message);
        }
        
    }
}