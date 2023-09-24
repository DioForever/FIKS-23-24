namespace cables
{
    class Program
    {
        static void Main()
        {
            string[] input = File.ReadAllText("input.txt").Split("\n");

            // lets get how many test cases we have
            int t = int.TryParse(input[0], out t) ? t : 0;


            // now we need to go through each test case
            int line = 1;
            int correct = 0;
            string[] answers = new string[t];
            for (int caseNumber = 1; caseNumber <= 1; caseNumber++)
            {
                // we will take description of each test case and get height, width and how many devices there are 
                string[] description = input[line].Split(" ");
                int h = (int.TryParse(description[0], out h) ? h : 0) + 1;
                int w = (int.TryParse(description[1], out w) ? w : 0) + 1;
                int n = int.TryParse(description[2], out n) ? n : 0;
                //  h = height, w = width, n = number of devices
                System.Console.WriteLine("Case #" + caseNumber + ":" + " line: " + (line + 1));
                System.Console.WriteLine(input[line]);
                line++;


                Dictionary<string, int> device_identifiers = new Dictionary<string, int>();
                Dictionary<string, (int, List<int[]>)> device_count = new Dictionary<string, (int, List<int[]>)>();
                int device_identifier_count = 1;
                // now we need to create a grid with the height and width we got from the description
                int[,] grid = new int[h, w];
                grid = preset_grid(grid, h, w);
                string[] devices = new string[n];


                // System.Console.WriteLine(t + " " + h + " " + w + " " + n);
                for (int i = 0; i < n; i++)
                {
                    string[] device = input[line].Split(" ");

                    int deviceH = int.TryParse(device[0], out deviceH) ? deviceH : 0;
                    int deviceW = int.TryParse(device[1], out deviceW) ? deviceW : 0;

                    // System.Console.WriteLine(deviceH + " " + deviceW + " " + device[2]);
                    line++;
                    // System.Console.WriteLine($"device {string.Join(" ", device)}");
                    if (!device_identifiers.ContainsKey(device[2]))
                    {
                        // we havent seen this device before so we set its identifier
                        // System.Console.WriteLine(line.ToString() + " " + i.ToString() + " " + n.ToString());
                        device_identifiers.Add(device[2], device_identifier_count);
                        device_identifier_count++;
                        List<int[]> coordinates = new List<int[]>();
                        int[] cords = new int[2] { deviceH, deviceW };
                        coordinates.Add(cords);
                        device_count.Add(device[2], (1, coordinates));
                    }
                    else
                    {
                        List<int[]> coordinates = device_count[device[2]].Item2;
                        int[] cords = new int[2] { deviceH, deviceW };
                        coordinates.Add(cords);
                        device_count[device[2]] = (device_count[device[2]].Item1 + 1, coordinates);
                    }
                    // devices[i] = device[0] + " " + device[1] + " " + device[2];
                    grid[deviceH, deviceW] = device_identifiers[device[2]];
                }


                if (solveGrid(grid, device_count))
                {
                    correct++;
                    System.Console.WriteLine("pujde to");
                    System.Console.WriteLine(checkOutcome(caseNumber, "pujde to"));
                    answers[caseNumber - 1] = "pujde to";
                    continue;
                }
                System.Console.WriteLine("ajajaj");
                System.Console.WriteLine(checkOutcome(caseNumber, "ajajaj"));
                answers[caseNumber - 1] = "ajajaj";

            }
            System.Console.WriteLine("Correct: " + correct + " out of " + t);
            File.WriteAllLines("output.txt", answers);
        }

        public static bool even_devices(Dictionary<string, (int, List<int[]>)> device_count)
        {
            foreach (KeyValuePair<string, (int, List<int[]>)> device in device_count)
            {
                if (device.Value.Item1 < 2)
                {
                    return false;
                }
            }
            return true;
        }

        public static int[,] preset_grid(int[,] grid, int h, int w)
        {
            for (int i = 0; i < h; i++)

                for (int j = 0; j < w; j++)
                {
                    grid[i, j] = 0;
                }
            return grid;
        }

        public static void show_grid(int[,] grid)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    System.Console.Write(grid[i, j] + " ");
                }
                System.Console.WriteLine();
            }
        }

        public static bool checkOutcome(int caseNumber, string outcome)
        {
            string[] input = File.ReadAllText("outputCorrect.txt").Split("\n");
            if (input[5] == outcome)
            {
                return true;
            }
            return false;
        }

        public static bool checkConnected(int[,] grid, int cabelH, int cabelW, int device_identifier_count)
        {
            // we need to check if the cabel is connected to another device

            // Top corner
            if (cabelH - 1 >= 0)
            {
                if (grid[cabelH - 1, cabelW] == device_identifier_count) return true;
            }
            // Top Left corner
            if (cabelH - 1 >= 0 && cabelW - 1 >= 0)
            {
                if (grid[cabelH - 1, cabelW - 1] == device_identifier_count) return true;
            }
            // Top Right corner
            if (cabelH - 1 >= 0 && cabelW + 1 < grid.GetLength(1))
            {
                if (grid[cabelH - 1, cabelW + 1] == device_identifier_count) return true;
            }
            // Left corner
            if (cabelW - 1 >= 0)
            {
                if (grid[cabelH, cabelW - 1] == device_identifier_count) return true;
            }
            // Right corner
            if (cabelW + 1 < grid.GetLength(1))
            {
                if (grid[cabelH, cabelW + 1] == device_identifier_count) return true;
            }
            // Bottom corner
            if (cabelH + 1 < grid.GetLength(0))
            {
                if (grid[cabelH + 1, cabelW] == device_identifier_count) return true;
            }
            // Bottom Left corner
            if (cabelH + 1 < grid.GetLength(0) && cabelW - 1 >= 0)
            {
                if (grid[cabelH + 1, cabelW - 1] == device_identifier_count) return true;
            }
            // Bottom Right corner
            if (cabelH + 1 < grid.GetLength(0) && cabelW + 1 < grid.GetLength(1))
            {
                if (grid[cabelH + 1, cabelW + 1] == device_identifier_count) return true;
            }
            return false;

        }

        public static int[] getStartingPoints(Dictionary<string, (int, List<int[]>)> device_count)
        {
            int[] starting_points = new int[device_count.Count];

            for (int i = 0; i < device_count.Count; i++)
            {
                // we need to get all starting points for each device
                starting_points[i] = device_count.ElementAt(i).Value.Item1;

            }
            return starting_points;

        }
        public static bool solveGrid(int[,] grid, Dictionary<string, (int, List<int[]>)> device_count)
        {
            show_grid(grid);
            if (!even_devices(device_count))
            {
                return false;
            }
            // we need to get all combinations of starting and ending points
            int[] starting_points = getStartingPoints(device_count);

            List<Connection> connections = new List<Connection>();

            foreach (KeyValuePair<string, (int, List<int[]>)> device in device_count)
            {
                for (int i = 0; i < device.Value.Item2.Count; i++)
                {
                    // System.Console.WriteLine(string.Join("Device ", device.Value.Item2[i]), device);
                    int[] starting_point = device.Value.Item2[i];
                    int[] ending_point = new int[2] { 0, 0 };
                    connections.Add(new Connection(starting_point, new List<int[,]>(), ending_point));
                }
            }
            foreach (Connection connect in connections)
            {
                System.Console.WriteLine("Connections " + string.Join(" ", connect.starting_point) + " - " + string.Join(" ", connect.ending_point));
            }





            return true;
        }

        public static void findPath()
        {

        }


    }

    public class Connection
    {
        public int[] starting_point;
        public int[,] grid;
        public List<int[,]> path;
        public int[] ending_point;

        public Connection(int[] starting_point, List<int[,]> path, int[] ending_point)
        {
            this.starting_point = starting_point;
            this.path = path;
            this.ending_point = ending_point;

        }
    }
}












