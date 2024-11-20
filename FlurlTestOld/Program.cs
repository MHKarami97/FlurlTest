using System;

namespace FlurlTestOld
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Test Flurl 2.4.2 on .net framework 4.6.2");
                Console.WriteLine("Thread count:");
                var threadCount = Console.ReadLine();

                if (string.IsNullOrEmpty(threadCount) || !int.TryParse(threadCount, out var threads))
                {
                    throw new Exception("thread count is not valid");
                }

                Console.WriteLine("write y to exit");

                string type;
                do
                {
                    Console.WriteLine("=-------------=");
                    Console.WriteLine("=-------------=");
                    Console.WriteLine("=-------------=");

                    Console.WriteLine("1: Sync, 2: Async, 3: Async with .ConfigureAwait(false), 4: Sync with .ConfigureAwait(false)");
                    type = Console.ReadLine();

                    var loadTestRunner = new LoadTestRunner(threads, "http://localhost:3133");

                    switch (type)
                    {
                        case "1":
                            loadTestRunner.RunSync();
                            break;

                        case "2":
                            loadTestRunner.RunAsync().GetAwaiter().GetResult();
                            break;

                        case "3":
                            loadTestRunner.RunAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                            break;

                        case "4":
                            loadTestRunner.RunSyncWithConfigureAwait();
                            break;

                        default:
                            throw new Exception("not valid type");
                    }
                } while (type != "y");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}