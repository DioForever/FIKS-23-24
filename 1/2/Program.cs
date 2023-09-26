namespace NAS
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 5, 4 };
            int[] positions = { 1, 2 };

            //     position, value
            // Dictionary<int, List<int>> schedule = new Dictionary<int, List<int>>();
            List<int[]> schedule = new List<int[]>();
            for (int i = 1; i <= arr.Length; i++) schedule.Add(new int[] { arr[i - 1] });


            for (int i = 0; i < 10; i++)
            {
                System.Console.WriteLine("Iteration i" + i);
                System.Console.WriteLine("positions size " + positions.Length);
                for (int p = 0; p < positions.Length; p++)
                {
                    System.Console.WriteLine("Iteration p" + p);

                    int position = positions[p];
                    foreach (int value in schedule[position])
                    {
                        drawSchedule(schedule);
                        addValue(schedule, positions, position, value);
                        drawSchedule(schedule);
                    }
                }
            }
            // addValue(schedule, positions, 2, 4);


            // for (int k = 0; k < 200; k++){
            //     for (int i = 0; i < positions.Length; i++)
            //     {
            //         int position = positions[i];
            //         int value = schedule[position][0];
            //         int futurePosition = position + value;
            //         if (futurePosition < schedule.Count)
            //         {
            //             schedule[futurePosition] = schedule[futurePosition].Concat(new int[] { value }).ToArray();
            //         }
            //     }
            // }






        }

        /*
            k (minimal) = arr.Length()
            CANCELED --- What about making the numbers smaller by subtracting the smallest number-1 from all of them
            you go through the positions and when you get to one, you move it to the front and then you check if it's valid




            we take the smallest number out of them and check if the location of the ones around it are either empty or the number as in the original array


        */

        public static void drawSchedule(List<int[]> schedule)
        {
            //  Show the schedule
            for (int i = 0; i < schedule.Count; i++)
            {
                System.Console.Write(string.Join(", ", schedule[i]) + " | ");
            }
            System.Console.WriteLine("\n--------------------");
            for (int i = 0; i < schedule.Count; i++)
            {
                string space = new String(' ', Math.Abs((int)(schedule[i].Length * 1.5) - 1));
                System.Console.Write(space + (i + 1) + space + " | ");
            }
        }

        public static void addValue(List<int[]> schedule, int[] positions, int position, int value)
        {
            while (schedule.Count - 1 < position + value) schedule.Add(new int[] { });
            if (schedule[position + value].Length == 0)
            {
                schedule.Add(new int[] { value });
                System.Console.WriteLine("size " + (Array.IndexOf(positions, position)) + " - " + (position + value));
                System.Console.WriteLine("positions " + string.Join(", ", positions));
                positions[Array.IndexOf(positions, position)] = position + value;
            }
            else
            {
                schedule[position + value] = schedule[position + value].Concat(new int[] { value }).ToArray();
            }
        }

        public static int findPeriod(int[] arr)
        {

            return 0;
        }


        public static void updateAll(int[] positions, Dictionary<int, List<int>> schedule, int ammount)
        {
            for (int k = 0; k < ammount; k++)
            {
                for (int i = 0; i < positions.Length; i++)
                {
                    updateSpecific(positions, schedule, positions[i]);
                }
            }
        }

        public static void updateSpecific(int[] positions, Dictionary<int, List<int>> schedule, int position)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                System.Console.WriteLine("Double " + position);
                List<int> temp = new List<int>();

                System.Console.WriteLine("Checking " + (position + schedule[position][0]));
                if (schedule.ContainsKey(position + schedule[position][i]))
                {
                    temp = schedule[i];
                }

                temp.Add(schedule[position][i]);
                System.Console.WriteLine("added to " + (position + schedule[position][i]) + " - " + schedule[position][i]);
                schedule.Add(position + schedule[position][i], temp);


            }
            // schedule.Add(position + schedule[position], schedule[position]);
            // System.Console.WriteLine("added to " + (position + schedule[position]) + " - " + schedule[position]);
            // System.Console.WriteLine(schedule[position]);
            // positions[Array.IndexOf(positions, position)] = position + schedule[position];
        }
    }
}