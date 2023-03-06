using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Rec(int[] mass, int n, string s)
        {
            if (n == mass.Length)
            {
                Console.WriteLine("{ " + s + "}" + "\n");
                return;
            }
            Rec(mass, n + 1, s);
            s = s + Convert.ToString(mass[n]) + " ";
            Rec(mass, n + 1, s);
        }

        static void Main(string[] args)
        {
            Console.Write("Введите размер множества: ");
            string n1 = Console.ReadLine();
            int n = Convert.ToInt32(n1);
            int m = n;
            int[] mnoj = new int[n];
            int q = 1;
            for (int i = 0; i < n; i++)
            {
                mnoj[i] = q;
                q += 1;
            }
            Rec(mnoj, 0, "");
        }
      
    }
}
