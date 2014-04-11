using System;

namespace Utils
{
    public class Messenger
    {
        public static void Write(string message, params object[] args)
        {
            message = string.Format(message, args);
            OnWrited(message);
        }
    
        public static event Action<string> Writed;

        protected static void OnWrited(string message)
        {
            Action<string> handler = Writed;
            if (handler != null) handler(message);
        }
    }
}