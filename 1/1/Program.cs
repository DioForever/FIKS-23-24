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
            for (int line = 1; line <= t; line++)
            {
                // we will take description of each test case and get height, width and how many devices there are 
                string[] description = input[1].Split(" ");
                int h = (int.TryParse(description[0], out h) ? h : 0) + 1;
                int w = (int.TryParse(description[1], out w) ? w : 0) + 1;
                int n = int.TryParse(description[2], out n) ? n : 0;
                //  h = height, w = width, n = number of devices

                System.Console.WriteLine("" + t + " " + h + " " + w + " " + n);


                Dictionary<string, int> device_identifiers = new Dictionary<string, int>();
                int device_identifier_count = 1;
                // now we need to create a grid with the height and width we got from the description
                int[,] grid = new int[h, w];
                grid = preset_grid(grid, h, w);




                for (int i = 0; i < n; i++)
                {
                    // we will take each device and get its height and width
                    string[] device = input[2 + i].Split(" ");
                    int deviceH = int.TryParse(device[0], out deviceH) ? deviceH : 0;
                    int deviceW = int.TryParse(device[1], out deviceW) ? deviceW : 0;
                    // deviceH = device height, deviceW = device width
                    if (!device_identifiers.ContainsKey(device[2]))
                    {
                        // we havent seen this device before so we set its identifier
                        device_identifiers.Add(device[2], device_identifier_count);
                        device_identifier_count++;
                    }
                    grid[deviceH, deviceW] = device_identifiers[device[2]];
                    System.Console.WriteLine("" + deviceH + " " + deviceW + " " + device[2]);
                }
                line += n;

                show_grid(grid);
            }
        }

        public void even_devices()
        {

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

    }
}












