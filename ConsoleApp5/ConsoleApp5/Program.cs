using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ConsoleApp5 {
    class A
    {
        public static void Main()
        {
            A ob = new A();
            ob.Generate("beep boop beer!");
        }

        public void Generate(string line)
        {
            string str = line;
            string str1 = str.Replace(" ", "");
            var newstr = String.Join("", str1.Distinct());
            char[] Letters = newstr.ToCharArray(); //массив уникальных символов
            double[] P = new double[newstr.Length]; //массив частот символов
            int q = 0;
            int size = str1.Length;
            for (int i = 0; i < Letters.Length; i++)
            {
                for (int j = 0; j < str.Length; j++)
                {
                    if (Letters[i] == str[j])
                    {
                        q += 1;
                    }
                }
                double z = (double)q / (double)size;
                P[i] = z;
                q = 0;
            }
            string[] final = new string[str.Length];

            double schet1 = 0;
            double schet2 = 0;

            Sort(P, Letters);
            Fano(0, P.Length, final, P, schet1, schet2);

            for (int i = 0; i < P.Length; i++)
            {

                Console.WriteLine("["+Letters[i]+"] = " + final[i]);

            }

        }

        public void Sort(double[] P, char[] Alpha)
        {
            for (int i = 0; i < P.Length; i++)
            {
                for (int j = 0; j < P.Length - i - 1; j++)
                {
                    if (P[j] < P[j + 1])
                    {
                        char temp2;
                        double temp1 = 0;

                        temp1 = P[j];
                        temp2 = Alpha[j];
                        P[j] = P[j + 1];
                        Alpha[j] = Alpha[j + 1];
                        P[j + 1] = temp1;
                        Alpha[j + 1] = temp2;

                    }
                }
            }


        }
        int m;

        public int Median(int L, int R, double part1, double part2, double[] P)
        {

            part1 = 0;
            for (int i = L; i <= R - 1; i++)
            {
                part1 = part1 + P[i];
            }

            part2 = P[R - 1];
            m = R;
            while (part1 >= part2)
            {
                m = m - 1;
                part1 = part1 - P[m];
                part2 = part2 + P[m];
            }
            return m;


        }

        public void Fano(int L, int R, string[] Res, double[] P, double part1, double part2)
        {
            int n;

            if (L < R)
            {

                n = Median(L, R, part1, part2, P);
                for (int i = L; i <= R; i++)
                {
                    if (i <= n)
                    {
                        Res[i] += Convert.ToByte(0);
                    }
                    else
                    {
                        Res[i] += Convert.ToByte(1);
                    }
                }

                Fano(L, n, Res, P, part1, part2);

                Fano(n + 1, R, Res, P, part1, part2);

            }


        }
    }
}