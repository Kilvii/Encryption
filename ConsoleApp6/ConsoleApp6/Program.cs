using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp6
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            string input;
            Console.WriteLine("Введите строку");
            input = Console.ReadLine();
            Console.WriteLine("Введенная строка: " + input);

            HuffmanTree huffmanTree = new HuffmanTree();
            Console.ReadLine();
            
            huffmanTree.Build(input);


            
            string enterN2 = input;

           
            enterN2 = new string(enterN2.Distinct().ToArray());
            Console.WriteLine("Все символы без повторений " + enterN2);
            int[] counter = new int[input.Length];
            Console.WriteLine("Частота символов ");
        
            for (int i = 0; i < input.Length; i++)
                counter[i] = 0;
            for (int i = 0; i < input.Length; i++)
                for (int j = 0; j < enterN2.Length; j++)
                    if (input[i] == enterN2[j])
                    {
                        counter[j] += 1;
                    }
            for (int i = 0; i < input.Length; i++)
                if (counter[i] != 0)
                    Console.WriteLine(enterN2[i] + ":" + counter[i]);

            
            int tmp = 0;
            char[] enterN3 = enterN2.ToCharArray(); ;
            char[] МУСОР = new char[enterN2.Length];
            for (int i = 0; i < enterN2.Length - 1; ++i) 
            {
                for (int j = 0; j < enterN2.Length - 1; ++j) 
                {
                    if (counter[j] < counter[j + 1])
                    {
                        tmp = counter[j];
                        МУСОР[j] = enterN3[j];
                        counter[j] = counter[j + 1];
                        enterN3[j] = enterN3[j + 1];
                        counter[j + 1] = tmp;
                        enterN3[j + 1] = МУСОР[j];
                    }
                }
            }
            Console.WriteLine();

            
            Console.WriteLine("Отсортированый по убыванию массив: ");
            for (int i = 0; i < input.Length; i++)
                if (counter[i] != 0)
                    Console.WriteLine(enterN3[i] + ":" + counter[i]);

            
            int[] enterNumbers = new int[enterN2.Length];
            string[] enterNumbers2 = new string[enterN2.Length];
            for (int i = 0; i < enterN2.Length; i++)
            {
                enterNumbers[i] = Convert.ToInt32(enterN3[i]);

            }

            
            BitArray encoded = huffmanTree.Encode(input);


            Console.Write("Закодированная строка: ");
            foreach (bool bit in encoded)
            {
                Console.Write((bit ? 1 : 0) + "");
            }
            Console.WriteLine();
            Console.ReadLine();
        }

        public class HuffmanTree
        {
            private List<Node> nodes = new List<Node>();
            public Node Root { get; set; }
            public Dictionary<char, int> Frequencies = new Dictionary<char, int>();

            public void Build(string source)
            {
                for (int i = 0; i < source.Length; i++)
                {
                    if (!Frequencies.ContainsKey(source[i]))
                    {
                        Frequencies.Add(source[i], 0);
                    }

                    Frequencies[source[i]]++;
                }

                foreach (KeyValuePair<char, int> symbol in Frequencies)
                {
                    nodes.Add(new Node() { Symbol = symbol.Key, Frequency = symbol.Value });
                }

                

                while (nodes.Count > 1)
                {
                    List<Node> orderedNodes = nodes.OrderBy(node => node.Frequency).ToList<Node>();

                    if (orderedNodes.Count >= 2)
                    {
                        List<Node> taken = orderedNodes.Take(2).ToList<Node>();
                        Node parent = new Node()
                        {
                            Symbol = '*',
                            Frequency = taken[0].Frequency + taken[1].Frequency,
                            Left = taken[0],
                            Right = taken[1]
                        };

                        nodes.Remove(taken[0]);
                        nodes.Remove(taken[1]);
                        nodes.Add(parent);
                    }

                    this.Root = nodes.FirstOrDefault();

                }


            }

            public BitArray Encode(string source)
            {
                List<bool> encodedSource = new List<bool>();
                for (int i = 0; i < source.Length; i++)
                {
                    List<bool> encodedSymbol = this.Root.Traverse(source[i], new List<bool>());
                    encodedSource.AddRange(encodedSymbol);
                }
                BitArray bits = new BitArray(encodedSource.ToArray());
                return bits;
            }

            public bool IsLeaf(Node node)
            {
                return (node.Left == null && node.Right == null);
            }
            
            public void Counter()
            {
                foreach (KeyValuePair<char, int> pair in Frequencies.OrderBy(pair => pair.Value))
                    Console.WriteLine(string.Format("{0}-{1}", pair.Key, pair.Value));
            }

        }
        public class Node
        {
            public char Symbol { get; set; }
            public int Frequency { get; set; }
            public Node Right { get; set; }
            public Node Left { get; set; }

            public List<bool> Traverse(char symbol, List<bool> data)
            {
                // Leaf
                if (Right == null && Left == null)
                {
                    if (symbol.Equals(this.Symbol))
                    {
                        return data;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    List<bool> left = null;
                    List<bool> right = null;

                    if (Left != null)
                    {
                        List<bool> leftPath = new List<bool>();
                        leftPath.AddRange(data);
                        leftPath.Add(false);

                        left = Left.Traverse(symbol, leftPath);
                    }

                    if (Right != null)
                    {
                        List<bool> rightPath = new List<bool>();
                        rightPath.AddRange(data);
                        rightPath.Add(true);
                        right = Right.Traverse(symbol, rightPath);
                    }

                    if (left != null)
                    {
                        return left;
                    }
                    else
                    {
                        return right;
                    }
                }
            }
        }
    }
}
