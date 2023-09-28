using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cables
{
    public class Program
    {
        static void Main()
        {
            System.Console.WriteLine("start");
            string[] input = File.ReadAllText("input.txt").Split("\n");
            // lets get how many test cases we have
            int t = int.TryParse(input[0], out t) ? t : 0;
            string[] output = new string[t];
            string[] outputCorrect = File.ReadAllText("outputCorrect.txt").Split("\n");
            int line = 1;
            for (int caseNum = 1; caseNum <= t; caseNum++)
            {
                int[] inputValues = input[line].Split(" ").Select(int.Parse).ToArray();
                int roomHeight = inputValues[0];
                int roomWidth = inputValues[1];
                int roomDeviceNumber = inputValues[2];
                line++;
                string[,] map = new string[roomHeight + 1, roomWidth + 1];


                for (int i = 0; i < roomDeviceNumber; i++)
                {
                    string[] deviceValues = input[line].Split(" ").ToArray();
                    int deviceHeight = int.TryParse(deviceValues[0], out deviceHeight) ? deviceHeight : 0;
                    int deviceWidth = int.TryParse(deviceValues[1], out deviceWidth) ? deviceWidth : 0;
                    string deviceName = deviceValues[2];

                    map[deviceHeight, deviceWidth] = deviceName;
                    line++;
                }

                // Now that we have the map, lets put them in a list and check one after another if they can be connected
                string result = Solvable(map, caseNum);

                output[caseNum - 1] = result;



            }
            File.WriteAllLines("output.txt", output);

        }

        static List<string> MapToList(string[,] map)
        {
            List<string> devices = new List<string>();
            // TOP LINE
            for (int w = 0; w < map.GetLength(1); w++)
            {
                if (map[0, w] != null) devices.Add(map[0, w]);
            }

            // MIDDLE RIGHT LINES
            for (int h = 1; h < map.GetLength(0) - 1; h++)
            {
                if (map[h, map.GetLength(1) - 1] != null) devices.Add(map[h, map.GetLength(1) - 1]);
            }

            // BOTTOM LINE
            for (int w = map.GetLength(1) - 1; w >= 0; w--)
            {
                if (map[map.GetLength(0) - 1, w] != null) devices.Add(map[map.GetLength(0) - 1, w]);
            }

            // MIDDLE LEFT LINES
            for (int h = map.GetLength(0) - 2; h > 0; h--)
            {
                if (map[h, 0] != null) devices.Add(map[h, 0]);
            }


            return devices;
        }

        static string Solvable(string[,] map, int caseNum)
        {
            List<string> devices = MapToList(map);
            int lastSize = 0;
            List<int> deletePositions = new List<int>();
            while (lastSize != devices.Count)
            {
                lastSize = devices.Count;
                for (int i = 0; i < devices.Count; i++)
                {
                    string deviceCurr = devices[i];
                    string deviceBefore;
                    int deviceBeforePos = new int();
                    if (i - 1 == -1) { deviceBefore = devices[devices.Count - 1]; deviceBeforePos = devices.Count - 1; }
                    else { deviceBefore = devices[i - 1]; deviceBeforePos = i - 1; }
                    string deviceNext;
                    int deviceNextPos = new int();
                    if (i + 1 == devices.Count) { deviceNext = devices[0]; deviceNextPos = 0; }
                    else { deviceNext = devices[i + 1]; deviceNextPos = i + 1; }

                    if (deviceCurr == deviceBefore)
                    {
                        if (deletePositions.Contains(i) == false && deletePositions.Contains(deviceBeforePos) == false)
                        {
                            deletePositions.Add(i); deletePositions.Add(deviceBeforePos);
                        }
                    }
                    else if (deviceCurr == deviceNext)
                    {
                        if (deletePositions.Contains(i) == false && deletePositions.Contains(deviceNextPos) == false)
                        {
                            deletePositions.Add(i); deletePositions.Add(deviceNextPos);
                        }
                    }
                }
                List<int> sortedDescending = deletePositions.OrderByDescending(x => x).ToList();
                sortedDescending = sortedDescending.Distinct().ToList();
                for (int i = 0; i < sortedDescending.Count; i++)
                {

                    devices.RemoveAt(sortedDescending[i]);
                }

                sortedDescending.Clear();
                deletePositions.Clear();
                if (devices.Count == 0) return "pujde to";
            }
            return "ajajaj";
        }
        static void PrintMap(string[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    System.Console.Write(map[i, j] + " ");
                }
                System.Console.WriteLine();
            }
        }
    }
}

