namespace Lab_2
{
    internal class IndividualTask
    {
        private static object locker = new();
        private static Random random = new Random();

        public static void Execute()
        {
            Console.WriteLine("Введите длинну массива");

            int arrayLength;

            if (!int.TryParse(Console.ReadLine(), out arrayLength))
            {
                arrayLength = 0;
            }

            int[] array = SetArrayItems(arrayLength);

            PrintArray(array);

            Thread worker1 = new Thread(ThrerdWorker1);
            Thread worker2 = new Thread(ThrerdWorker2);

            worker1.Start(array);
            worker2.Start(array);
        }

        private static void ThrerdWorker1(object data)
        {
            int[]? array = data as int[];

            if (array == null)
            {
                return;
            }

            lock (locker)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        array[i] += 10;
                    }
                }

                PrintArray(array);
            }
        }

        private static void ThrerdWorker2(object data)
        {
            int[]? array = data as int[];

            if (array == null)
            {
                return;
            }

            lock (locker)
            {
                int minElement = array.Min();
                int maxElement = array.Max();

                Console.WriteLine("Индекс минимального элемента массива = {0}, эелемент = {1}", Array.IndexOf(array, minElement), minElement);
                Console.WriteLine("Индекс максимального элемента массива = {0}, эелемент = {1}", Array.IndexOf(array, maxElement), maxElement);
            }
        }

        private static int[] SetArrayItems(int length)
        {
            var array = new int[length];

            for (int i = 0; i < length; i++)
            {
                array[i] = -1 * random.Next(99) + random.Next(99);
            }

            return array;
        }

        private static void PrintArray(int[] array)
        {
            foreach (var item in array)
            {
                Console.Write(item.ToString() + "\t");
            }

            Console.WriteLine();
        }
    }
}
