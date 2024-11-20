using System;
using System.Collections.Generic;
using Base;

namespace FlurlTest
{
    public sealed class ApiCallerSync
    {
        private readonly TestSync _client;
        private const string CallerKey = "CallerKey";
        private const string CallerKeyValue = "1";
        private static readonly Dictionary<string, string> Header = new Dictionary<string, string> { { CallerKey, CallerKeyValue } };

        public ApiCallerSync(string api)
        {
            _client = new TestSync(api);
        }

        public OutputModel GetSync(int customerId)
        {
            try
            {
                var result = _client.SendAsHttpGetSync<OutputModel>("/Customer/Get", customerId, Header);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("error", ex);
            }
        }

        public OutputModel GetSyncWithConfigureAwait(int customerId)
        {
            try
            {
                var result = _client.SendAsHttpGetSyncWithConfigureAwait<OutputModel>("/Customer/Get", customerId, Header);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("error", ex);
            }
        }
    }
}