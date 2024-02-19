using System;

class Chess
{
    static void Main()
    {
        string[] input = File.ReadAllLines("input.txt");


        string[] dimensions = input[0].Split();
        int width = int.Parse(dimensions[0]);
        int height = int.Parse(dimensions[1]);
        int enemiesCount = int.Parse(dimensions[2]);
        string[,] map = new string[height, width];

        string[] frodoCoords = input[1].Split();
        int frodoX = int.Parse(frodoCoords[0]);
        int frodoY = int.Parse(frodoCoords[1]);
        map[frodoX, frodoY] = "F";

        Dictionary<int, Func<int, int, bool>> rules = new Dictionary<int, Func<int, int, bool>>();
        Dictionary<int, location> enemyLocs = new Dictionary<int, location>();

        for (int i = 0; i < enemiesCount; i++)
        {
            string[] enemyData = input[i + 2].Split();
            char type = enemyData[0][0];
            int enemyX = int.Parse(enemyData[1]);
            int enemyY = int.Parse(enemyData[2]);
            char direction = enemyData[3][0];

            map[enemyX, enemyY] = i.ToString();
            var rule = GetRule(type, enemyX, enemyY, direction);
            rules.Add(i, rule);
            location loc = new location(enemyX, enemyY);
            enemyLocs.Add(i, loc);

            Console.WriteLine($"Type: {type}, Position: ({enemyX}, {enemyY}), Direction: {direction}");
        }

        PrintMap(map, rules);

        int highestKS = 0;
        highestKS = FindHighestKillStreak(map, rules, enemyLocs, frodoX, frodoY, highestKS);

        System.Console.WriteLine($"HighestKS = {highestKS}");

    }

    public static int FindHighestKillStreak(string[,] map, Dictionary<int, Func<int, int, bool>> rules, Dictionary<int, location> enemyLocs, int fx, int fy, int highestKillStreak)
    {
        // 
        int currLocX = fx;
        int currLocY = fy;

        if (!Chess.CheckRules(fx, fy, rules)) return highestKillStreak;


        for (int k = 1; k < enemyLocs.Count; k++)
        {
            int i = enemyLocs.Keys.ToArray()[k];
            int ex = enemyLocs[i].x;
            int ey = enemyLocs[i].y;
            if (Pathfinding.FindPath(currLocX, currLocY, ex, ey, map, rules))
            {
                int newHKS = 0;
                newHKS = highestKillStreak + 1;
                System.Console.WriteLine($"Path to {i}");
                string[,] mapMod = new string[map.GetLength(0), map.GetLength(1)];
                mapMod = map;
                mapMod[ex, ey] = "-";

                Dictionary<int, Func<int, int, bool>> rulesMod = new Dictionary<int, Func<int, int, bool>>();
                rulesMod = rules;
                rulesMod.Remove(i);

                Dictionary<int, location> enemyLocsMod = new Dictionary<int, location>();
                enemyLocsMod = enemyLocs;
                enemyLocs.Remove(i);

                int outcome = FindHighestKillStreak(mapMod, rulesMod, enemyLocsMod, ex, ey, newHKS);
                if (outcome > highestKillStreak) highestKillStreak = outcome;
            }
        }
        return highestKillStreak;
    }


    public static void PrintMap(string[,] map, Dictionary<int, Func<int, int, bool>> rules)
    {
        for (int y = 0; y < map.GetLength(1); y++)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                string type = map[x, y];
                if (type == null) type = "-";
                Console.Write($"{type} ");
            }
            System.Console.WriteLine();
        }
    }




    public static bool CheckRules(int x, int y, Dictionary<int, Func<int, int, bool>> rules)
    {
        for (int i = 0; i < rules.Keys.Count; i++)
        {
            int key = rules.Keys.ToArray()[i];

            if (rules[key](x, y) == false) return false;
        }
        return true;
    }

    public static Func<int, int, bool> GetRule(char type, int ex, int ey, char direction)
    {
        switch (type)
        {
            case 'Z': // Zaporak
                return (fx, fy) => CheckZaporak(ex, ey, fx, fy, direction);

            case 'C': // CD Mechanika
                return (fx, fy) => CheckCDMechanika(ex, ey, fx, fy, direction);

            case 'T': // Trojsky kun
                return (fx, fy) => CheckTrojskyKun(ex, ey, fx, fy);

            case 'V': // Vysilac
                return (fx, fy) => CheckVysilac(ex, ey, fx, fy);

            default:
                throw new ArgumentException("Invalid type");
        }
    }
    public static bool CheckZaporak(int ex, int ey, int fx, int fy, char direction)
    {
        if (ex == fx && ey == fy) return true;
        switch (direction)
        {
            case 'J':
                if ((fx == ex - 1 && fy == ey - 1) || (fx == ex + 1 && fy == ey - 1)) return false;
                break;
            case 'S':
                if ((fx == ex - 1 && fy == ey + 1) || (fx == ex + 1 && fy == ey + 1)) return false;
                break;
            case 'V':
                if ((fx == ex + 1 && fy == ey - 1) || (fx == ex + 1 && fy == ey + 1)) return false;
                break;
            case 'Z':
                if ((fx == ex - 1 && fy == ey - 1) || (fx == ex - 1 && fy == ey + 1)) return false;
                break;
            default:
                throw new ArgumentException("Invalid type");
        }
        return true;
    }
    public static bool CheckCDMechanika(int ex, int ey, int fx, int fy, char direction)
    {
        if (ex == fx && ey == fy) return true;
        switch (direction)
        {
            case 'J':
                if (fx == ex && fy <= ey) return false;
                break;
            case 'S':
                if (fx == ex && fy >= ey) return false;
                break;
            case 'Z':
                if (fx <= ex && fy == ey) return false;
                break;
            case 'V':
                if (fx >= ex && fy == ey) return false;
                break;
            default:
                throw new ArgumentException("Invalid type");
        }
        return true;
    }
    public static bool CheckTrojskyKun(int ex, int ey, int fx, int fy)
    {
        if (ex == fx && ey == fy) return true;

        if (fy - 2 == ey && fx + 1 == ex) return false;
        if (fy - 2 == ey && fx - 1 == ex) return false;

        if (fy - 1 == ey && fx + 2 == ex) return false;
        if (fy - 1 == ey && fx - 2 == ex) return false;

        if (fy + 1 == ey && fx - 2 == ex) return false;
        if (fy + 1 == ey && fx + 2 == ex) return false;

        if (fy + 2 == ey && fx + 1 == ex) return false;
        if (fy + 2 == ey && fx - 1 == ex) return false;

        return true;
    }
    public static bool CheckVysilac(int ex, int ey, int fx, int fy)
    {
        if (ex == fx && ey == fy) return true;

        // int Absfx = Math.Abs(fx);
        // int Absfy = Math.Abs(fy);
        int diffX = Math.Abs(ex - fx);
        int diffY = Math.Abs(ey - fy);
        if (diffX == diffY) return false;
        return true;
    }

}


