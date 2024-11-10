using SingleResponsibilityPrinciple.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{
    public class TradeProcessor
    {
        private readonly ITradeDataProvider _tradeDataProvider;
        private readonly ITradeParser _tradeParser;
        private readonly ITradeStorage _tradeStorage;

        public TradeProcessor(ITradeDataProvider tradeDataProvider, ITradeParser tradeParser, ITradeStorage tradeStorage)
        {
            _tradeDataProvider = tradeDataProvider;
            _tradeParser = tradeParser;
            _tradeStorage = tradeStorage;
        }

        public async Task ProcessTrades()
        {
            // Await asynchronous data retrieval
            var lines = await _tradeDataProvider.GetTradeData();

            // Parse trades (if this is async, await it)
            var trades = _tradeParser.Parse(lines); // No await needed unless Parse is async

            // Persist parsed trades asynchronously
            await _tradeStorage.Persist(trades);
        }
    }
}
