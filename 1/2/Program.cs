using System;
using System.Collections.Generic;
using System.Linq;

public class NAS
{
    static int highestK = 0;
    static void Main()
    {
        var watch = new System.Diagnostics.Stopwatch();

        watch.Start();
        string[] input = File.ReadAllText("input.txt").Split('\n');
        int t = int.Parse(input[0]);
        int[] output = new int[t];

        for (int i = 1; i <= t; i++)
        {
            int[] arr = input[i * 2].Split(' ').ToArray().Select(x => int.Parse(x)).ToArray();
            System.Console.WriteLine(i);
            System.Console.WriteLine("i " + i + "/" + output.Length);
            output[i - 1] = GetK(arr);
        }

        File.WriteAllLines("output.txt", output.Select(x => x.ToString()).ToArray());
        watch.Stop();
        TimeSpan ts = watch.Elapsed;

        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
        Console.WriteLine("RunTime " + elapsedTime);
    }

    static int GetK(int[] arr)
    {
        int dividable = 0;
        for (int k = arr.Length; k >= highestK; k--)
        {
            dividable = getDividableLength(arr, k);
            if (dividable == k) return dividable;
        }
        return dividable;

    }

    static int getDividableLength(int[] arr, int k)
    {
        int highestK = 0;
        int count = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] % k == 0) count++;
        }
        if (count >= k) highestK = k;
        return highestK;
    }
}
