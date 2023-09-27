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
            string[] input = File.ReadAllText("inputCorrect.txt").Split("\n");
            // lets get how many test cases we have
            int t = int.TryParse(input[0], out t) ? t : 0;
            int line = 1;
            //  15 Rooms
            for (int i = 1; i < 15; i++)
            {
                int[] inputValues = input[line].Split(" ").Select(int.Parse).ToArray();
                int roomHeight = inputValues[0];
                int roomWidth = inputValues[1];
                int roomDeviceNumber = inputValues[2];
                string[,] grid = Preset_grid(new string[roomHeight + 1, roomWidth + 1]);
                System.Console.WriteLine("-----------------");
                Show_grid(grid);
                System.Console.WriteLine("--------------->");
                for (int count = 0; count < roomDeviceNumber; count++)
                {
                    line++;
                    string[] deviceInfo = input[line].Split(" ");
                    int deviceHeight = int.TryParse(deviceInfo[0], out deviceHeight) ? deviceHeight : 0;
                    int deviceWidth = int.TryParse(deviceInfo[1], out deviceWidth) ? deviceWidth : 0;
                    string deviceName = deviceInfo[2];
                    System.Console.WriteLine("Device " + deviceName + " " + deviceHeight + " " + deviceWidth);

                    grid[deviceHeight, deviceWidth] = (deviceName);
                    System.Console.WriteLine("Device " + deviceName + " " + deviceHeight + " " + deviceWidth);
                }
                Show_grid(grid);

            }
        }


        public static string[,] Preset_grid(string[,] grid)
        {
            for (int i = 0; i < grid.GetLength(0); i++)

                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i, j] = " ";
                }
            return grid;
        }

        public static void Show_grid(string[,] grid)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    System.Console.Write(grid[i, j] + "|      |");
                }
            }
            // for (int i = 0; i < grid.GetLength(0); i++)
            // {
            //     for (int j = 0; j < grid.GetLength(1); j++)
            //     {
            //         System.Console.Write(grid[i, j] + "       ");
            //     }
            //     System.Console.WriteLine();
            // }
        }

        public static int[] checkConnected(int[,] grid, int cabelH, int cabelW, int device_identifier_count)
        {
            // we need to check if the cabel is connected to another device

            // Top corner
            if (cabelH - 1 >= 0)
            {
                if (grid[cabelH - 1, cabelW] == device_identifier_count)
                {
                    int[] pos = new int[2] { cabelH - 1, cabelW };
                    return pos;
                }
            }
            // Top Left corner
            if (cabelH - 1 >= 0 && cabelW - 1 >= 0)
            {
                if (grid[cabelH - 1, cabelW - 1] == device_identifier_count)
                {
                    int[] pos = new int[2] { cabelH - 1, cabelW - 1 };
                    return pos;
                }
            }
            // Top Right corner
            if (cabelH - 1 >= 0 && cabelW + 1 < grid.GetLength(1))
            {
                if (grid[cabelH - 1, cabelW + 1] == device_identifier_count)
                {
                    int[] pos = new int[2] { cabelH - 1, cabelW + 1 };
                    return pos;
                }
            }
            // Left corner
            if (cabelW - 1 >= 0)
            {
                if (grid[cabelH, cabelW - 1] == device_identifier_count)
                {
                    int[] pos = new int[2] { cabelH, cabelW - 1 };
                    return pos;
                }
            }
            // Right corner
            if (cabelW + 1 < grid.GetLength(1))
            {
                if (grid[cabelH, cabelW + 1] == device_identifier_count)
                {
                    int[] pos = new int[2] { cabelH, cabelW + 1 };
                    return pos;
                }
            }
            // Bottom corner
            if (cabelH + 1 < grid.GetLength(0))
            {
                if (grid[cabelH + 1, cabelW] == device_identifier_count)
                {
                    int[] pos = new int[2] { cabelH + 1, cabelW };
                    return pos;
                }
            }
            // Bottom Left corner
            if (cabelH + 1 < grid.GetLength(0) && cabelW - 1 >= 0)
            {
                if (grid[cabelH + 1, cabelW - 1] == device_identifier_count)
                {
                    int[] pos = new int[2] { cabelH + 1, cabelW - 1 };
                    return pos;
                }
            }
            // Bottom Right corner
            if (cabelH + 1 < grid.GetLength(0) && cabelW + 1 < grid.GetLength(1))
            {
                if (grid[cabelH + 1, cabelW + 1] == device_identifier_count)
                {
                    int[] pos = new int[2] { cabelH + 1, cabelW + 1 };
                    return pos;
                }
            }

            int[] posFail = new int[2] { -1, -1 };
            return posFail;

        }
        // paths  
        public static int[][] getStartingPoints(Dictionary<int, List<int[]>> devicePositions)
        {
            // Tuple<int[]> starting_points = new Tuple<int[]>(new int[2] { 0, 1 });
            int[][] starting_points = new int[devicePositions.Count][];

            System.Console.WriteLine("keys " + string.Join(",", devicePositions.Keys.ToArray()));
            for (int identifier = 1; identifier <= devicePositions.Count; identifier++)
            {
                System.Console.WriteLine("identifier " + identifier);
                List<int[]> devicePos = devicePositions[identifier];
                for (int unique = 0; unique < devicePos.Count; unique++)
                {
                    System.Console.WriteLine("Start pos " + string.Join(",", devicePos[unique]));

                }

            }
            return starting_points;

        }
        public static int[][] getInstructionPoints(Dictionary<int, List<int[]>> devicePositions, int[][] starting_points)
        {
            // int[] allIdentifiers = devicePositions.Keys.ToArray();

            // for (int i = 0; i < allIdentifiers.Length; i++)
            // {
            //     System.Console.WriteLine(allIdentifiers[i]);
            // }

            return null;
        }
        public static void Find_path(int[,] grid, int[] start, int[] end, int device_identifier, List<int[]> path, Dictionary<int, (List<int[]>, int[], int[], int[,])> pathList)
        {
            int[] check = checkConnected(grid, start[0], start[1], device_identifier);
            if (check != new int[2] { -1, -1 })
            {
                System.Console.WriteLine("Path " + string.Join(",", path));
                pathList.Add(device_identifier, (path, end, check, grid));
                return;
            }

            // PROTECTION
            // ----------
            // 
            // 
            // 
            if (start[0] - 1 >= 0)
            {
                // Its not past top side
                // Top corner
                if (grid[start[0] - 1, start[1]] == 0)
                {
                    grid[start[0] - 1, start[1]] = -1;
                    int[] pos = new int[2] { start[0] - 1, start[1] };
                    path.Add(pos);
                    System.Console.WriteLine("POS " + pos);
                    Find_path(grid, pos, end, device_identifier, path, pathList);
                }


            }

            // PROTECTION
            // ----------
            // -
            // -
            // -
            if (start[0] - 1 >= 0 && start[1] - 1 >= 0)
            {
                // Top Left corner
                if (grid[start[0] - 1, start[1] - 1] == 0)
                {
                    grid[start[0] - 1, start[1] - 1] = -1;
                    int[] pos = new int[2] { start[0] - 1, start[1] - 1 };
                    path.Add(pos);
                    System.Console.WriteLine("POS " + pos);
                    Find_path(grid, pos, end, device_identifier, path, pathList);
                }

            }

            // PROTECTION
            // ----------
            //          -
            //          -
            //          -
            if (start[0] - 1 >= 0 && start[1] + 1 <= grid.GetLength(1))
            {
                // Top Left corner
                if (grid[start[0] - 1, start[1] + 1] == 0)
                {
                    grid[start[0] - 1, start[1] + 1] = -1;
                    int[] pos = new int[2] { start[0] - 1, start[1] + 1 };
                    path.Add(pos);
                    System.Console.WriteLine("POS " + pos);
                    Find_path(grid, pos, end, device_identifier, path, pathList);
                }

            }

            // PROTECTION
            // -
            // -         
            // -         
            // -         
            if (start[1] - 1 >= 0)
            {
                // Left corner
                if (grid[start[0], start[1] - 1] == 0)
                {
                    grid[start[0], start[1] - 1] = -1;
                    int[] pos = new int[2] { start[0], start[1] - 1 };
                    path.Add(pos);
                    System.Console.WriteLine("POS " + pos);
                    Find_path(grid, pos, end, device_identifier, path, pathList);
                }

            }

            // PROTECTION
            //          -
            //          -         
            //          -         
            //          -         
            if (start[1] + 1 <= grid.GetLength(1))
            {
                // Right corner
                if (grid[start[0], start[1] + 1] == 0)
                {
                    grid[start[0], start[1] + 1] = -1;
                    int[] pos = new int[2] { start[0], start[1] + 1 };
                    path.Add(pos);
                    System.Console.WriteLine("POS " + pos);
                    Find_path(grid, pos, end, device_identifier, path, pathList);
                }

            }

            // PROTECTION
            //          -
            //          -         
            //          -         
            //-----------     
            if (start[0] + 1 <= grid.GetLength(0) && start[1] + 1 <= grid.GetLength(1))
            {
                // Bottom right corner
                if (grid[start[0] + 1, start[1] + 1] == 0)
                {
                    grid[start[0] + 1, start[1] + 1] = -1;
                    int[] pos = new int[2] { start[0] + 1, start[1] + 1 };
                    path.Add(pos);
                    System.Console.WriteLine("POS " + pos);
                    Find_path(grid, pos, end, device_identifier, path, pathList);
                }

            }

            // PROTECTION
            //-
            //-                   
            //-                   
            //-----------     
            if (start[0] + 1 <= grid.GetLength(0) && start[1] - 1 >= 0)
            {
                // Bottom left corner
                if (grid[start[0] + 1, start[1] - 1] == 0)
                {
                    grid[start[0] + 1, start[1] - 1] = -1;
                    int[] pos = new int[2] { start[0] + 1, start[1] - 1 };
                    path.Add(pos);
                    System.Console.WriteLine("POS " + pos);
                    Find_path(grid, pos, end, device_identifier, path, pathList);
                }

            }

            // PROTECTION
            //
            //                  
            //                 
            //-----------     
            if (start[0] + 1 <= grid.GetLength(0))
            {
                // Bottom corner
                if (grid[start[0] + 1, start[1]] == 0)
                {
                    grid[start[0] + 1, start[1]] = -1;
                    int[] pos = new int[2] { start[0] + 1, start[1] };
                    path.Add(pos);
                    System.Console.WriteLine("POS " + pos);
                    Find_path(grid, pos, end, device_identifier, path, pathList);
                }

            }
;
        }

        public static int findPath(int h, int w, int n)
        {

            return 0;
        }
    }




}

