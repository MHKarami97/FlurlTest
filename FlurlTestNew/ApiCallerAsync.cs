using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BaseNew;

namespace FlurlTestNew
{
    public sealed class ApiCallerAsync
    {
        private readonly TestAsync _client;
        private const string CallerKey = "CallerKey";
        private const string CallerKeyValue = "1";
        private static readonly Dictionary<string, string> Header = new Dictionary<string, string> { { CallerKey, CallerKeyValue } };

        public ApiCallerAsync(string api)
        {
            _client = new TestAsync(api);
        }

        public async Task<OutputModel> GetAsync(object query = null)
        {
            try
            {
                var result = await _client.SendAsHttpGetAsync<OutputModel>("/Customer/Get", query, Header);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("error", ex);
            }
        }
    }
}