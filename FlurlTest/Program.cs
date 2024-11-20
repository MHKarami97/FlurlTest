using System;

namespace FlurlTest
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Test Flurl 4.0.2 on .net framework 4.6.2");

                Console.WriteLine("1: Sync, 2: Async, 3: Async with .ConfigureAwait(false), 4: Sync with .ConfigureAwait(false)");
                var type = Console.ReadLine();

                Console.WriteLine("Thread count:");
                var threadCount = Console.ReadLine();

                if (threadCount != null)
                {
                    var loadTestRunner = new LoadTestRunner(int.Parse(threadCount), "http://localhost:3133");

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
                }
                else
                {
                    throw new Exception("thread count is null");
                }
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