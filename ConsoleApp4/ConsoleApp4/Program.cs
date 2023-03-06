using System;

namespace ConsoleApp4
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
            int d, u, v;
            int u1, u2, v1, v2;
            int q, r;
            if (b == 0)
            {
                d = a;
                u = 1;
                v = 0;
                Console.WriteLine("d = {0} ; u = {1} ; v ={2}",d,u,v);
            }
            u2 = 1;u1 = 0;v2 = 0;v1 = 1;
            while (b > 0)
            {
                q = a / b;
                r = a - q * b;
                u = u2 - q * u1;
                v = v2 - q * v1;
                a = b;
                b = r;
                u2 = u1;
                u1 = u;
                v2 = v1;
                v1 = v;
            }
            d = a;
            u = u2;
            v = v2;
            Console.WriteLine("d = {0} ; u = {1} ; v ={2}", d, u, v);
        }
    }
}
