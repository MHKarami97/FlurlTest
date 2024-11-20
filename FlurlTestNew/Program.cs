using System;
using Nito.AsyncEx;

namespace FlurlTestNew
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string type = null;

            try
            {
                const string version = "4.0.2";
                Console.WriteLine($"Test Flurl {version} on .net framework 4.6.2");

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

                Console.WriteLine("1: Sync\n" +
                                  "2: Async\n" +
                                  "3: Async with .ConfigureAwait(false)\n" +
                                  "4: Sync with .ConfigureAwait(false)\n" +
                                  "5: Sync with .Result\n" +
                                  "6: Sync with AsyncContext.Run\n" +
                                  "7: Run All");
                type = Console.ReadLine();

                LoadTestRunner loadTestRunner;

                Console.WriteLine("===========================");
                Console.WriteLine("Wait Until Test Complete...");

                switch (type)
                {
                    case "1":
                        loadTestRunner = new LoadTestRunner(threads, "http://localhost:3133", log);
                        loadTestRunner.RunSync();
                        break;

                    case "2":
                        loadTestRunner = new LoadTestRunner(threads, "http://localhost:3133", log);
                        loadTestRunner.RunAsync("Async").GetAwaiter().GetResult();
                        break;

                    case "3":
                        loadTestRunner = new LoadTestRunner(threads, "http://localhost:3133", log);
                        loadTestRunner.RunAsync("AsyncWithConfigureAwait").ConfigureAwait(false).GetAwaiter().GetResult();
                        break;

                    case "4":
                        loadTestRunner = new LoadTestRunner(threads, "http://localhost:3133", log);
                        loadTestRunner.RunSyncWithConfigureAwait();
                        break;

                    case "5":
                        loadTestRunner = new LoadTestRunner(threads, "http://localhost:3133", log);
                        loadTestRunner.RunSyncWithResult();
                        break;

                    case "6":
                        loadTestRunner = new LoadTestRunner(threads, "http://localhost:3133", log);
                        AsyncContext.Run(() => loadTestRunner.RunAsync("AsyncWithAsyncContext"));
                        break;

                    case "7":
                        loadTestRunner = new LoadTestRunner(threads, "http://localhost:3133", log);
                        loadTestRunner.RunSync();
                        loadTestRunner.RunSyncWithConfigureAwait();
                        loadTestRunner.RunSyncWithResult();
                        AsyncContext.Run(() => loadTestRunner.RunAsync("AsyncWithAsyncContext"));
                        loadTestRunner.RunAsync("Async").GetAwaiter().GetResult();
                        loadTestRunner.RunAsync("AsyncWithConfigureAwait").ConfigureAwait(false).GetAwaiter().GetResult();
                        break;

                    case "y":
                        break;

                    default:
                        throw new Exception("not valid type");
                }
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