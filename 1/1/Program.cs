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
            for (int caseNumber = 1; caseNumber <= t; caseNumber++)
            {
                // we will take description of each test case and get height, width and how many devices there are 
                string[] description = input[line].Split(" ");
                int h = (int.TryParse(description[0], out h) ? h : 0) + 1;
                int w = (int.TryParse(description[1], out w) ? w : 0) + 1;
                int n = int.TryParse(description[2], out n) ? n : 0;
                //  h = height, w = width, n = number of devices
                System.Console.WriteLine("Case #" + caseNumber + ":" + " line: " + (line + 1));
                // System.Console.WriteLine(input[line]);


                Dictionary<string, int> device_identifiers = new Dictionary<string, int>();
                Dictionary<string, int> device_count = new Dictionary<string, int>();
                int device_identifier_count = 1;
                // now we need to create a grid with the height and width we got from the description
                int[,] grid = new int[h, w];
                grid = preset_grid(grid, h, w);
                string[] devices = new string[n];


                // System.Console.WriteLine(t + " " + h + " " + w + " " + n);
                for (int i = 0; i <= n; i++)
                {

                    string[] device = input[line].Split(" ");
                    int deviceH = int.TryParse(device[0], out deviceH) ? deviceH : 0;
                    int deviceW = int.TryParse(device[1], out deviceW) ? deviceW : 0;
                    // System.Console.WriteLine(deviceH + " " + deviceW + " " + device[2]);
                    if (!device_identifiers.ContainsKey(device[2]))
                    {
                        // we havent seen this device before so we set its identifier
                        device_identifiers.Add(device[2], device_identifier_count);
                        device_identifier_count++;
                        device_count.Add(device[2], 1);
                    }
                    // devices[i] = device[0] + " " + device[1] + " " + device[2];
                    grid[deviceH, deviceW] = device_identifiers[device[2]];
                    line++;
                }


                if (!even_devices(device_count))
                {
                    // System.Console.WriteLine("Devices " + String.Join(" ", devices));
                    System.Console.WriteLine("ajajaj ");
                    correct++;
                    // System.Console.WriteLine(String.Join("\n", devices));
                    continue;
                }

            }
            System.Console.WriteLine("Correct: " + correct + " out of " + t);
        }

        public static bool even_devices(Dictionary<string, int> device_count)
        {
            foreach (KeyValuePair<string, int> device in device_count)
            {
                if (device.Value > 2)
                {
                    // System.Console.WriteLine("MORE THAN EVEN" + " " + device.Key + " " + device.Value);
                }
                if (device.Value % 2 != 0)
                {
                    System.Console.WriteLine("NOT EVEN" + " " + device.Key + " " + device.Value);
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

        public static void manhatan_distance(int n, int[,] grid, int h, int w)
        {

        }

    }
}












