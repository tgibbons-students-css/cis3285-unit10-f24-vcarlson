using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingleResponsibilityPrinciple.Contracts;
using SingleResponsibilityPrinciple;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple.Tests
{
    [TestClass()]
    public class URLTradeDataProviderTests
    {
        private int countStrings(IEnumerable<string> collectionOfStrings)
        {
            // Count the trades
            int count = 0;
            foreach (var trade in collectionOfStrings)
            {
                count++;
            }
            return count;
        }

        [TestMethod()]
        public async Task TestSize1()  // Marked as async
        {
            // Arrange
            ILogger logger = new ConsoleLogger();
            string tradeURL = "http://raw.githubusercontent.com/tgibbons-css/CIS3285_Unit9_F24/refs/heads/master/SingleResponsibilityPrinciple/trades.txt";

            ITradeDataProvider tradeProvider = new URLTradeDataProvider(tradeURL, logger);

            // Act
            IEnumerable<string> trades = await tradeProvider.GetTradeData();  // Await the async method

            // Assert
            Assert.AreEqual(countStrings(trades), 4);
        }
    }
}
