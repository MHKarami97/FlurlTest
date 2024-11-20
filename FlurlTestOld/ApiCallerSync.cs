using System;
using System.Collections.Generic;
using Base;

namespace FlurlTestOld
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

        public OutputModel GetSync(object query = null)
        {
            try
            {
                var result = _client.SendAsHttpGetSync<OutputModel>("/Customer/Get", query, Header);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("error", ex);
            }
        }
        
        public OutputModel GetSyncWithResult(object query = null)
        {
            try
            {
                var result = _client.SendAsHttpGetSyncWithResult<OutputModel>("/Customer/Get", query, Header);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("error", ex);
            }
        }

        public OutputModel GetSyncWithConfigureAwait(object query = null)
        {
            try
            {
                var result = _client.SendAsHttpGetSyncWithConfigureAwait<OutputModel>("/Customer/Get", query, Header);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("error", ex);
            }
        }
    }
}