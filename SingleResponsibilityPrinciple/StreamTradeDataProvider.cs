using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SingleResponsibilityPrinciple.Contracts;

namespace SingleResponsibilityPrinciple
{
    public class StreamTradeDataProvider : ITradeDataProvider
    {
        private readonly Stream _stream;
        private readonly ILogger _logger;

        public StreamTradeDataProvider(Stream stream, ILogger logger)
        {
            _stream = stream;
            _logger = logger;
        }

        public async Task<IEnumerable<string>> GetTradeData()
        {
            var tradeData = new List<string>();
            _logger.LogInfo("Reading trades from file stream asynchronously.");

            using (var reader = new StreamReader(_stream))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    tradeData.Add(line);
                }
            }
            return tradeData;
        }
    }
}
