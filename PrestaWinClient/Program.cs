using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace PrestaWinClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += Application_ThreadException;
            Application.ThreadExit += Application_ThreadExit;
            Application.ApplicationExit += Application_ApplicationExit;
            


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new ManageProductsForm());

            Application.Run(new TestBegemotForm());
        }

        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            AbortAsyncThread();
        }

        static void Application_ThreadExit(object sender, EventArgs e)
        {
            AbortAsyncThread();
        }

        private static void AbortAsyncThread()
        {
            ThreadController.AbortAllThreads();
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            AbortAsyncThread();
        }
    }

    public class ThreadController
    {
        static List<Thread> threads=new List<Thread>();
        public static void AddThread(Thread t)
        {
            threads.Add(t);
        }

        private static bool isProcessed = false;
        public static void AbortAllThreads()
        {
            if (isProcessed) return;

            foreach (var thread in threads)
            {
                try
                {
                    if (thread != null && thread.ThreadState != ThreadState.Stopped)
                    {
                        thread.Abort();
                    }
                }
                catch (Exception)
                {
                    
                    //throw;
                }
                
            }

            isProcessed = true;
        }
    }
}
