namespace NAS
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 4, 7, 3, 2, 9, 6, 5, 7 };
            List<List<int>> combinations = GenerateCombinations(arr);

            int highestK = 0;
            List<int> highestComb = new List<int>();
            foreach (var combination in combinations)
            {
                if (combination.Count == 0) continue;
                for (int k = 1; k <= combination.Count; k++)
                {
                    var schedule = new List<int[]>();
                    bool success = checkPeriod(k, combination);
                    if (success)
                    {
                        if (k > highestK) { highestK = k; highestComb = combination; }
                        System.Console.WriteLine("k: " + k + " | " + success + " | " + string.Join(", ", combination));
                    }
                }
            }
            System.Console.WriteLine("Highest k: " + highestK + " | " + string.Join(", ", highestComb));
        }

        static List<List<int>> GenerateCombinations(int[] arr)
        {
            List<List<int>> result = new List<List<int>> { new List<int>() };

            foreach (var num in arr)
            {
                int count = result.Count;
                for (int i = 0; i < count; i++)
                {
                    var newCombination = new List<int>(result[i]);
                    newCombination.Add(num);
                    result.Add(newCombination);
                }
            }

            return result;
        }

        static bool checkPeriod(int k, List<int> combination)
        {
            bool success = true;
            for (int i = 0; i < combination.Count; i++)
            {
                if ((combination[i] + i) % k != i) success = false;
            }
            return success;
        }

        static void drawSchedule(List<int[]> schedule)
        {
            //  Show the schedule
            for (int i = 0; i < schedule.Count; i++)
            {
                System.Console.Write(string.Join(", ", schedule[i]) + " | ");
            }
            System.Console.WriteLine("\n--------------------");
            for (int i = 0; i < schedule.Count; i++)
            {
                string space = new String(' ', Math.Abs((int)(schedule[i].Length * 1.5) - 1));
                System.Console.Write(space + (i + 1) + space + " | ");
            }
        }
    }
}