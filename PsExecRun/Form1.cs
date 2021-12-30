using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Net.NetworkInformation;

namespace PsExecRun
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string remoteMachine = textBox1.Text.Trim();
            string batpath = textBox2.Text.Trim();
            //string qwewqeqw = "netsh advfirewall firewall set rule name=" +"Remote Service Management(RPC)"+ " profile=domain new enable=yes";
            string appName = "PsExec.exe";
            string args = string.Format("\\\\{0} -u philip.calugcugan -p jp120193 -d -s -i cmd /c " + batpath + "", remoteMachine);
            // Process.Start(appName, args);

            MyProcess proc = new MyProcess();

            ProcessStartInfo cmd = new ProcessStartInfo();

            cmd.FileName = appName;
            cmd.WindowStyle = ProcessWindowStyle.Normal;
            cmd.Arguments = args;

            proc.StartInfo = cmd;
            proc.Exited += new EventHandler(myProcess_HasExited);
            proc.Start();

            //while (string.IsNullOrEmpty(proc.MainWindowTitle))
            //{
            //    MessageBox.Show("Wait");
            //    System.Threading.Thread.Sleep(100);

                
            //}

            proc.WaitForExit();


            //proc.Stop();
           // proc = Process.Start(cmd);

           // proc.WaitForInputIdle();
          //  proc.WaitForExit();
        }

        class MyProcess : Process
        {
            public void Stop()
            {
                this.CloseMainWindow();
                this.Close();
                OnExited();
            }
        }

        private static void myProcess_HasExited(object sender, System.EventArgs e)
        {
            MessageBox.Show("Done!");
        }

    }
}
