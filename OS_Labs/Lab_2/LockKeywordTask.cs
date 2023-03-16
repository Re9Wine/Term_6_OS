namespace Lab_2
{
    internal class LockKeywordTask
    {
        private static object syncToken = new object();

        public static void Execute()
        {
            int[] array = CreateArray();
            Console.WriteLine("Исходный массив:");
            DisplayArray(array);
            // Создание потоков для сортировки и отображения массива
            Thread sortingThread = new Thread(SortingThreadMain);
            Thread displayingThread = new Thread(DisplayingThreadMain);
            sortingThread.Start(array);
            Thread.Sleep(200);
            displayingThread.Start(array);

        }

        static void SortingThreadMain(object data)
        {
            // Проверка передачи корректных данных
            int[] array = data as int[];
            if (array == null)
            {
                return;
            }
            // Вход в критическую секцию
            lock (syncToken)
            {
                Console.WriteLine("Сортировка....");
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        int tmp;
                        if (array[j] > array[j + 1])
                        {
                            tmp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = tmp;
                            Thread.Sleep(200);
                        }
                    }
                }
            }
        }
        // Точка входа потока вывода массива
        private static void DisplayingThreadMain(object data)
        {
            int[] array = data as int[];
            if (array == null)
            {
                return;
            }
            // Вход в критическую секцию
            lock (syncToken)
            {
                DisplayArray(array);
                Console.ReadKey();
            }
        }
        // Создание массива и заполнение случайными числами от -99 до 99.
        private static int[] CreateArray()
        {
            Console.WriteLine("Введите размерность массива");
            string arraySizeString = Console.ReadLine();
            int arraySize;
            if (!int.TryParse(arraySizeString, out arraySize))
            {
                arraySize = 0;
            }
            int[] array = new int[arraySize];
            Random rand = new Random();
            for (int i = 0; i < arraySize; i++)
            {
                array[i] = -1 * rand.Next(99) + rand.Next(99);
            }
            return array;
        }
        private static void DisplayArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i] + " ");
            }
            Console.WriteLine();
        }

    }
}