public static class Pathfinding
{
    public static bool FindPath(int fx, int fy, int ex, int ey, string[,] map, Dictionary<int, Func<int, int, bool>> rules)
    {
        if (!rules[0](fx, fy) || !rules[0](ex, ey))
            return false;

        int mapWidth = map.GetLength(1);
        int mapHeight = map.GetLength(0);

        HashSet<(int, int)> closedList = new HashSet<(int, int)>();

        PriorityQueue<(int, int), int> openList = new PriorityQueue<(int, int), int>();
        openList.Enqueue((fx, fy), GetDistance((fx, fy), (ex, ey)));

        while (openList.Count > 0)
        {
            var current = openList.Dequeue();
            int cx = current.Item1;
            int cy = current.Item2;

            if (cx == ex && cy == ey)
                return true;

            closedList.Add((cx, cy));

            // Define movement directions: UP, RIGHT, DOWN, LEFT
            int[] dx = { 0, 1, 0, -1 };
            int[] dy = { -1, 0, 1, 0 };

            for (int i = 0; i < 4; i++)
            {
                int nx = cx + dx[i];
                int ny = cy + dy[i];

                if (nx >= 0 && nx < mapWidth && ny >= 0 && ny < mapHeight && Chess.CheckRules(nx, ny, rules) && !closedList.Contains((nx, ny)) && Chess.CheckRules(nx, ny, rules))
                {
                    int tentativeGScore = GetDistance((fx, fy), (nx, ny));

                    if (!openList.Contains((nx, ny)) || tentativeGScore < openList.GetPriority((nx, ny)))
                    {
                        openList.Enqueue((nx, ny), tentativeGScore + GetDistance((nx, ny), (ex, ey)));
                    }
                }
            }
        }

        return false;
    }

    private static int GetDistance((int, int) a, (int, int) b)
    {
        int dx = Math.Abs(a.Item1 - b.Item1);
        int dy = Math.Abs(a.Item2 - b.Item2);
        return dx + dy; // Manhattan distance for non-diagonal movement
    }
}

public class PriorityQueue<T, U> where U : IComparable<U>
{
    private readonly SortedDictionary<U, Queue<T>> dict = new SortedDictionary<U, Queue<T>>();

    public int Count { get; private set; }

    public void Enqueue(T item, U priority)
    {
        if (!dict.ContainsKey(priority))
        {
            dict[priority] = new Queue<T>();
        }

        dict[priority].Enqueue(item);
        Count++;
    }

    public T Dequeue()
    {
        var item = dict.First();
        var queue = item.Value;
        var dequeued = queue.Dequeue();
        if (queue.Count == 0)
        {
            dict.Remove(item.Key);
        }
        Count--;
        return dequeued;
    }

    public bool Contains(T item)
    {
        return dict.Values.Any(queue => queue.Contains(item));
    }

    public U GetPriority(T item)
    {
        foreach (var kvp in dict)
        {
            if (kvp.Value.Contains(item))
            {
                return kvp.Key;
            }
        }
        throw new InvalidOperationException("Item not found in queue");
    }
}


class location
{
    public int x;
    public int y;
    public location(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

public class Node
{
    public int x;
    public int y;
    public int gCost;
    public int hCost;
    public int fCost { get { return gCost + hCost; } }
    public Node parent;

    public Node(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}