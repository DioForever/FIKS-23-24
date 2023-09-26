namespace NAS
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 6, 9, 3 };

            //     position, value
            Dictionary<int, int> dict = new Dictionary<int, int>();
            for (int i = 1; i <= arr.Length; i++)
            {
                dict.Add(i, 1);
            }

            int currentTime = 0;

            // while (valid)


        }

        /*
            k (minimal) = arr.Length()
            CANCELED --- What about making the numbers smaller by subtracting the smallest number-1 from all of them
            we take the smallest number out of them and check if the location of the ones around it are either empty or the number as in the original array


        */


        public static bool valid(int[] arr, int currentTime)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (currentTime % arr[i] != 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}