using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Würfel
{
    class Program
    {
        static int Dice()
        {
            Random rnd = new Random();
            int number = rnd.Next(1, 7);
            return number;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Dice: \t\t1 \nGuess Game: \t2");
            string choice = Console.ReadLine();
            Console.Clear();

            if (choice == "1")
            {
                while (true)
                {
                    int number = Dice();
                    Console.WriteLine(number);
                    Console.ReadLine();
                }
            }
            if (choice == "2")
            {
                while (true)
                {
                    int number = Dice();
                    Console.WriteLine("guess between 1 and 6:\n");
                    string convert = Console.ReadLine();
                    int.TryParse(convert, out int guess);
                    if (guess == number)
                    {
                        Console.WriteLine("Right, number was " + number);
                    }
                    else
                        Console.WriteLine("Wrong, number was " + number);
                    Console.WriteLine("Press \"Enter\" to restart.");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }
    }
}
