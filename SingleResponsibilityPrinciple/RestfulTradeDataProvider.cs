using SingleResponsibilityPrinciple.Contracts;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{
    public class RestfulTradeDataProvider : ITradeDataProvider
    {
        private readonly string url;
        private readonly ILogger logger;
        private readonly HttpClient client = new HttpClient();

        public RestfulTradeDataProvider(string url, ILogger logger)
        {
            this.url = url;
            this.logger = logger;
        }

        public async Task<IEnumerable<string>> GetTradeData()
        {
            logger.LogInfo("Connecting to the Restful server using HTTP");
            var tradeData = new List<string>();

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                tradeData.AddRange(content.Split("\n"));
                logger.LogInfo($"Received trade strings of length = {tradeData.Count}");
            }
            else
            {
                logger.LogWarning($"Failed to retrieve data. Status code: {response.StatusCode}");
            }

            return tradeData;
        }
    }
}
