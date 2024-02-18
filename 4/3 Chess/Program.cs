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

        for (int i = 0; i < enemiesCount; i++)
        {
            string[] enemyData = input[i + 2].Split();
            char type = enemyData[0][0];
            int enemyX = int.Parse(enemyData[1]);
            int enemyY = int.Parse(enemyData[2]);
            char direction = enemyData[3][0];

            map[enemyX, enemyY] = type.ToString();
            var rule = GetRule(type, enemyX, enemyY, direction);
            rules.Add(i, rule);

            Console.WriteLine($"Type: {type}, Position: ({enemyX}, {enemyY}), Direction: {direction}");
        }

        PrintMap(map, rules);


    }

    public static void PrintMap(string[,] map, Dictionary<int, Func<int, int, bool>> rules)
    {
        for (int x = 0; x < map.GetLength(1); x++)
        {
            for (int y = 0; y < map.GetLength(0); y++)
            {
                string type = map[y, x];
                if (type == null) type = "-";
                if (CheckRules(y, x, rules) == false) type = "X";
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