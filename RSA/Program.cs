using System;
using System.Collections.Generic;
using System.Numerics;

namespace RSA
{
    class Program
    {
        static void Main(string[] args)
        {
            //Вариант 6
            long p = 199;
            long q = 337;
            Console.WriteLine("Введите строку: ");
            string line = Console.ReadLine();
            long n = p * q;
            long m = (p - 1) * (q - 1);
            long d = Calculate_d(m);
            long e_ = Calculate_e(d, m);

            List<string> result = RSA_Encode(line, e_, n);
          
            string buff = "";
            for (int i = 0; i < result.Count; i++)
            {
                buff += result[i];
            }
            Console.WriteLine("Зашифрованное сообщение:{0}",buff);

            List<string> input = new List<string>();
            input = result;
            string result2 = RSA_Decode(input,line, d, n);
            Console.WriteLine("Расшифрованное сообщение:{0}",result2);

        }
        static private bool IsSimple(long n)
        {
            if (n < 2)
                return false;

            if (n == 2)
                return true;

            for (long i = 2; i < n; i++)
                if (n % i == 0)
                    return false;

            return true;
        }
        static private long Calculate_d(long m)
        {
            long d = m - 1;

            for (long i = 2; i <= m; i++)
                if ((m % i == 0) && (d % i == 0)) //если имеют общие делители
                {
                    d--;
                    i = 1;
                }

            return d;
        }
        static private long Calculate_e(long d,long m)
        {
            long e = 10;

            while (true)
            {
                if ((e * d) % m == 1)
                    break;
                else
                    e++;
            }

            return e;
        }

        static private List<string> RSA_Encode(string s,long e,long n)
        {
            List<string> result = new List<string>();

            char[] symbols = s.ToCharArray();

            BigInteger bi;

            for (int i = 0; i < s.Length; i++)
            {
                int index = Array.IndexOf(symbols, s[i]);

                bi = new BigInteger(index);
                bi = BigInteger.Pow(bi, (int)e);

                BigInteger n_ = new BigInteger((int)n);

                bi = bi % n_;

                result.Add(bi.ToString());
            }

            return result;
        }

        static private string RSA_Decode(List<string> input,string s,long d,long n)
        {
            string result = "";
            char[] symbols = s.ToCharArray();
            BigInteger bi;

            foreach (string item in input)
            {
                bi = new BigInteger(Convert.ToDouble(item));
                bi = BigInteger.Pow(bi, (int)d);

                BigInteger n_ = new BigInteger((int)n);

                bi = bi % n_;

                int index = Convert.ToInt32(bi.ToString());

                result += symbols[index].ToString();
            }

            return result;
        }
    }      
}
