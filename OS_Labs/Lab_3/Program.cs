using System.Diagnostics;

namespace Lab_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Boss");

            EventWaitHandle first = new EventWaitHandle(false, EventResetMode.ManualReset, "Read");
            EventWaitHandle second = new EventWaitHandle(false, EventResetMode.ManualReset, "Write");
            EventWaitHandle third = new EventWaitHandle(false, EventResetMode.ManualReset, "Delete");
            EventWaitHandle end = new EventWaitHandle(false, EventResetMode.ManualReset, "Exit");


            Semaphore sem = new Semaphore(3, 3, "sem");

            Console.Write("Количество процессов для запуска: ");

            var count = int.Parse(Console.ReadLine());

            bool flag = true;

            while (flag)
            {
                Console.Write("Введите пароль: ");

                string? password = Console.ReadLine();

                if (password == "123")
                {
                    ProcessStartInfo employeeInfo = new ProcessStartInfo()
                    {
                        FileName = @"D:\Study\Term 6\ОС\Labs\Term_6_OS\OS_Labs\Scout\bin\Debug\net6.0\Scout.exe",
                        UseShellExecute = true,
                    };

                    var employeeProcesses = new List<Process>();

                    for (int i = 0; i < count; i++)
                    {
                        var process = Process.Start(employeeInfo);

                        employeeProcesses.Add(process);
                    }

                    Console.WriteLine("Ожидание ...");

                    while (true)
                    {
                        int eventIndex = WaitHandle.WaitAny(new WaitHandle[] { first, second, third, end });

                        switch (eventIndex)
                        {
                            case 0:
                                Console.WriteLine("1");
                                first.Reset();
                                break;
                            case 1:
                                Console.WriteLine("2");
                                second.Reset();
                                break;
                            case 2:
                                Console.WriteLine("3");
                                third.Reset();
                                break;
                            case 3:
                                Console.WriteLine("Конец работы одного потока");
                                end.Reset();
                                break;
                            default:
                                break;
                        }
                    }

                    flag = false;
                }
                else
                {
                    Console.WriteLine("Неверный пароль, повторите попытку!");
                }
            }
        }
    }
}