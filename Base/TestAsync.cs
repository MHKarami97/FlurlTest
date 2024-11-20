using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl.Http;

namespace Base
{
    public class TestAsync
    {
        private const int TimeOutOnSecond = 5;
        private readonly string _baseHttpAddress;

        public TestAsync(string baseHttpAddress)
        {
            _baseHttpAddress = baseHttpAddress;
        }

        public async Task<TResponse> SendAsHttpGetAsync<TResponse>(string url, object query = null, Dictionary<string, string> headers = null)
        {
            try
            {
                return await _baseHttpAddress
                    .WithHeaders(headers)
                    .WithTimeout(TimeSpan.FromSeconds(TimeOutOnSecond))
                    .AppendPathSegment(url)
                    .SetQueryParams(query)
                    .GetJsonAsync<TResponse>();
            }
            catch (FlurlHttpException ex)
            {
                var error = await ex.GetResponseStringAsync() ?? string.Empty;

                throw new Exception(error, ex);
            }
        }
    }
}