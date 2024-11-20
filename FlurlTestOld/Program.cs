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
                Console.WriteLine("1: Sync, 2: Async, 3: Async with .ConfigureAwait(false), 4: Sync with .ConfigureAwait(false)");
                var type = Console.ReadLine();

                var loadTestRunner = new LoadTestRunner(1000, "http://localhost:5000");

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
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}