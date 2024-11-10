using SingleResponsibilityPrinciple.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{
    public class URLAsyncProvider : ITradeDataProvider
    {
        private readonly ITradeDataProvider _baseTradeProvider;

        public URLAsyncProvider(ITradeDataProvider baseTradeProvider)
        {
            _baseTradeProvider = baseTradeProvider;
        }

        public async Task<IEnumerable<string>> GetTradeData()
        {
            // Return the result of the base provider’s GetTradeData asynchronously
            return await _baseTradeProvider.GetTradeData();
        }
    }
}
