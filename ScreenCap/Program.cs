using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using xClasses;
using System.Text.RegularExpressions;
using System.Threading;

namespace ScreenCap
{
    class Program
    {
        static void Main(string[] args)
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            etc123 qwe = new etc123();


            bool sw = false;
            string _myIp = qwe.GetLocalIPv4();
            string path = DateTime.Now.ToString("yyyyMMdd") + "(" + _myIp + ")" + ".png";

            string ss = Regex.Replace(_myIp, @"\.+", "_");

            Thread.Sleep(300);

            try
            {
                Rectangle bounds = Screen.GetBounds(Point.Empty);
                using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                    }
                    bitmap.Save(@"C:\\sc\\" + path, ImageFormat.Png);
                }
                sw = true;
            }
            catch (Exception ex)
            {
                insert_logs("failed screenshot " + path);
            }
           
            if (sw)
            {
                insert_logs("success screenshot " + path);
            }
        }

        static void insert_logs(string logs)
        {

            try
            {
                const string location = @"scl";

                if (!File.Exists(location))
                {
                    var createText = "New Activities Logs" + Environment.NewLine;
                    File.WriteAllText(location, createText);
                }
                var appendLogs = "Activities Logs: " + logs + " " + DateTime.Now + Environment.NewLine;
                File.AppendAllText(location, appendLogs);
            }
            catch (Exception ex)
            {
                const string location = @"scl";
                if (!File.Exists(location))
                {
                    TextWriter file = File.CreateText(@"C:\ponLogs");
                    var createText = "New Activities Logs" + Environment.NewLine;

                    File.WriteAllText(location, createText);

                }
                var appendLogs = ex.Message + logs + DateTime.Now + Environment.NewLine;
                File.AppendAllText(location, appendLogs);

            }
        }
    }
}
