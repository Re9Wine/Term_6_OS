namespace Lab_1
{
    internal class MyThread
    {
        public int count;
        public int repetitionCount;
        public Thread thrd;
        static bool stop = false;
        static string currentName;

        public MyThread(string name, int repetitionCount)
        {
            count = 0;
            thrd = new Thread(new ThreadStart(this.run));
            thrd.Name = name;
            currentName = name;
            this.repetitionCount = repetitionCount;
        }

        void run()
        {
            Console.WriteLine("Поток " + thrd.Name + " стартовал. ");
            do
            {
                count++;
            } while (stop == false && count < repetitionCount);
            stop = true;
        }

    }
}
