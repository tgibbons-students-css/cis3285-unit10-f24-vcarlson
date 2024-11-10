using SingleResponsibilityPrinciple.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{
    public class AdjustTradeDataProvider : ITradeDataProvider
    {
        private readonly ITradeDataProvider _urlTradeDataProvider;

        public AdjustTradeDataProvider(ITradeDataProvider urlTradeDataProvider)
        {
            _urlTradeDataProvider = urlTradeDataProvider;
        }

        public async Task<IEnumerable<string>> GetTradeData()
        {
            // Retrieve trade data from the URLTradeDataProvider asynchronously
            var tradeData = await _urlTradeDataProvider.GetTradeData();

            // Replace "GBP" with "EUR" in each trade data line
            var adjustedTradeData = tradeData.Select(line => line.Replace("GBP", "EUR")).ToList();

            return adjustedTradeData;
        }
    }
}
