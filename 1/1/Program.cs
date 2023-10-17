// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

// namespace cables
// {
//     public class Program
//     {
//         static void Main()
//         {
//             System.Console.WriteLine("start");
//             var watch = new System.Diagnostics.Stopwatch();
//             watch.Start();
//             string[] input = File.ReadAllText("input.txt").Split("\n");
//             // lets get how many test cases we have
//             int t = int.TryParse(input[0], out t) ? t : 0;
//             string[] output = new string[t];
//             string[] outputCorrect = File.ReadAllText("outputCorrect.txt").Split("\n");
//             int line = 1;
//             for (int caseNum = 1; caseNum <= t; caseNum++)
//             {
//                 int[] inputValues = input[line].Split(" ").Select(int.Parse).ToArray();
//                 int roomHeight = inputValues[0];
//                 int roomWidth = inputValues[1];
//                 int roomDeviceNumber = inputValues[2];
//                 line++;
//                 string[,] map = new string[roomHeight + 1, roomWidth + 1];


//                 for (int i = 0; i < roomDeviceNumber; i++)
//                 {
//                     string[] deviceValues = input[line].Split(" ").ToArray();
//                     int deviceHeight = int.TryParse(deviceValues[0], out deviceHeight) ? deviceHeight : 0;
//                     int deviceWidth = int.TryParse(deviceValues[1], out deviceWidth) ? deviceWidth : 0;
//                     string deviceName = deviceValues[2];

//                     map[deviceHeight, deviceWidth] = deviceName;
//                     line++;
//                 }

//                 // Now that we have the map, lets put them in a list and check one after another if they can be connected
//                 string result = Solvable(map, caseNum);

//                 output[caseNum - 1] = result;



//             }
//             File.WriteAllLines("output.txt", output);
//             watch.Stop();
//             TimeSpan ts = watch.Elapsed;
//             string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
//             ts.Hours, ts.Minutes, ts.Seconds,
//             ts.Milliseconds / 10);
//             watch.Start();
//             Console.WriteLine("RunTime " + elapsedTime);


//         }

//         static List<string> MapToList(string[,] map)
//         {
//             List<string> devices = new List<string>();
//             // TOP LINE
//             for (int w = 0; w < map.GetLength(1); w++)
//             {
//                 if (map[0, w] != null) devices.Add(map[0, w]);
//             }

//             // MIDDLE RIGHT LINES
//             for (int h = 1; h < map.GetLength(0) - 1; h++)
//             {
//                 if (map[h, map.GetLength(1) - 1] != null) devices.Add(map[h, map.GetLength(1) - 1]);
//             }

//             // BOTTOM LINE
//             for (int w = map.GetLength(1) - 1; w >= 0; w--)
//             {
//                 if (map[map.GetLength(0) - 1, w] != null) devices.Add(map[map.GetLength(0) - 1, w]);
//             }

//             // MIDDLE LEFT LINES
//             for (int h = map.GetLength(0) - 2; h > 0; h--)
//             {
//                 if (map[h, 0] != null) devices.Add(map[h, 0]);
//             }


//             return devices;
//         }

//         static string Solvable(string[,] map, int caseNum)
//         {
//             List<string> devices = MapToList(map);
//             int lastSize = 0;
//             List<int> deletePositions = new List<int>();
//             while (lastSize != devices.Count)
//             {
//                 lastSize = devices.Count;
//                 for (int i = 0; i < devices.Count; i++)
//                 {
//                     string deviceCurr = devices[i];
//                     string deviceBefore;
//                     int deviceBeforePos = new int();
//                     if (i - 1 == -1) { deviceBefore = devices[devices.Count - 1]; deviceBeforePos = devices.Count - 1; }
//                     else { deviceBefore = devices[i - 1]; deviceBeforePos = i - 1; }
//                     string deviceNext;
//                     int deviceNextPos = new int();
//                     if (i + 1 == devices.Count) { deviceNext = devices[0]; deviceNextPos = 0; }
//                     else { deviceNext = devices[i + 1]; deviceNextPos = i + 1; }

//                     if (deviceCurr == deviceBefore)
//                     {
//                         if (deletePositions.Contains(i) == false && deletePositions.Contains(deviceBeforePos) == false)
//                         {
//                             deletePositions.Add(i); deletePositions.Add(deviceBeforePos);
//                         }
//                     }
//                     else if (deviceCurr == deviceNext)
//                     {
//                         if (deletePositions.Contains(i) == false && deletePositions.Contains(deviceNextPos) == false)
//                         {
//                             deletePositions.Add(i); deletePositions.Add(deviceNextPos);
//                         }
//                     }
//                 }
//                 List<int> sortedDescending = deletePositions.OrderByDescending(x => x).ToList();
//                 sortedDescending = sortedDescending.Distinct().ToList();
//                 for (int i = 0; i < sortedDescending.Count; i++)
//                 {

