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

        // int[] arr = { 18228, 17376, 12314, 18369, 103, 4234, 4225, 12061, 19037, 19703, 8931, 4442, 24776, 3091, 18547, 19605, 12653, 7523, 672, 10441, 8815, 10919, 4835, 939, 9021 };

        int[] arr = { 4, 7, 3, 2, 9, 6, 5, 7 };
        int[] arr1 = { 1, 2, 2, 3, 3, 4, 4, 4, 8, 9 };
        int[] arr2 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };

        System.Console.WriteLine(getK(arr));
        System.Console.WriteLine(getK(arr1));
        System.Console.WriteLine(getK(arr2));
        //         int[] arr = {
        //     74726, 101403, 2844, 222330, 175764, 221323, 219383, 58331, 84621, 107334,
        //     31331, 194516, 110602, 74116, 73547, 104636, 60181, 176472, 151412, 118494,
        //     2426, 33893, 183576, 77742, 115845, 57434, 36664, 178879, 105860, 22112,
        //     109429, 171304, 168689, 177083, 86689, 182858, 34897, 141285, 341, 210845,
        //     92792, 175156, 156041, 189119, 115773, 171938, 192234, 11504, 175369,
        //     163814, 184716, 159966, 199380, 156493, 47067, 78947, 579, 141298, 115356,
        //     165895, 202388, 129192, 71607, 169673, 172891, 16922, 24691, 59146, 62355,
        //     75617, 194626, 208221, 24920, 172040, 127918, 26473, 60732, 153696, 79556,
        //     122124, 2574, 45747, 195427, 144801, 15045, 214006, 28756, 34761, 61105,
        //     119588, 92982, 48122, 100436, 12175, 7480, 174976, 83087, 163156, 8653,
        //     108770, 132967, 177748, 146381, 166671, 106149, 51140, 106757, 14010,
        //     23232, 61687, 22290, 213852, 175106, 133938, 121799, 114212, 58313, 1274,
        //     118617, 37870, 142107, 45835, 156949, 134729, 60818, 156806, 195535, 4539,
        //     160177, 96258, 207815, 19073, 151632, 194349, 149002, 214243, 221496,
        //     108061, 201883, 135924, 12954, 88881, 135132, 189486, 7252, 89024, 143388,
        //     110323, 40202, 11056, 181846, 137410, 6419, 80603, 158961, 160630, 24795,
        //     218580, 57030, 151827, 209486, 11128, 82973, 137747, 158964, 183405,
        //     222564, 78549, 9585, 182488, 56327, 61601, 30602, 95377, 108033, 161386,
        //     54273, 163828, 90644, 92647, 116325, 132570, 128305, 155441, 175042,
        //     131747, 58509, 166894, 140044, 189849, 111059, 9986, 164176, 144036, 29157,
        //     11635, 196904, 54886, 211308, 21047, 5372, 145429, 109458, 174376, 217132,
        //     174754, 68537, 89832, 165362, 213427, 176436, 41831, 97955, 203648, 215403,
        //     223623, 46092, 194627, 165857, 90605, 124064, 157463, 63282, 144738, 176288
        // };
        // System.Console.WriteLine(getK(arr));
        // for (int i = 1; i <= 2; i++)
        // {
        //     int n = int.Parse(input[i * 2 - 1]);
        //     int[] arr = input[i * 2].Split(' ').Select(int.Parse).ToArray();
        //     System.Console.WriteLine("n: " + n);
        //     output[i - 1] = getK(arr);
        //     highestK = 0;
        // }

        File.WriteAllLines("output.txt", output.Select(x => x.ToString()).ToArray());
        watch.Stop();
        TimeSpan ts = watch.Elapsed;

        // Format and display the TimeSpan value.
        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
        Console.WriteLine("RunTime " + elapsedTime);
        // System.Console.WriteLine("Highest k: " + highestK);
    }

    static int getK(int[] arr)
    {
        for (int k = arr.Length; k >= highestK; k--)
        {
            if (k == 0) k++;
            int[] currentCombination = new int[k];
            // if (k % 2 == 0) { arr = stripEven(arr); } else { arr = stripOdd(arr); }
            // System.Console.WriteLine("array " + string.Join(", ", arr));
            GenerateCombinations(arr, k, 0, currentCombination, 0);
            // System.Console.WriteLine("k: " + k);
        }
        return highestK;
    }
    static int[] stripOdd(int[] arr)
    {
        List<int> list = new List<int>();
        foreach (int item in arr)
        {
            if (item % 2 == 0) list.Add(item);
        }
        return list.ToArray();
    }
    static int[] stripEven(int[] arr)
    {
        List<int> list = new List<int>();
        foreach (int item in arr)
        {
            if (item % 2 != 0) list.Add(item);
        }
        return list.ToArray();
    }
    static void GenerateCombinations(int[] arr, int k, int startIndex, int[] currentCombination, int currentIndex)
    {
        if (k <= highestK) { return; }
        if (!currentCombination.Contains(0))
        {

            // System.Console.WriteLine(checkPeriod(k, currentCombination));
            if (checkPeriod(k, currentCombination)) { highestK = k; System.Console.WriteLine("k: " + k + " - " + string.Join(", ", currentCombination)); }
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
