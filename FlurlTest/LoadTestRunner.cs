using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlurlTest
{
    public class LoadTestRunner
    {
        private readonly ApiCallerSync _callerSync;
        private readonly ApiCallerAsync _callerAsync;
        private readonly int _threadCount;

        public LoadTestRunner(int threadCount, string api)
        {
            _threadCount = threadCount;
            _callerSync = new ApiCallerSync(api);
            _callerAsync = new ApiCallerAsync(api);
        }

        public void RunSync()
        {
            try
            {
                var responses = new ConcurrentQueue<string>();
                var responseTimes = new ConcurrentQueue<TimeSpan>();
                var errors = new ConcurrentQueue<Exception>();

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                Console.Clear();
                Console.WriteLine("Wait Until Test Complete...");

                var tasks = new List<Task>();

                for (var i = 0; i < _threadCount; i++)
                {
                    Thread.Sleep(1);

                    tasks.Add(Task.Run(() =>
                    {
                        var localStopwatch = new Stopwatch();
                        localStopwatch.Start();

                        try
                        {
                            _ = _callerSync.GetSync(1);

                            responses.Enqueue("ok");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);

                            errors.Enqueue(ex);
                            responses.Enqueue("error");
                        }

                        localStopwatch.Stop();
                        responseTimes.Enqueue(localStopwatch.Elapsed);
                    }));
                }

                Task.WhenAll(tasks).GetAwaiter().GetResult();

                stopwatch.Stop();

                Console.Clear();
                Console.WriteLine("----------");
                Console.WriteLine($"Thread Count: {_threadCount}");
                Console.WriteLine($"Total responses: {responses.Count}");
                Console.WriteLine($"Total exception: {errors.Count}");
                Console.WriteLine("----------");
                Console.WriteLine($"Total elapsed time: {stopwatch.Elapsed}");
                Console.WriteLine($"Avg responseTime: {responseTimes.Average(r => r.TotalMilliseconds)}");
                Console.WriteLine($"Max responseTime: {responseTimes.Max(r => r.TotalMilliseconds)}");
                Console.WriteLine($"Min responseTime: {responseTimes.Min(r => r.TotalMilliseconds)}");
                Console.WriteLine("----------");
                Console.WriteLine($"Total ok responses: {responses.Count(r => r == "ok")}");
                Console.WriteLine($"Total error responses: {responses.Count(r => r == "error")}");
                Console.WriteLine("----------");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void RunSyncWithConfigureAwait()
        {
            try
            {
                var responses = new ConcurrentQueue<string>();
                var responseTimes = new ConcurrentQueue<TimeSpan>();
                var errors = new ConcurrentQueue<Exception>();

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                Console.Clear();
                Console.WriteLine("Wait Until Test Complete...");

                var tasks = new List<Task>();

                for (var i = 0; i < _threadCount; i++)
                {
                    Thread.Sleep(1);

                    tasks.Add(Task.Run(() =>
                    {
                        var localStopwatch = new Stopwatch();
                        localStopwatch.Start();

                        try
                        {
                            _ = _callerSync.GetSyncWithConfigureAwait(1);

                            responses.Enqueue("ok");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);

                            errors.Enqueue(ex);
                            responses.Enqueue("error");
                        }

                        localStopwatch.Stop();
                        responseTimes.Enqueue(localStopwatch.Elapsed);
                    }));
                }

                Task.WhenAll(tasks).ConfigureAwait(false).GetAwaiter().GetResult();

                stopwatch.Stop();

                Console.Clear();
                Console.WriteLine("----------");
                Console.WriteLine($"Thread Count: {_threadCount}");
                Console.WriteLine($"Total responses: {responses.Count}");
                Console.WriteLine($"Total exception: {errors.Count}");
                Console.WriteLine("----------");
                Console.WriteLine($"Total elapsed time: {stopwatch.Elapsed}");
                Console.WriteLine($"Avg responseTime: {responseTimes.Average(r => r.TotalMilliseconds)}");
                Console.WriteLine($"Max responseTime: {responseTimes.Max(r => r.TotalMilliseconds)}");
                Console.WriteLine($"Min responseTime: {responseTimes.Min(r => r.TotalMilliseconds)}");
                Console.WriteLine("----------");
                Console.WriteLine($"Total ok responses: {responses.Count(r => r == "ok")}");
                Console.WriteLine($"Total error responses: {responses.Count(r => r == "error")}");
                Console.WriteLine("----------");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task RunAsync()
        {
            try
            {
                var responses = new ConcurrentQueue<string>();
                var responseTimes = new ConcurrentQueue<TimeSpan>();
                var errors = new ConcurrentQueue<Exception>();

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                Console.Clear();
                Console.WriteLine("Wait Until Test Complete...");

                var tasks = new List<Task>();

                for (var i = 0; i < _threadCount; i++)
                {
                    Thread.Sleep(1);

                    tasks.Add(Task.Run(async () =>
                    {
                        var localStopwatch = new Stopwatch();
                        localStopwatch.Start();

                        try
                        {
                            _ = await _callerAsync.GetAsync(1);

                            responses.Enqueue("ok");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);

                            errors.Enqueue(ex);
                            responses.Enqueue("error");
                        }

                        localStopwatch.Stop();
                        responseTimes.Enqueue(localStopwatch.Elapsed);
                    }));
                }

                await Task.WhenAll(tasks);

                stopwatch.Stop();

                Console.Clear();
                Console.WriteLine("----------");
                Console.WriteLine($"Thread Count: {_threadCount}");
                Console.WriteLine($"Total responses: {responses.Count}");
                Console.WriteLine($"Total exception: {errors.Count}");
                Console.WriteLine("----------");
                Console.WriteLine($"Total elapsed time: {stopwatch.Elapsed}");
                Console.WriteLine($"Avg responseTime: {responseTimes.Average(r => r.TotalMilliseconds)}");
                Console.WriteLine($"Max responseTime: {responseTimes.Max(r => r.TotalMilliseconds)}");
                Console.WriteLine($"Min responseTime: {responseTimes.Min(r => r.TotalMilliseconds)}");
                Console.WriteLine("----------");
                Console.WriteLine($"Total ok responses: {responses.Count(r => r == "ok")}");
                Console.WriteLine($"Total error responses: {responses.Count(r => r == "error")}");
                Console.WriteLine("----------");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}