//                     devices.RemoveAt(sortedDescending[i]);
//                 }

//                 sortedDescending.Clear();
//                 deletePositions.Clear();
//                 if (devices.Count == 0) return "pujde to";
//             }
//             return "ajajaj";
//         }
//         static void PrintMap(string[,] map)
//         {
//             for (int i = 0; i < map.GetLength(0); i++)
//             {
//                 for (int j = 0; j < map.GetLength(1); j++)
//                 {
//                     System.Console.Write(map[i, j] + " ");
//                 }
//                 System.Console.WriteLine();
//             }
//         }
//     }
// }


internal class Program
{
    static void Main(string[] args)
    {
        //Nacte data
        string Soubor = "input.txt";
        string Input = File.ReadAllText(Soubor);
        File.WriteAllText("output.txt", String.Empty);
        string[] RadkovanyVstup = Input.Split('\n');
        int PocetZadani = int.Parse(RadkovanyVstup[0]);
        //Spusti zpracovani prvniho zadani
        PraceSeZadanim(RadkovanyVstup, 1, PocetZadani);

    }


    //Funkce na zpracování zadání
    static void PraceSeZadanim(string[] RadkovanyVstup, int RadekKPrecteni, int PocetZadani)
    {
        string[] Cisla = RadkovanyVstup[RadekKPrecteni].Split(' ');

        int Y = int.Parse(Cisla[0]);
        int X = int.Parse(Cisla[1]);
        int N = int.Parse(Cisla[2]);

        string[] Zarizeni = new string[N];

        for (int j = 1; j <= N; j++)
        {
            Zarizeni[j - 1] = RadkovanyVstup[RadekKPrecteni + j];
        }
        //Seradi uzly, jak jdou v kruhu(ctverci)
        Zarizeni = ZaradDoKruhu(Zarizeni).ToArray();
        //Odstrani z arraye cisla
        Zarizeni = Zarizeni.Select(s => string.Join(" ", s.Split(' ').Skip(2))).ToArray();

        File.AppendAllText("output.txt", PropojKabely(Zarizeni));

        foreach (var item in Zarizeni)
        {
            Console.WriteLine(item);
        }
        PocetZadani--;
        if (PocetZadani > 0)
        {
            PraceSeZadanim(RadkovanyVstup, RadekKPrecteni + N + 1, PocetZadani);
        }
        else
        {
            Console.WriteLine("DOKONCENO");
            Console.Read();
        }


    }
    static string PropojKabely(string[] pole)
    {
        bool NejakaZmena = false;

        do
        {
            NejakaZmena = false;

            for (int i = 0; i < pole.Length - 1; i++)
            {
                if (pole[i] == pole[i + 1])
                {
                    // Odstranění dvou vedle sebe stejných prvků
                    pole = pole.Take(i).Concat(pole.Skip(i + 2)).ToArray();
                    NejakaZmena = true;
                    break;  // Návrat na začátek pole po provedení změn
                }
            }
        } while (NejakaZmena);

        if (pole.Length == 0)
        {
            return "pujde to\n";
        }
        else
        {
            return "ajajaj\n";
        }
    }

    static IEnumerable<string> ZaradDoKruhu(string[] pole)
    {
        // Extrahuj čísla pro souřadnice Y a X
        var pozice = pole.Select(prvek => new
        {
            Prvek = prvek,
            Y = int.Parse(prvek.Split(' ')[0]),
            X = int.Parse(prvek.Split(' ')[1])
        }).ToArray();

        var centralniBod = new
        {
            Y = pozice.Sum(p => p.Y) / pozice.Length,
            X = pozice.Sum(p => p.X) / pozice.Length
        };

        // Seřaď pole podle úhlů v rozsahu od 0 do 2π kolem centrálního bodu
        var serazeno = pozice.OrderBy(p => Math.Atan2(p.Y - centralniBod.Y, p.X - centralniBod.X) + 2 * Math.PI)
                             .ThenBy(p => Math.Sqrt(Math.Pow(p.Y - centralniBod.Y, 2) + Math.Pow(p.X - centralniBod.X, 2)))
                             .Select(p => p.Prvek)
                             .ToArray();

        return serazeno;
    }
}