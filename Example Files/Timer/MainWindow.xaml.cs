using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Timer
{
    public partial class MainWindow : Window
    {
        void Reset()
        {
            HourCbox.Dispatcher.Invoke(() => {
                HourCbox.Text = "0";
            });
            MinuteCbox.Dispatcher.Invoke(() => {
                MinuteCbox.Text = "0";
            });
            SecondCbox.Dispatcher.Invoke(() => {
                SecondCbox.Text = "0";

            });
            TimerText.Dispatcher.Invoke(() =>
            {
                TimerText.Text = "00:00:00";
            });

        }
        void Counter()
        {
            int hours = 0;
            int minutes = 0;
            int seconds = 0;
            string h;
            string m;
            string s;
            string Value = "";

            HourCbox.Dispatcher.Invoke(() => {
                Value = HourCbox.SelectedValue.ToString();
            });
            int.TryParse(Value, out hours);
            MinuteCbox.Dispatcher.Invoke(() => {
                Value = MinuteCbox.SelectedValue.ToString();
            });
            int.TryParse(Value, out minutes);
            SecondCbox.Dispatcher.Invoke(() => {
                Value = SecondCbox.SelectedValue.ToString();

            });
            int.TryParse(Value, out seconds);

            while (hours != 0 || minutes != 0 || seconds != 0)
            {
                if (hours != 0 && minutes == 0)
                {
                    hours--;
                    minutes = 60;
                }
                if (minutes != 0 && seconds == 0)
                {
                    minutes--;
                    seconds = 60;

                }
                h = hours.ToString();
                m = minutes.ToString();
                s = seconds.ToString();
                TimerText.Dispatcher.Invoke(() =>
                {
                    TimerText.Text = h + ":" + m + ":" + s;
                });
                Thread.Sleep(1000);
                seconds--;

            }

            Reset();
        }
        public MainWindow()
        {
            InitializeComponent();

            for (int i = 1; i < 25; i++) {
                HourCbox.Items.Add(i);
            }
            for (int i = 1; i < 61; i++) {
                MinuteCbox.Items.Add(i);
                SecondCbox.Items.Add(i);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                Counter();
            });

        }

    }
}
