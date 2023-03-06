using System;

namespace DES
{
    class Program
    {
        private const int sizeOfBlock = 128;
        private const int sizeOfChar = 16;
        private const int shiftKey = 2;
        private const int quantityOfRounds = 16;
        static string[] Blocks;
        static void Main(string[] args)
        {
            Console.WriteLine("Введите строку: ");
            string line = Console.ReadLine();
            line = StringToRightLength(line);
            CutStringIntoBlocks(line);
            
        }
        static private string StringToRightLength(string input)
        {
            while (((input.Length * sizeOfChar) % sizeOfBlock) != 0)
                input += "#";

            return input;
        }
        static private void CutStringIntoBlocks(string input)
        {
            Blocks = new string[(input.Length * sizeOfChar) / sizeOfBlock];

            int lengthOfBlock = input.Length / Blocks.Length;

            for (int i = 0; i < Blocks.Length; i++)
            {
                Blocks[i] = input.Substring(i * lengthOfBlock, lengthOfBlock);
                Blocks[i] = StringToBinaryFormat(Blocks[i]);
            }
        }
        static private void CutBinaryStringIntoBlocks(string input)
        {
            Blocks = new string[input.Length / sizeOfBlock];

            int lengthOfBlock = input.Length / Blocks.Length;

            for (int i = 0; i < Blocks.Length; i++)
                Blocks[i] = input.Substring(i * lengthOfBlock, lengthOfBlock);
        }
        static private string StringToBinaryFormat(string input)
        {
            string output = "";

            for (int i = 0; i < input.Length; i++)
            {
                string char_binary = Convert.ToString(input[i], 2);

                while (char_binary.Length < sizeOfChar)
                    char_binary = "0" + char_binary;

                output += char_binary;
            }

            return output;
        }
        static private string CorrectKeyWord(string input, int lengthKey)
        {
            if (input.Length > lengthKey)
                input = input.Substring(0, lengthKey);
            else
                while (input.Length < lengthKey)
                    input = "0" + input;

            return input;
        }
        static private string EncodeDES_One_Round(string input, string key)
        {
            string L = input.Substring(0, input.Length / 2);
            string R = input.Substring(input.Length / 2, input.Length / 2);

            return (R + XOR(L, f(R, key)));
        }

        static private string DecodeDES_One_Round(string input, string key)
    {
        string L = input.Substring(0, input.Length / 2);
        string R = input.Substring(input.Length / 2, input.Length / 2);

        return (XOR(f(L, key), R) + L);
    }
    static private string XOR(string s1, string s2)
    {
        string result = "";

        for (int i = 0; i < s1.Length; i++)
        {
            bool a = Convert.ToBoolean(Convert.ToInt32(s1[i].ToString()));
            bool b = Convert.ToBoolean(Convert.ToInt32(s2[i].ToString()));

            if (a ^ b)
                result += "1";
            else
                result += "0";
        }
        return result;
    }
    static private string f(string s1, string s2)
    {
        return XOR(s1, s2);
    }
    static private string KeyToNextRound(string key)
    {
        for (int i = 0; i < shiftKey; i++)
        {
            key = key[key.Length - 1] + key;
            key = key.Remove(key.Length - 1);
        }

        return key;
    }
    static private string KeyToPrevRound(string key)
    {
        for (int i = 0; i < shiftKey; i++)
        {
            key = key + key[0];
            key = key.Remove(0, 1);
        }

        return key;
    }
    static private string StringFromBinaryToNormalFormat(string input)
    {
        string output = "";

        while (input.Length > 0)
        {
            string char_binary = input.Substring(0, sizeOfChar);
            input = input.Remove(0, sizeOfChar);

            int a = 0;
            int degree = char_binary.Length - 1;

            foreach (char c in char_binary)
                a += Convert.ToInt32(c.ToString()) * (int)Math.Pow(2, degree--);

            output += ((char)a).ToString();
        }

        return output;
    }

}

