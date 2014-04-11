using System;
using System.Windows.Forms;

namespace ToyShopDataLib
{
    public class ProccessMesenger
    {
        public static event Action<string> MessageSanded;

        protected static void OnMessageSanded(string message)
        {
            var handler = MessageSanded;
            if (handler != null) handler(message);
        }

        public static void Write(string format, params object[] args)
        {
            var message = string.Format(format, args);
            OnMessageSanded(message);
        }

        public static void AttachForm(Form form)
        {
            Action<string> handler = msg => form.BeginInvoke((Action)(() =>
            {
                form.Text = msg;
                form.Refresh();
            }));
            
            MessageSanded += handler;
            form.Closed += (s, e) => MessageSanded -= handler;
        }

        

    }
}