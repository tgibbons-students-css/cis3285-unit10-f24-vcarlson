using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingleResponsibilityPrinciple.Contracts;
using SingleResponsibilityPrinciple;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple.Tests
{
    [TestClass()]
    public class StreamTradeDataProviderTests
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
        public async Task TestSize1()  // Marked as async
        {
            // Arrange
            ILogger logger = new ConsoleLogger();
            string fileName = "SingleResponsibilityPrinciple.Tests.trades_1good.txt";  // Adjust resource name if necessary
            Stream tradeStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fileName);

            // Check if stream is null
            if (tradeStream == null)
            {
                throw new FileNotFoundException($"Resource {fileName} not found.");
            }

            ITradeDataProvider tradeProvider = new StreamTradeDataProvider(tradeStream, logger);

            // Act
            IEnumerable<string> trades = await tradeProvider.GetTradeData();  // Await the task

            // Assert
            Assert.AreEqual(countStrings(trades), 1);
        }

        [TestMethod()]
        public async Task TestSize5()  // Marked as async
        {
            // Arrange
            ILogger logger = new ConsoleLogger();
            string fileName = "SingleResponsibilityPrinciple.Tests.trades_5good.txt";  // Adjust resource name if necessary
            Stream tradeStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fileName);

            // Check if stream is null
            if (tradeStream == null)
            {
                throw new FileNotFoundException($"Resource {fileName} not found.");
            }

            ITradeDataProvider tradeProvider = new StreamTradeDataProvider(tradeStream, logger);

            // Act
            IEnumerable<string> trades = await tradeProvider.GetTradeData();  // Await the task

            // Assert
            Assert.AreEqual(countStrings(trades), 5);
        }
    }
}
