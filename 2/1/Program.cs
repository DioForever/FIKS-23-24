namespace configuration
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    class Program
    {
        // Overall time complexity: O(n * 2^m )
        static void Main(string[] args)
        {
            var (scriptAffects, m, n) = ParseInput("input.txt");
            List<string> validChain = ValidChains(m, n, scriptAffects);
            Console.WriteLine(validChain.Count);
            if (validChain.Count > 0) Console.WriteLine(validChain[0]);
            File.WriteAllLines("output.txt", validChain);
        }

        static (Dictionary<int, List<int>>, int, int) ParseInput(string inputFileName)
        {
            string[] input = File.ReadAllLines(inputFileName);
            Dictionary<int, List<int>> scriptAffects = new Dictionary<int, List<int>>();
            if (input.Length == 0) return (scriptAffects, 0, 0);

            string[] mn = input[0].Split(' ');
            int m = int.TryParse(mn[0], out m) ? m : 0;
            int n = int.TryParse(mn[1], out n) ? n : 0;

            for (int j = 0; j < m; j++)
            {
                int[] scriptsAffected = Array.ConvertAll(input[j + 1].Split(' '), int.Parse).Skip(1).Select(x => x - 1).ToArray();
                scriptAffects.Add(j, scriptsAffected.ToList());
            }

            return (scriptAffects, m, n);
        }

        // Time complexity: O(2^m)
        public static List<string> ValidChains(int m, int n, Dictionary<int, List<int>> scriptAffects)
        {
            List<string> validChains = new List<string>();

            for (int i = 0; i < Math.Pow(2, m); i++)
            {
                string chain = Convert.ToString(i, 2).PadLeft(m, '0');

                if (CheckValidity(chain, n, scriptAffects))
                {
                    validChains.Add(chain);
                }

            }
            return validChains;
        }

        // Time complexity: O(n)
        public static bool CheckValidity(string chain, int n, Dictionary<int, List<int>> scriptAffects)
        {
            var scriptSum = new int[n];

            for (int i = 0; i < chain.Length; i++)
            {
                if (chain[i] == '1')
                {
                    foreach (int script in scriptAffects[i])
                    {
                        scriptSum[script]++;
                    }
                }
            }
            for (int j = 0; j < scriptSum.Length; j++)
            {
                if (scriptSum[j] % 2 == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }

}


