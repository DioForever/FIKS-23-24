using System;
using System.Collections.Generic;
using System.Linq;

public class Combinations
{
    static int highestK = 0;
    static void Main()
    {
        var watch = new System.Diagnostics.Stopwatch();

        watch.Start();
        string[] input = File.ReadAllText("inputCorrect.txt").Split('\n');
        int t = int.Parse(input[0]);
        int[] output = new int[t];
        int[] arr = { 18228, 17376, 12314, 18369, 103, 4234, 4225, 12061, 19037, 19703, 8931, 4442, 24776, 3091, 18547, 19605, 12653, 7523, 672, 10441, 8815, 10919, 4835, 939, 9021 };
        // int[] arr = { 1, 2, 2, 3, 3, 4, 4, 4, 8, 9 };


        // int highestK = 2; // Change this value to your desired highest k
        // int k = 3;

        for (int k = (arr.Length / 100); k <= arr.Length; k++)
        {
            if (k == 0) k++;
            int[] currentCombination = new int[k];
            GenerateCombinations(arr, k, 0, currentCombination, 0);
            // System.Console.WriteLine("k: " + k);
        }
        System.Console.WriteLine("Highest k: " + highestK);
    }
    static void GenerateCombinations(int[] arr, int k, int startIndex, int[] currentCombination, int currentIndex)
    {
        if (k <= highestK) { System.Console.WriteLine("skipped"); return; }
        if (!currentCombination.Contains(0))
        {

            // System.Console.WriteLine(checkPeriod(k, currentCombination));
            if (checkPeriod(k, currentCombination)) { System.Console.WriteLine("Current combination " + string.Join(", ", currentCombination)); highestK = k; System.Console.WriteLine("success: " + highestK); }
        }
        if (currentIndex == k)
        {
            // A combination of size k has been formed, so print it
            // Console.WriteLine("Current combination " + string.Join(", ", currentCombination));
            return;
        }

        for (int i = startIndex; i < arr.Length; i++)
        {
            if (k <= highestK) return;
            if (CountOccurrences(arr, arr[i]) > CountOccurrences(currentCombination, arr[i]))
            {
                currentCombination[currentIndex] = arr[i];

                // Recursively generate combinations with the remaining elements
                GenerateCombinations(arr, k, i + 1, currentCombination, currentIndex + 1);
            }
        }
    }

    static int CountOccurrences(int[] arr, int value)
    {
        int count = 0;
        foreach (int item in arr)
        {
            if (item == value)
            {
                count++;
            }
        }
        return count;
    }


    static bool checkPeriod(int k, int[] combination)
    {
        bool success = true;
        for (int i = 0; i < combination.Length; i++)
        {
            if ((combination[i] + i) % k != i) success = false;
        }
        return success;
    }


}
