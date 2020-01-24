using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Code_Testing
{
    class Program
    {
        static void Reset()
        {
            //HourCbox.Text = "0";
            //MinuteCbox.Text = "0";
            //SecondCbox.Text = "0";
            //TimerText.Text = "00:00:00";
            Console.WriteLine("Done");
        }

        static void Main(string[] args)
        {
            int Hours = 24;
            int Minutes = 1;
            int Seconds = 10;

            //string Value = HourCbox.SelectedValue.ToString();
            //int.TryParse(Value, out Hours);
            //Value = MinuteCbox.SelectedValue.ToString();
            //int.TryParse(Value, out Minutes);
            //Value = SecondCbox.SelectedValue.ToString();
            //int.TryParse(Value, out Seconds);

            while (Hours != 0 && Minutes != 0 && Seconds != 0)
            {
                Console.WriteLine(Hours + ":" + Minutes + ":" + Seconds);
                Thread.Sleep(300);
                Console.Clear();
                Seconds--;
                if (Minutes != 0 && Seconds == 0)
                {
                    Minutes--;
                    Seconds = 60;

                }
                if (Hours != 0 && Minutes == 0)
                {
                    Hours--;
                    Minutes = 59;
                }
            }
            Reset();
            new Thread(() =>
            {
            }).Start();


        }
    }
}
