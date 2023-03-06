using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace ConsoleApp7
{
    class Hamming
    {
        public static void Main()
        {
            int q;
            Console.WriteLine("Введите вариант кодирования: \n1)Ввести строку \n2)Ввести биты");
            q = Convert.ToInt32(Console.ReadLine());
            switch (q)
            {
                case 1:
                    Console.WriteLine("Введите слово/строку");
                    string word = Console.ReadLine();
                    HammingCodeStr(word);
                    break;
                case 2:
                    HammingCode();
                    break;
                default:
                    Console.WriteLine("Неверное число");
                    break;
            }          
        }

        static void HammingCodeStr(string line) 
        {
            Dictionary<char, string> LetForSet = new Dictionary<char, string>();
            string str1 = line.Replace(" ", "");
            var newstr = String.Join("", str1.Distinct());
            char[] Letters = newstr.ToCharArray();
            Console.WriteLine();
            int[] a = new int[8 * line.Length];
            string[] codes = new string[Letters.Length];
            for (int i = 0; i <codes.Length; i++)
            {
                int buff = Convert.ToInt32(Letters[i]);
                string code = Convert.ToString(buff, 2);
                codes[i] = code;
                char[] symbols = code.ToCharArray();
                LetForSet.Add(Letters[i], code);
            }
            Console.WriteLine("Вы ввели:");
            for (int i = 0; i < line.Length; i++)
            {
                char buff = line[i];
                a[]
            }


        }
        static void HammingCode()
        {
            Console.WriteLine("Введите количество битов для данных Хэмминга:");
            int n = Convert.ToInt32(Console.ReadLine());
            int[] a = new int[n];

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("Введите бит № " + (n - i) + ":");
                int buff = Convert.ToInt32(Console.ReadLine());
                while(buff>1 || buff < 0)
                {
                    Console.WriteLine("Введите бит заново");
                    buff = Convert.ToInt32(Console.ReadLine());
                }
                a[n - i - 1] = buff;
            }

            Console.WriteLine("Вы ввели:");
            for (int i = 0; i < n; i++)
            {
                Console.Write(a[n - i - 1]);
            }
            Console.WriteLine();

            int[] b = GenerateCode(a);

            Console.WriteLine("Сгенерированный код является:");
            for (int i = 0; i < b.Length; i++)
            {
                Console.Write((b[b.Length - i - 1]));
            }
            Console.WriteLine();

            
            Console.WriteLine("Введите положение бита, которое нужно изменить, чтобы проверить обнаружение ошибок на стороне приемника (0 для отсутствия ошибок):");
            int error = Convert.ToInt32(Console.ReadLine());
            while(error < 0 && error > b.Length)
            {
                Console.WriteLine("Введите ещё раз");
                error = Convert.ToInt32(Console.ReadLine());
            }
            if (error != 0)
            {
                b[error - 1] = (b[error - 1] + 1) % 2;
            }
            Console.WriteLine("Отправленный код:");
            for (int i = 0; i < b.Length; i++)
            {
                Console.Write(b[b.Length - i - 1]);
            }
            Console.WriteLine();
            Receive(b, b.Length - a.Length);
        }
        static int[] GenerateCode(int[] a)
        {
            int[] b;

            
            int i = 0, parity_count = 0, j = 0, k = 0;
            while (i < a.Length)
            {
                

                if (Math.Pow(2, parity_count) == i + parity_count + 1)
                {
                    parity_count++;
                }
                else
                {
                    i++;
                }
            }

            
            b = new int[a.Length + parity_count];

            

            for (i = 1; i <= b.Length; i++)
            {
                if (Math.Pow(2, j) == i)
                {
                    

                    b[i - 1] = 2;
                    j++;
                }
                else
                {
                    b[k + j] = a[k++];
                }
            }
            for (i = 0; i < parity_count; i++)
            {
                

                b[((int)Math.Pow(2, i)) - 1] = GetParity(b, i);
            }
            return b;
        }
        static int GetParity(int[] b, int power)
        {
            int parity = 0;
            for (int i = 0; i < b.Length; i++)
            {
                if (b[i] != 2)
                {
                    

                    int k = i + 1;
                    string s = Convert.ToString(k,2);

                    /

                    int x = ((int.Parse(s)) / ((int)Math.Pow(10, power))) % 10;
                    if (x == 1)
                    {
                        if (b[i] == 1)
                        {
                            parity = (parity + 1) % 2;
                        }
                    }
                }
            }
            return parity;
        }
        static void Receive(int[] a, int parity_count)
        {
            

            int power;
            

            int[] parity = new int[parity_count];
            

            string syndrome=string.Empty;
            

            for (power = 0; power < parity_count; power++)
            {
               

                for (int i = 0; i < a.Length; i++)
                {
                    

                    int k = i + 1;
                    string s = Convert.ToString(k, 2);
                    int bit = ((int.Parse(s)) / ((int)Math.Pow(10, power))) % 10;
                    if (bit == 1)
                    {
                        if (a[i] == 1)
                        {
                            parity[power] = (parity[power] + 1) % 2;
                        }
                    }
                }
                syndrome = parity[power] + syndrome;
            }
            

            int error_location = Convert.ToInt32(syndrome, 2);
            if (error_location != 0)
            {
                Console.WriteLine("Ошибка находится в местоположении " + error_location + ".");
                a[error_location - 1] = (a[error_location - 1] + 1) % 2;
                Console.WriteLine("Исправленный код :");
                for (int i = 0; i < a.Length; i++)
                {
                    Console.Write(a[a.Length - i - 1]);
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("В полученных данных нет ошибки.");
            }

            
            Console.WriteLine("Отправленные исходные данные были: ");
            power = parity_count - 1;
            for (int i = a.Length; i > 0; i--)
            {
                if (Math.Pow(2, power) != i)
                {
                    Console.Write(a[i - 1]);
                }
                else
                {
                    power--;
                }
            }
            Console.WriteLine();
        }




    }
}
