using System;
using System.Threading;
using System.Windows.Forms;

namespace PrestaWinClient
{
    public class Splash
    {
        public static void Show()
        {
            Thread t = new Thread(ShowSplash);
            ThreadController.AddThread(t);
            t.Start();
        }

        public static void Close()
        {
            if (form == null) return;
            form.BeginInvoke((Action)CloseSplash);
        }

        public static void WriteLine(string msg, params object[] args)
        {
            msg = string.Format(msg, args);
            WriteLine(msg);
        }

        public static void WriteLine(string msg)
        {
            if (form == null || !form.Visible) return;

            if (!form.IsHandleCreated)
                   form.CreateControl();
            
            Action act = () => form.WriteOutput(msg);
            
            form.BeginInvoke(act);
        }


        static void CloseSplash()
        {
            if (form == null) return;
            form.DialogResult = DialogResult.OK;
        }

        private static SplashForm form;

        private static void ShowSplash()
        {
            form = new SplashForm();
            form.ShowDialog();
        }
    }
}