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


            Dictionary<int, List<int[]>> devicePositions = new Dictionary<int, List<int[]>>();
            List<int[]> deviceOne = new List<int[]>();
            List<int[]> deviceTwo = new List<int[]>();
            deviceOne.Add(new int[2] { 0, 1 });
            deviceOne.Add(new int[2] { 1, 3 });
            deviceTwo.Add(new int[2] { 2, 0 });
            deviceTwo.Add(new int[2] { 0, 0 });
            devicePositions.Add(1, deviceOne);
            devicePositions.Add(2, deviceTwo);
            // Dictionary<int, (List<int[]>, int[], int[], int[,])> pathList = new Dictionary<int, (List<int[]>, int[], int[], int[,])>();



            int[,] grid = Preset_grid(new int[2 + 1, 3 + 1]);

            grid[0, 0] = 2;
            grid[2, 0] = 2;
            grid[1, 3] = 1;
            grid[0, 1] = 1;

            Show_grid(grid);


            // for (int i = 0; i < starting_points.Length; i++)
            // {
            //     System.Console.WriteLine(string.Join(",", starting_points[i]));

            //     Find_path(grid, starting_points[i], devicePositions.ElementAt(i).Value, devicePositions.ElementAt(i).Key, new List<int[]>(), pathList);
            //     System.Console.WriteLine("------");
            //     System.Console.WriteLine(pathList.Count);
            //     for (int k = 0; k < pathList.Count; k++)
            //     {
            //         Show_grid(pathList.ElementAt(k).Value.Item4);
            //         System.Console.WriteLine(string.Join(",", pathList.ElementAt(k).Value.Item1));
            //         System.Console.WriteLine("+++++++++++");
            //     }
            // }
            // foreach (int[] startingPair in starting_points)
            // {
            //     System.Console.WriteLine(string.Join(",", startingPair));
            //     Find_path(grid, startingPair, devicePositions[1], 1, new List<int[]>(), paths);
            // }

            // Find_path(grid, )
        }


        public static int[,] Preset_grid(int[,] grid)
        {
            for (int i = 0; i < grid.GetLength(0); i++)

                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i, j] = 0;
                }
            return grid;
        }

        public static void Show_grid(int[,] grid)
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

