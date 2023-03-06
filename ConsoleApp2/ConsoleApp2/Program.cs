using System;
using System.Numerics;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            BigInteger y = new BigInteger();
            Console.Write("Введите нечётное целое число:");
            string str = Console.ReadLine();
            int n = Convert.ToInt32(str);
            while (n % 2 == 0 || n < 3)
            {
                Console.WriteLine("Here we go again..");
                str = Console.ReadLine();
                n = Convert.ToInt32(str);
            }
            Console.Write("Введите параметр безопасности:");
            string str1 = Console.ReadLine();
            int t = Convert.ToInt32(str1);
            while (t < 1)
            {
                Console.WriteLine("Here we go again..");
                str1 = Console.ReadLine();
                t = Convert.ToInt32(str1);
            }
            int r, a, s = 0;
            bool check = false;
            r = n - 1;
            while (r % 2 == 0)
            {
                r /= 2;
                s += 1;
            }
            //r = (long)((n - 1) / Math.Pow(2, s));
            //while (r % 2 == 0)
            //{
            //    s += 1;
            //    r = (long)((n - 1) / Math.Pow(2, s));
            //}

            if (n - 1 == Math.Pow(2, s) * r)
            {
                check = true;
            }
            Console.WriteLine("Проверка: {0}", check);//
            Random rnd = new Random();

            for (int i = 1; i < t; i++)
            {
                a = rnd.Next(2, n - 2);
                //y = (long)(Math.Pow(a, r) % n);
                y = BigInteger.ModPow(a, r, n);
                if (y == 1 || y == n - 1)
                {
                    continue;
                }
                for (int j = 0; j < n - 1; j++)
                {
                    //y = (long)(Math.Pow(y, 2) % n);
                    y = BigInteger.ModPow(y, 2, n);
                    if (y == 1)
                    {
                        Console.WriteLine("Составное");
                        return;
                    }
                    if (y == n - 1)
                    {
                        break;
                    }
                }
                if (y != n - 1)
                {
                    Console.WriteLine("Составное");
                    return;
                }
            }
            Console.WriteLine("Простое");
        }
    }
}
