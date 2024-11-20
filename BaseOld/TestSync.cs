using System;
using System.Collections.Generic;
using Flurl.Http;

namespace Base
{
    public class TestSync
    {
        private const int TimeOutOnSecond = 5;
        private readonly string _baseHttpAddress;

        public TestSync(string baseHttpAddress)
        {
            _baseHttpAddress = baseHttpAddress;
        }

        public TResponse SendAsHttpGetSync<TResponse>(string url, object query = null, Dictionary<string, string> headers = null)
        {
            try
            {
                return _baseHttpAddress
                    .WithHeaders(headers)
                    .WithTimeout(TimeSpan.FromSeconds(TimeOutOnSecond))
                    .AppendPathSegment(url)
                    .SetQueryParams(query)
                    .GetJsonAsync<TResponse>()
                    .GetAwaiter().GetResult();
            }
            catch (FlurlHttpException ex)
            {
                var error = ex.GetResponseStringAsync().GetAwaiter().GetResult() ?? string.Empty;

                throw new Exception(error, ex);
            }
        }

        public TResponse SendAsHttpGetSyncWithConfigureAwait<TResponse>(string url, object query = null, Dictionary<string, string> headers = null)
        {
            try
            {
                return _baseHttpAddress
                    .WithHeaders(headers)
                    .WithTimeout(TimeSpan.FromSeconds(TimeOutOnSecond))
                    .AppendPathSegment(url)
                    .SetQueryParams(query)
                    .GetJsonAsync<TResponse>()
                    .ConfigureAwait(false).GetAwaiter().GetResult();
            }
            catch (FlurlHttpException ex)
            {
                var error = ex.GetResponseStringAsync().ConfigureAwait(false).GetAwaiter().GetResult() ?? string.Empty;

                throw new Exception(error, ex);
            }
        }
    }
}