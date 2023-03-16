using System.Diagnostics;
using System.Threading;

namespace Lab_1
{
    internal class Program
    {
        static void Main(string[] args)
        {

        }

        static void Task_2()
        {
            Process newProc = Process.Start(@"D:\Soft\7-Zip\7zFM.exe");
            Console.WriteLine("Новый процесс стартовал.");
            newProc.WaitForExit();
            newProc.Close();

        }

        static void Task_3()
        {
            int repetitionCount = 5000000;

            MyThread mt = new MyThread("с нормальным приоритетом", repetitionCount);
            MyThread mt_1 = new MyThread("с увеличенным на 1 приоритетом", repetitionCount);
            MyThread mt_2 = new MyThread("с увеличенным на 2 приоритетом", repetitionCount);

            mt.thrd.Priority = ThreadPriority.Normal;
            mt_1.thrd.Priority = ThreadPriority.AboveNormal;
            mt_2.thrd.Priority = ThreadPriority.Highest;

            mt.thrd.Start();
            mt_1.thrd.Start();
            mt_2.thrd.Start();

            mt.thrd.Join();
            mt_1.thrd.Join();
            mt_2.thrd.Join();

            Console.WriteLine();
            Console.WriteLine("Поток " + mt.thrd.Name + " достичал до - " + mt.count);
            Console.WriteLine("Поток " + mt_1.thrd.Name + " достичал до - " + mt_1.count);
            Console.WriteLine("Поток " + mt_2.thrd.Name + " достичал до - " + mt_2.count);

        }
    }
}