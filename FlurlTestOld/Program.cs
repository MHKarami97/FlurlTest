using System;

namespace FlurlTestOld
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string type = null;

            try
            {
                Console.WriteLine("Test Flurl 2.4.2 on .net framework 4.6.2");

                Console.WriteLine("Thread count:");
                var threadCount = Console.ReadLine();

                if (string.IsNullOrEmpty(threadCount) || !int.TryParse(threadCount, out var threads))
                {
                    throw new Exception("thread count is not valid");
                }

                Console.WriteLine("Show log (true/false):");
                var showLog = Console.ReadLine();

                if (string.IsNullOrEmpty(showLog) || !bool.TryParse(showLog, out var log))
                {
                    throw new Exception("show log is not valid");
                }

                Console.WriteLine("write y to exit");
                Console.WriteLine("--------------------------");

                var isFirstTime = true;
                do
                {
                    if (!isFirstTime)
                    {
                        Console.WriteLine("--------------------------");
                        Console.WriteLine("--------------------------");
                        Console.WriteLine("--------------------------");
                    }

                    Console.WriteLine("1: Sync, 2: Async, 3: Async with .ConfigureAwait(false), 4: Sync with .ConfigureAwait(false)");
                    type = Console.ReadLine();

                    LoadTestRunner loadTestRunner;

                    switch (type)
                    {
                        case "1":
                            loadTestRunner = new LoadTestRunner(threads, "http://localhost:3133", log);
                            loadTestRunner.RunSync();
                            break;

                        case "2":
                            loadTestRunner = new LoadTestRunner(threads, "http://localhost:3133", log);
                            loadTestRunner.RunAsync().GetAwaiter().GetResult();
                            break;

                        case "3":
                            loadTestRunner = new LoadTestRunner(threads, "http://localhost:3133", log);
                            loadTestRunner.RunAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                            break;

                        case "4":
                            loadTestRunner = new LoadTestRunner(threads, "http://localhost:3133", log);
                            loadTestRunner.RunSyncWithConfigureAwait();
                            break;

                        case "y":
                            break;

                        default:
                            throw new Exception("not valid type");
                    }

                    isFirstTime = false;
                } while (type != "y");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            if (type != "y")
            {
                Console.WriteLine("Press any key to exit...");
                Console.ReadLine();
            }
        }
    }
}