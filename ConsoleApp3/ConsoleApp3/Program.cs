using System;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите натуральное число а: ");
            string s1 = Console.ReadLine();
            int a = Convert.ToInt32(s1);
            while (a < 0)
            {
                Console.WriteLine("Введите заново: ");
                s1 = Console.ReadLine();
                a = Convert.ToInt32(s1);
            }
            Console.WriteLine("Введите натуральное число b (b <= a): ");
            string s2 = Console.ReadLine();
            int b = Convert.ToInt32(s2);
            while (b < 0 || b > a)
            {
                Console.WriteLine("Введите заново: ");
                s2 = Console.ReadLine();
                b = Convert.ToInt32(s2);
            }
            while (b != 0)
            {
                b = a % (a = b);
            }
            Console.WriteLine("(a,b) = ({0},{1})",a,b);
        }
    }
}
