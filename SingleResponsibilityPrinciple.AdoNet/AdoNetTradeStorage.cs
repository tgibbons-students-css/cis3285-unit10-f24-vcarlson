using Microsoft.Data.SqlClient;
using SingleResponsibilityPrinciple.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple.AdoNet
{
    public class AdoNetTradeStorage : ITradeStorage
    {
        private readonly ILogger logger;

        public AdoNetTradeStorage(ILogger logger)
        {
            this.logger = logger;
        }

        public async Task Persist(IEnumerable<TradeRecord> trades)
        {
            // Define connection strings for different environments
            string azureConnectString = @"Server=tcp:cis3285-sql-server.database.windows.net,1433; Initial Catalog = Unit8_TradesDatabase; Persist Security Info=False; User ID=cis3285;Password=Saints4SQL; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 60;";

            // Log info about connection attempt
            logger.LogInfo("INFO: Connecting to AZURE cloud database... This may timeout the first time");

            using (var connection = new SqlConnection(azureConnectString))
            {
                await connection.OpenAsync(); // Open connection asynchronously

                // Begin transaction asynchronously
                using (var transaction = await connection.BeginTransactionAsync())
                {
                    // Explicitly cast DbTransaction to SqlTransaction
                    var sqlTransaction = (SqlTransaction)transaction;

                    foreach (var trade in trades)
                    {
                        var command = connection.CreateCommand();
                        command.Transaction = sqlTransaction; // Assign the explicitly cast transaction
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "dbo.insert_trade";
                        command.Parameters.AddWithValue("@sourceCurrency", trade.SourceCurrency);
                        command.Parameters.AddWithValue("@destinationCurrency", trade.DestinationCurrency);
                        command.Parameters.AddWithValue("@lots", trade.Lots);
                        command.Parameters.AddWithValue("@price", trade.Price);

                        await command.ExecuteNonQueryAsync(); // Execute asynchronously
                    }

                    await sqlTransaction.CommitAsync(); // Commit transaction asynchronously
                }

                await connection.CloseAsync(); // Close connection asynchronously
            }

            // Log info about how many trades were processed
            int tradeCount = trades.Count(); // Enumerate trades synchronously and get the count
            logger.LogInfo("{0} trades processed", tradeCount);
        }
    }
}
