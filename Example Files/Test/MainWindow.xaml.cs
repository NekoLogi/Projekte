using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Forms;
using System.IO.Compression;

namespace Test
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            while (true)
            {
                System.Diagnostics.Process.Start("Start.bat");
                Thread.Sleep(14400000);
                SendKeys.SendWait("/say [Packt euren Mist, Server startet in 5 Minuten neu!]");
                SendKeys.SendWait("{ENTER}");
                Thread.Sleep(240000);
                SendKeys.SendWait("/say [1 Minute bis Neustart]");
                SendKeys.SendWait("{ENTER}");
                Thread.Sleep(50000);
                SendKeys.SendWait("/say [10 Sekunden bis Neustart]");
                SendKeys.SendWait("{ENTER}");
                Thread.Sleep(10000);
                SendKeys.SendWait("/save-all");
                SendKeys.SendWait("{ENTER}");
                Thread.Sleep(500);
                SendKeys.SendWait("stop");
                SendKeys.SendWait("{ENTER}");
                Thread.Sleep(7000);

                DateTime localDate = DateTime.Now;
                string time = localDate.ToString();
                time = time.Replace('.', '-');
                time = time.Replace(' ', '_');
                time = time.Replace(':', '-');

                string startPath = @"world";
                string zipPath = @"Backup" + time + ".zip";

                ZipFile.CreateFromDirectory(startPath, zipPath);
                Thread.Sleep(10000);
            }




        }


    }
}
