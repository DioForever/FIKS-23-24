namespace Routing
{
    class Routing
    {

        static void Main(string[] args)
        {

            Task[] tasks = ParseInput("input.txt");
            List<string> output = new List<string>();

            for (int t = 0; t < tasks.Length; t++)
            {
                // if (t == 5) break;
                // System.Console.WriteLine(tasks[t].adresses.Length);
                for (int a = 0; a < tasks[t].adresses.Length; a++)
                {
                    Adress adress = tasks[t].adresses[a];
                    // System.Console.WriteLine(adress.ToString());
                }
                var highestMask = GetHighestMask(tasks[t], t, tasks.Length);
                // System.Console.WriteLine($"Highest mask achieved = {highestMask}");
                output.Add(highestMask.mask.ToDecimalIP());
                if (t == tasks.Length) output.Add((highestMask.savedTime + 10).ToString());
                else output.Add(highestMask.savedTime.ToString());
                System.Console.WriteLine($"---{t}");
            }
            // System.Console.WriteLine(output);
            File.WriteAllLines("output.txt", output);
            System.Console.WriteLine("Writen" + output.Count);

        }
        static Task[] ParseInput(string inputFileName)
        {
            string[] input_unparsed = File.ReadAllLines(inputFileName);
            int t = int.Parse(input_unparsed[0]); // Number of tasks
            Task[] tasks = new Task[t];
            int linePointer = 1;
            for (int taskCount = 0; taskCount < t; taskCount++)
            {
                string[] line_n_k = input_unparsed[linePointer].Split(" ");
                int n = int.Parse(line_n_k[0]); // Number of Adresses
                int k = int.Parse(line_n_k[1]); // Maximal number of subnets
                Adress[] adresses = new Adress[n];
                linePointer++;
                for (int adressCount = 0; adressCount < n; adressCount++)
                {
                    string[] adress_unparsed = input_unparsed[linePointer].Split(".");
                    byte[] IP = new byte[32];
                    for (int i = 0; i < 4; i++)
                    {
                        string bits = Convert.ToString(int.Parse(adress_unparsed[i]), 2).PadLeft(8, '0');
                        for (int l = 0; l < 8; l++)
                        {
                            IP[i * 8 + l] = byte.Parse((bits[l]).ToString());
                        }
                    }
                    Adress adress = new Adress(IP);
                    adresses[adressCount] = adress;

                    linePointer++;
                }

                tasks[taskCount] = new Task(adresses, k);
            }
            return tasks;
        }

        static (Adress mask, int savedTime) GetHighestMask(Task task, int t, int tasksLen)
        {
            // Initiate all necesary variables
            int bitPointer = 0;
            // Dictionary<int, int> adressToSubnetDict = new Dictionary<int, int>();
            Dictionary<int, int[]> subnetsDict = new Dictionary<int, int[]>();
            int[] adressesInSubnet = new int[task.adresses.Length];
            for (int i = 0; i < task.adresses.Length; i++)
            {
                // adressToSubnetDict.Add(i, 0);
                adressesInSubnet[i] = i + 1;
            }
            subnetsDict.Add(0, adressesInSubnet);
            // System.Console.WriteLine($"start {ArrayToString(subnetsDict[0])}");
            byte[] maskBytes = new byte[32] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Adress newMask = new Adress(maskBytes);
            // Call recursive function to get the HIGHEST mask possible
            Task taskCurr = task;
            Thread threadMask = new Thread(() => FindHighestMask(taskCurr, bitPointer, subnetsDict, newMask));
            var highestMaskSearch = FindHighestMask(task, bitPointer, subnetsDict, newMask);
            Adress mask = highestMaskSearch.mask;
            System.Console.WriteLine($"Task {mask} {task.adresses.Length}");
            // foreach (int key in highestMaskSearch.subnetsDict.Keys)
            // {
            //     System.Console.WriteLine($"Subnet {key} - {string.Join(", ", highestMaskSearch.subnetsDict[key])}");
            // }
            // System.Console.WriteLine("Finding shortcut");
            // if (t == (tasksLen - 1)) return (mask, 0);
            var savedTime = GetSavedTime(highestMaskSearch.subnetsDict, highestMaskSearch.mask, task);
            // System.Console.WriteLine($"Finished looking for shortcut {savedTime}");

            return (mask, savedTime);
        }
        // private static int totalTimeSaved = 0; // Shared variable
        // private static List<string> explored = new List<string>();

        // private static object lockTotalTime = new object(); // Used for thread safety
        // private static object lockExplored = new object();
        static int GetSavedTime(Dictionary<int, int[]> subnetsDict, Adress mask, Task task)
        {
            // We get positions that we care about by going through mask
            List<int> positions = new List<int>(32);
            for (int pos = 0; pos < mask.IP.Length; pos++)
            {
                if (mask.IP[pos] == 1)
                {
                    positions.Add(pos);
                    // System.Console.WriteLine($"Pos {pos}");
                }
            }
            // System.Console.WriteLine(positions.Count);

            // We go through all subnets, create their array of bytes
            List<byte[]> bytes = new List<byte[]>();
            int[] keys = subnetsDict.Keys.ToArray();
            // System.Console.WriteLine();
            // System.Console.WriteLine(subnetsDict.Keys.Count);
            for (int i = 0; i < subnetsDict.Keys.Count; i++)
            {
                int currIPNum = subnetsDict[keys[i]][0];
                // System.Console.WriteLine($"curr IPNum {currIPNum - 1}");
                byte[] travelAdress = new byte[positions.Count];
                short travelAdressCount = 0;
                Adress adress = task.adresses[currIPNum - 1];
                foreach (int pos in positions)
                {
                    travelAdress[travelAdressCount] = adress.IP[pos];
                    travelAdressCount++;
                }
                // System.Console.WriteLine(ArrayToStringByte(travelAdress));
                bytes.Add(travelAdress);

            }
            // Now we go and check if there is any time save between A -> B and A -> C -> B
            int totalTimeSaved = 0;
            List<string> createdEdges = new List<string>();
            // System.Console.WriteLine();

            // int travelTime = TravelTime(bytes[start])
            List<int> travelable = new List<int>();
            for (int l = 0; l < bytes.Count; l++)
            {
                travelable.Add(l);
            }

            Graph graph = new Graph();
            for (int i = 0; i < bytes.Count; i++)
            {
                Dictionary<int, int> travelTimeTo = getTravelTimeTo(bytes, travelable, i);
                foreach (int key in travelTimeTo.Keys)
                {
                    graph.AddEdge($"{i}", $"{key}", travelTimeTo[key]);
                    // System.Console.WriteLine($"Added edge {i}-{key} T={travelTimeTo[key]}");
                }

            }
            int[,] distances = FloydWarshallWithGraph.SolveFloydGraph(graph);
            List<string> explored = new List<string>();

            // int shortestPathLength = distances[graph.GetVertexIndex($"{i}"), graph.GetVertexIndex($"{key}")];
            // Console.WriteLine("Shortest path length from 0 to 1: " + shortestPathLength);
            for (int startIndex = 0; startIndex < bytes.Count; startIndex++)
            {
                for (int finishIndex = 0; finishIndex < bytes.Count; finishIndex++)
                {
                    if (startIndex == finishIndex) continue;
                    if (explored.Contains($"{finishIndex}{startIndex}")) continue;
                    explored.Add($"{startIndex}{finishIndex}");
                    int straightTravelTime = TravelTime(bytes[startIndex], bytes[finishIndex]);
                    int shortestPathTime = distances[graph.GetVertexIndex($"{startIndex}"), graph.GetVertexIndex($"{finishIndex}")];
                    int savedTime = straightTravelTime - shortestPathTime;
                    if (savedTime > 0)
                    {
                        // System.Console.WriteLine($"Shorter path org{startIndex} ({straightTravelTime}) -> shr{finishIndex} ({shortestPathTime})");
                        totalTimeSaved += savedTime;
                    }
                }
            }

            System.Console.WriteLine($"Total TS {totalTimeSaved}");

            // for (int startIndex = 0; startIndex < bytes.Count; startIndex++)
            // {
            //     for (int finishIndex = 0; finishIndex < bytes.Count; finishIndex++)
            //     {
            //         int start = startIndex;
            //         int finish = finishIndex;
            //         Thread thread = new(() => DoShortcut(bytes, start, finish));
            //         thread.Start();
            //     }
            // }
            return totalTimeSaved;

        }


        // static void DoShortcut(List<byte[]> bytes, int startIndex, int finishIndex)
        // {
        //     if (startIndex == finishIndex) return;
        //     // if (explored.Contains($"{startIndex}{finishIndex}")) return;
        //     // // explored.Add($"{startIndex}{finishIndex}");
        //     // lock (lockCheck) explored.Add($"{finishIndex}{startIndex}");
        //     lock (lockExplored)
        //     {
        //         if (explored.Contains($"{startIndex}{finishIndex}"))
        //             return;
        //         explored.Add($"{finishIndex}{startIndex}");
        //     }
        //     var straightTravelTime = TravelTime(bytes[startIndex], bytes[finishIndex]);
        //     List<int> travelable = new List<int>();
        //     for (int l = 0; l < bytes.Count; l++)
        //     {
        //         if (l != startIndex) travelable.Add(l);
        //     }
        //     List<int> traveled = new List<int>();
        //     var shortcut = FindShortcut(bytes, travelable, 0, straightTravelTime, startIndex, finishIndex, startIndex, traveled, 0);
        //     if (shortcut.bestSave != 0)
        //     {
        //         System.Console.WriteLine($"Shortened with {startIndex}->{string.Join("->", shortcut.traveled)} F{finishIndex} - S->F = {shortcut.bestSave} by {string.Join(",", traveled)}");
        //         lock (lockTotalTime) totalTimeSaved += shortcut.bestSave;
        //     }
        //     return;
        // }



        // // static (List<int> travelable, int travelTime, List<int> traveled, bool outcome, int bestSave) FindShortcut(List<byte[]> bytes, List<int> travelable,
        //  int travelTime, int travelTimeStraight, int start, int finish, int last, List<int> traveled, int bestTS)
        // {
        //     ;
        //     Dictionary<int, int> travelTimeTo = getTravelTimeTo(bytes, travelable, last);
        //     int[] sortedKeys = GetSortedKeys(travelTimeTo);
        //     foreach (int key in sortedKeys)
        //     {
        //         // System.Console.WriteLine($"Looking at key {key}");
        //         if (key == finish)
        //         {
        //             // System.Console.WriteLine($"Found finish, returning");
        //             bestTS = UpdateBestTS(travelTimeStraight, bestTS, travelTime + travelTimeTo[key]);
        //             // if (bestTS != 0) { System.Console.WriteLine($"Updated {bestTS}"); Console.ReadLine(); }
        //             travelable.Remove(key);
        //             traveled.Add(key);
        //             return (travelable, travelTime + travelTimeTo[key], traveled, true, bestTS);
        //         }

        //         List<int> travelableSearch = travelable.ToList();
        //         List<int> traveledSearch = traveled.ToList();
        //         travelableSearch.Remove(key);
        //         traveledSearch.Add(key);

        //         if (travelTime + travelTimeTo[key] >= (travelTimeStraight - bestTS))
        //         {
        //             // System.Console.WriteLine($"Inefficient travel {key}");
        //             continue;
        //         }
        //         var searchShortcut = FindShortcut(bytes, travelableSearch, travelTime + travelTimeTo[key], travelTimeStraight, start, finish, key, traveledSearch, bestTS);
        //         if (bestTS < searchShortcut.bestSave) bestTS = searchShortcut.bestSave;
        //         if (searchShortcut.outcome)
        //         {
        //             // System.Console.WriteLine($"Sending down");
        //             return (searchShortcut.travelable, travelTimeStraight, searchShortcut.traveled, true, bestTS);
        //         }

        //     }

        //     return (travelable, travelTimeStraight, traveled, false, bestTS);
        // static (List<int> travelable, int travelTime, List<int> traveled, int bestSave) FindShortcut(List<byte[]> bytes, List<int> travelable,
        //  int travelTime, int travelTimeStraight, int start, int finish, int last, List<int> traveled, int bestTS)
        // {

        //     // System.Console.WriteLine($"Start {start} - finish {finish} - last {last}");
        //     Dictionary<int, int> travelTimeTo = getTravelTimeTo(bytes, travelable, last);
        //     int[] sortedKeys = GetSortedKeys(travelTimeTo);
        //     // System.Console.WriteLine(travelTimeTo.Count);
        //     // System.Console.WriteLine($"sorted {travelable.Count}");
        //     foreach (int key in sortedKeys)
        //     {
        //         // System.Console.WriteLine($"Traveled {string.Join(", ", traveled)}");
        //         // System.Console.WriteLine($"Key {key} - {travelTimeTo[key]}");
        //         // if (key == last)
        //         // {
        //         //     System.Console.WriteLine("Skipped");
        //         //     continue;
        //         // }
        //         if (key == finish)
        //         {
        //             bestTS = UpdateBestTS(travelTimeStraight, bestTS, travelTime + travelTimeTo[key]);
        //             // if (bestTS != 0) { System.Console.WriteLine($"Updated {bestTS}"); Console.ReadLine(); }
        //             continue;
        //         }

        //         List<int> travelableSearch = travelable.ToList();
        //         List<int> traveledSearch = traveled.ToList();
        //         travelableSearch.Remove(key);
        //         traveledSearch.Add(key);

        //         if (travelTime + travelTimeTo[key] >= (travelTimeStraight - bestTS)) continue;
        //         var searchShortcut = FindShortcut(bytes, travelableSearch, travelTime + travelTimeTo[key], travelTimeStraight, start, finish, key, traveledSearch, bestTS);
        //         if (bestTS < searchShortcut.bestSave) bestTS = searchShortcut.bestSave;

        //     }

        //     return (travelable, travelTimeStraight, traveled, bestTS);



        //     // System.Console.WriteLine($"sortedKeys {string.Join(", ", sortedKeys)}");
        //     // System.Console.WriteLine($"travelable {string.Join(", ", travelable)}");

        //     // foreach (int key in sortedKeys)
        //     // {
        //     //     // System.Console.WriteLine($"Key {key} from {string.Join(", ", sortedKeys)}");
        //     //     // System.Console.WriteLine($"Key {key} traveled {string.Join(", ", traveled)}");
        //     //     if (key == last)
        //     //     {
        //     //         // System.Console.WriteLine($"Skipped cuz same - {string.Join(", ", sortedKeys)}");
        //     //         continue;
        //     //     }
        //     //     if (key == finish)
        //     //     {
        //     //         // System.Console.WriteLine($"Finished cuz found a way {string.Join(", ", traveled)} - {travelTime + travelTimeTo[key]}?{travelTimeStraight}");
        //     //         if (travelTimeStraight - (travelTime + travelTimeTo[key]) > bestSave) bestSave = travelTimeStraight - (travelTime + travelTimeTo[key]);
        //     //         // return (travelable, travelTime + travelTimeTo[key], true, traveled, bestSave);
        //     //         continue;
        //     //     }

        //     //     int travelTimeDeep = travelTime + travelTimeTo[key];
        //     //     List<int> traveledDeep = traveled.ToList();
        //     //     List<int> travelableDeep = travelable.ToList();

        //     //     if (travelTimeDeep > travelTimeStraight)
        //     //     {
        //     //         // System.Console.WriteLine($"Returned cuz too long {travelTimeDeep}>{travelTimeStraight}");
        //     //         // return (travelable, travelTime, false, traveled, bestSave);
        //     //         continue;
        //     //     }

        //     //     travelableDeep.Remove(key);
        //     //     traveledDeep.Add(key);

        //     //     // System.Console.WriteLine($"Key {key} time + {travelTimeDeep}, traveled {string.Join(", ", traveled)}");
        //     //     var shortcutDeep = FindShortcut(bytes, travelableDeep, travelTimeDeep, travelTimeStraight, start, finish, key, traveledDeep, bestSave);

        //     //     if (shortcutDeep.outcome)
        //     //     {
        //     //         // System.Console.WriteLine($"Sent down finished {shortcutDeep.travelTime}?{travelTimeStraight}");
        //     //         if (travelTimeStraight - shortcutDeep.travelTime > bestSave) bestSave = travelTimeStraight - shortcutDeep.travelTime;
        //     //         // return (shortcutDeep.travelable, shortcutDeep.travelTime, true, shortcutDeep.traveled, bestSave);
        //     //     }
        //     // }


        // }

        static int UpdateBestTS(int straightTravelTime, int bestTS, int travelTime)
        {
            int TS = straightTravelTime - travelTime;
            if (TS > bestTS) return TS;
            return bestTS;
        }

        static int[] GetSortedKeys(Dictionary<int, int> travelTimeTo)
        {
            // Use LINQ to sort the keys based on their values
            var sortedKeys = travelTimeTo.OrderBy(kv => kv.Value).Select(kv => kv.Key).ToArray();
            return sortedKeys;
        }

        static Dictionary<int, int> getTravelTimeTo(List<byte[]> bytes, List<int> travelable, int last)
        {
            Dictionary<int, int> travelTimeTo = new Dictionary<int, int>();
            // System.Console.WriteLine($"Last {last}");

            for (int i = 0; i < travelable.Count; i++)
            {
                int keyTravelable = travelable[i];
                if (keyTravelable == last) continue;

                int travelTime = TravelTime(bytes[last], bytes[keyTravelable]);
                travelTimeTo[keyTravelable] = travelTime;
            }
            return travelTimeTo;
        }
        static int BytesDifferent(byte[] b1, byte[] b2)
        {
            int c = 0;
            for (short i = 0; i < b1.Length; i++)
            {
                if (b1[i] != b2[i]) c++;
            }
            return c;
        }

        static int TravelTime(byte[] b1, byte[] b2)
        {
            double bytesDiff = BytesDifferent(b1, b2);
            // System.Console.WriteLine(bytesDiff);
            int time = (int)Math.Floor(bytesDiff / 2);
            if (time == 0) return 1;
            return time;
        }

        /// <summary>
        /// Recursive function that looks for a mask while giving priority to 1 and once it find maximal possible mask it returns the adress.
        /// </summary>
        /// <param name="task"></param>
        /// <param name="bitPointer"></param>
        /// <param name="adressToSubnetDict"></param>
        /// <param name="subnetsDict"></param>
        /// <param name="nNum">Current highest number of subnetwork</param>
        /// <returns></returns>
        static (Dictionary<int, int[]> subnetsDict, Adress mask) FindHighestMask(Task task, int bitPointer, Dictionary<int, int[]> subnetsDict, Adress mask, int nNum = 0)
        {
            if (bitPointer == 32) return (subnetsDict, mask);
            // This gets us two groups if we intend to use 1
            (int[] ones, int[] zeros) = GetOnesAndZerosAtBit(task, bitPointer);
            // System.Console.WriteLine($"Current mask - {mask.ToString()}");

            // Trying using 1 at bitPointer =>
            // Update subnetworks
            var updatedSubnets = UpdateSubnets(subnetsDict, ones, zeros);
            // if > maxSubnetworks return False
            if (updatedSubnets.subnetsCount > task.maxSubnets) return FindHighestMask(task, bitPointer + 1, subnetsDict, mask);

            // Update mask at bitPointer to 1
            mask.IP[bitPointer] = 1;
            // Call FindHighestMask
            return FindHighestMask(task, bitPointer + 1, updatedSubnets.subnetsDict, mask, updatedSubnets.subnetsCount);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="subnetsDict">Dictionary of subnet numbers and int arrays of adresses nums</param>
        /// <param name="ones">Int array of ones</param>
        /// <param name="zeros">Int array of zeroes</param>
        /// <param name="n">Current highest number of subnetwork</param>
        /// <returns></returns>
        static (int subnetsCount, Dictionary<int, int[]> subnetsDict) UpdateSubnets(Dictionary<int, int[]> subnetsDict,
             int[] ones, int[] zeros)
        {
            Dictionary<int, int[]> subnetsNew = subnetsDict.ToDictionary(entry => entry.Key, entry => entry.Value);


            foreach (int keySubnet in subnetsDict.Keys)
            {
                int[] currSubnet = subnetsDict[keySubnet];
                int[] toBeRelocatedOnes = new int[currSubnet.Length];
                int onesRelocationCounter = 0;
                int[] toBeRelocatedZeros = new int[currSubnet.Length];
                int zerosRelocationCounter = 0;
                // System.Console.WriteLine($"Subnet {keySubnet} - {ArrayToString(currSubnet)}");
                foreach (int adressNum in currSubnet)
                {
                    // System.Console.WriteLine($"adressNum {adressNum}");
                    foreach (int oneNum in ones)
                    {
                        if (oneNum == adressNum) { toBeRelocatedOnes[onesRelocationCounter] = adressNum; onesRelocationCounter++; }
                    }
                    foreach (int zeroNum in zeros)
                    {
                        // System.Console.WriteLine($"Is {adressNum} == {zeroNum}? = {zeroNum == adressNum}");
                        if (zeroNum == adressNum) { toBeRelocatedZeros[zerosRelocationCounter] = adressNum; zerosRelocationCounter++; }
                    }
                }

                // System.Console.WriteLine($"toBeRelocated {ArrayToString(toBeRelocatedOnes)} and {ArrayToString(toBeRelocatedZeros)}");

                bool selfMet = false;
                var RelocationList = new List<int>(ones.Length);
                for (int i = 0; i < currSubnet.Length; i++)
                {
                    selfMet = false;

                    int adressNum = currSubnet[i];
                    foreach (int adressNumOnes in toBeRelocatedOnes)
                    {
                        if (adressNumOnes == 0) break;
                        if (adressNumOnes == adressNum)
                        {
                            selfMet = true;
                        }

                        // System.Console.WriteLine(adressNumOnes);
                    }
                    if (selfMet != true) RelocationList.Add(adressNum);
                }

                // If its equal, nothing changed so we dont need to bother recreating it
                // System.Console.WriteLine($"Length comp {RelocationList.Count} with {currSubnet.Length} -> {RelocationList.Count != currSubnet.Length}");
                if (RelocationList.Count != currSubnet.Length)
                {
                    var tempNewSubnet = RelocationList.ToArray();
                    var tempOldSubnet = new List<int>(currSubnet.Length - RelocationList.Count);

                    foreach (int adressNum in currSubnet)
                    {
                        bool found = false;
                        foreach (int adressNumCheck in tempNewSubnet)
                        {
                            if (adressNum == adressNumCheck) { found = true; break; }
                        }
                        if (!found) { tempOldSubnet.Add(adressNum); }
                    }

                    subnetsNew[keySubnet] = tempOldSubnet.ToArray();
                    if (tempNewSubnet.Length != 0) subnetsNew[subnetsNew.Count + 1] = tempNewSubnet;
                }
                // System.Console.WriteLine($"RelocationList - {ArrayToString(RelocationList.ToArray())}");
                // System.Console.WriteLine($"Old subnets - {SubnetsToString(subnetsDict)}");
                // System.Console.WriteLine($"New subnets - {SubnetsToString(subnetsNew)}");

            }

            return (subnetsNew.Count, subnetsNew);

        }

        static (int[], int[]) GetOnesAndZerosAtBit(Task task, int bitPointer)
        {
            List<int> ones = new List<int>();
            List<int> zeros = new List<int>();

            for (int adressCount = 0; adressCount < task.adresses.Length; adressCount++)
            {
                Adress adress = task.adresses[adressCount];
                if (adress.IP[bitPointer] == 1)
                {
                    ones.Add(adressCount + 1);
                    continue;
                }
                zeros.Add(adressCount + 1);
            }
            return (ones.ToArray(), zeros.ToArray());
        }

        class Task
        {
            public Adress[] adresses;
            public readonly int maxSubnets;
            public Task(Adress[] adresses, int maxSubnets)
            {
                this.adresses = adresses;
                this.maxSubnets = maxSubnets;
            }
        }

        class Adress
        {
            public byte[] IP;
            public Adress(byte[] IP)
            {
                this.IP = IP;
            }

            public override string ToString()
            {
                string adress = "";

                for (int l = 0; l < IP.Length; l++)
                {
                    adress += IP[l];
                    if ((l + 1) % 8 == 0 && (l + 1) != IP.Length) { adress += "."; }
                }
                return adress;
            }
            public string ToDecimalIP()
            {
                if (IP.Length != 32)
                {
                    throw new ArgumentException("Invalid binary IP address length. It should contain 32 bits.");
                }

                // Convert the binary IP array to a string of 0s and 1s
                string binaryIP = ToString();
                string[] binaryParts = binaryIP.Split('.');
                byte[] decimalParts = new byte[4];

                for (int i = 0; i < 4; i++)
                {
                    string binaryPart = binaryParts[i];
                    decimalParts[i] = Convert.ToByte(binaryPart, 2);
                }

                return $"{decimalParts[0]}.{decimalParts[1]}.{decimalParts[2]}.{decimalParts[3]}";
            }
        }
        static string ArrayToString(int[] input)
        {
            string s = "";
            for (int i = 0; i < input.Length; i++)
            {
                s += input[i].ToString() + "";
            }
            return s;
        }
        static string ArrayToStringByte(byte[] input)
        {
            string s = "";
            for (int i = 0; i < input.Length; i++)
            {
                s += input[i].ToString() + ", ";
            }
            return s;
        }

        static string SubnetsToString(Dictionary<int, int[]> subnets)
        {
            string s = "";
            foreach (int key in subnets.Keys)
            {
                s += "{" + ArrayToString(subnets[key]) + "} ";
            }
            return s;
        }
    }

}

