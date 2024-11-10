using Microsoft.Data.SqlClient;
using SingleResponsibilityPrinciple.Contracts;
using SingleResponsibilityPrinciple;

namespace SingleResponsibilityPrinciple.Tests
{
    [TestClass()]
    public class RestfulTradeDataProviderTests
    {
        private int countStrings(IEnumerable<string> collectionOfStrings)
        {
            // count the trades
            int count = 0;
            foreach (var trade in collectionOfStrings)
            {
                count++;
            }
            return count;
        }

        [TestMethod()]
        public async Task TestSize3()
        {
            //Arrange
            ILogger logger = new ConsoleLogger();
            string restfulURL = "http://unit9trader.azurewebsites.net/api/TradeData";

            ITradeDataProvider tradeProvider = new RestfulTradeDataProvider(restfulURL, logger);

            //Act
            IEnumerable<string> trades = await tradeProvider.GetTradeData();  // Await the async method

            //Assert
            Assert.AreEqual(countStrings(trades), 3);
        }
    }
}
