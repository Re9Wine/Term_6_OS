namespace Scout
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Scout");

            Semaphore sem = Semaphore.OpenExisting("sem");

            EventWaitHandle first = EventWaitHandle.OpenExisting("Read");
            EventWaitHandle second = EventWaitHandle.OpenExisting("Write");
            EventWaitHandle third = EventWaitHandle.OpenExisting("Delete");
            EventWaitHandle end = EventWaitHandle.OpenExisting("Exit");

            sem.WaitOne();

            Console.WriteLine("Введите Read, Write, Delete или Exit для передачи серверу");

            while (true)
            {
                string? message = Console.ReadLine();

                switch (message)
                {
                    case "Read":
                        first.Set();
                        break;
                    case "Write":
                        second.Set();
                        break;
                    case "Delete":
                        third.Set();
                        break;
                    case "Exit":
                        end.Set();
                        sem.Release();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}