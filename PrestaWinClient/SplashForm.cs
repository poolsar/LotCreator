using System;
using System.Windows.Forms;
using Utils;

namespace PrestaWinClient
{
    public partial class SplashForm : Form
    {
        public SplashForm()
        {
            InitializeComponent();

            
            Messenger.Writed += Messenger_Writed;
        }


        void Messenger_Writed(string message)
        {
            WriteOutputAsync(message);
        }


        public  void WriteOutputAsync(string msg)
        {
            if ( !Visible) return;

            if (!IsHandleCreated)
                CreateControl();

            Action act = () => WriteOutput(msg);

            BeginInvoke(act);
        }


        public void WriteOutput(string msg)
        { 

            textBox1.Text += Environment.NewLine + msg;
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();

        }
    }

}
