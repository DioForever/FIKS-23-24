namespace NAS
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 6, 9, 3 };
            int[] positions = { 1, 2, 3 };

            //     position, value
            Dictionary<int, int> schedule = new Dictionary<int, int>();
            for (int i = 1; i <= arr.Length; i++)
            {
                schedule.Add(i, arr[i - 1]);
            }

            int currentTime = 0;
            int lowestTime = 0;
            int highestValidPeriod = 0;

            bool valid = false;

            // while (!valid)
            // {

            // }
            updateAll(positions, schedule);
            System.Console.WriteLine(string.Join(" ", positions));
            System.Console.WriteLine(string.Join(" ", schedule));
            updateSpecific(positions, schedule, 11);
            System.Console.WriteLine(string.Join(" ", positions));
            System.Console.WriteLine(string.Join(" ", schedule));

            // for (int i = 0; i < positions.Length; i++)
            // {
            //     schedule.Add(positions[i] + schedule[positions[i]], schedule[positions[i]]);
            //     System.Console.WriteLine("added to " + (positions[i] + schedule[positions[i]]) + " - " + schedule[positions[i]]);
            //     System.Console.WriteLine(schedule[positions[i]]);
            //     positions[i] = positions[i] + schedule[positions[i]];

            // }


            // foreach (KeyValuePair<int, int> entry in schedule)
            // {
            //     System.Console.WriteLine(entry.Key + " " + entry.Value);
            //     schedule[entry.Key + entry.Value] = entry.Value;
            // }


            // while (valid)


        }

        /*
            k (minimal) = arr.Length()
            CANCELED --- What about making the numbers smaller by subtracting the smallest number-1 from all of them
            you go through the positions and when you get to one, you move it to the front and then you check if it's valid




            we take the smallest number out of them and check if the location of the ones around it are either empty or the number as in the original array


        */


        public static bool valid(int[] arr, int currentTime)
        {

            return true;
        }

        public static int findPeriod(int[] arr)
        {

            return 0;
        }


        public static void updateAll(int[] positions, Dictionary<int, int> schedule)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                updateSpecific(positions, schedule, positions[i]);
            }
        }

        public static void updateSpecific(int[] positions, Dictionary<int, int> schedule, int position)
        {
            schedule.Add(position + schedule[position], schedule[position]);
            System.Console.WriteLine("added to " + (position + schedule[position]) + " - " + schedule[position]);
            System.Console.WriteLine(schedule[position]);
            positions[Array.IndexOf(positions, position)] = position + schedule[position];
        }
    }
}