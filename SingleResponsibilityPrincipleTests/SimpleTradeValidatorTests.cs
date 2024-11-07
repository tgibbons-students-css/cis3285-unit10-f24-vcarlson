using Microsoft.Data.SqlClient;
using SingleResponsibilityPrinciple.Contracts;
using SingleResponsibilityPrinciple;

namespace SingleResponsibilityPrinciple.Tests
{
    [TestClass()]
    public class SimpleTradeValidatorTests
    {
        [TestMethod()]
        public void TestGoodCurrencyString()
        {
            //Arrange
            var logger = new ConsoleLogger();
            var tradeValidator = new SimpleTradeValidator(logger);
            string[] strData = { "AAABBB", "4444", "1.00" };

            //Act
            bool result = tradeValidator.Validate(strData);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void TestShortCurrencyString()
        {
            //Arrange
            var logger = new ConsoleLogger();
            var tradeValidator = new SimpleTradeValidator(logger);
            string[] strData = { "AAA", "4444", "1.00" };

            //Act
            bool result = tradeValidator.Validate(strData);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void TestLongCurrencyString()
        {
            //Arrange
            var logger = new ConsoleLogger();
            var tradeValidator = new SimpleTradeValidator(logger);
            string[] strData = { "AAAABBBB", "4444", "1.00" };

            //Act
            bool result = tradeValidator.Validate(strData);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void TestNormalTrade()
        {
            //Arrange
            var logger = new ConsoleLogger();
            var tradeValidator = new SimpleTradeValidator(logger);
            string[] strData = { "AAABBB", "4444", "1.00" };

            //Act
            bool result = tradeValidator.Validate(strData);

            //Assert
            Assert.IsTrue(result);
        }
        [TestMethod()]
        public void TestAmount999()
        {
            //Arrange
            var logger = new ConsoleLogger();
            var tradeValidator = new SimpleTradeValidator(logger);
            string[] strData = { "AAABBB", "999", "1.00" };

            //Act
            bool result = tradeValidator.Validate(strData);

            //Assert
            Assert.IsFalse(result);
        }
        [TestMethod()]
        public void TestAmount1000()
        {
            //Arrange
            var logger = new ConsoleLogger();
            var tradeValidator = new SimpleTradeValidator(logger);
            string[] strData = { "AAABBB", "1000", "1.00" };

            //Act
            bool result = tradeValidator.Validate(strData);

            //Assert
            Assert.IsTrue(result);
        }
        [TestMethod()]
        public void TestAmountNeg10000()
        {
            //Arrange
            var logger = new ConsoleLogger();
            var tradeValidator = new SimpleTradeValidator(logger);
            string[] strData = { "AAABBB", "-10000", "1.00" };

            //Act
            bool result = tradeValidator.Validate(strData);

            //Assert
            Assert.IsFalse(result);
        }
        [TestMethod()]
        public void TestAmount100k()
        {
            //Arrange
            var logger = new ConsoleLogger();
            var tradeValidator = new SimpleTradeValidator(logger);
            string[] strData = { "AAABBB", "100000", "1.00" };

            //Act
            bool result = tradeValidator.Validate(strData);

            //Assert
            Assert.IsTrue(result);
        }
        [TestMethod()]
        public void TestAmount101k()
        {
            //Arrange
            var logger = new ConsoleLogger();
            var tradeValidator = new SimpleTradeValidator(logger);
            string[] strData = { "AAABBB", "1000001", "1.00" };

            //Act
            bool result = tradeValidator.Validate(strData);

            //Assert
            Assert.IsFalse(result);
        }
        //  add a new unit tests to check for the correct behavior when the validator is given an empty string
        //  or "" for the first string in the strData list.
        // gh 10/31/2024 at 11:08 am, teg added to Unit 10
        [TestMethod()]
        public void TestEmptyCurrencyString()
        {
            // Arrange
            var logger = new ConsoleLogger();
            var tradeValidator = new SimpleTradeValidator(logger);
            string[] strData = { "", "4444", "1.00" }; // First element is an empty string

            // Act
            bool result = tradeValidator.Validate(strData);

            // Assert
            Assert.IsFalse(result); // Expecting validation to fail
        }
    }
}