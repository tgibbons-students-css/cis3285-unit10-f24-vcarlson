using SingleResponsibilityPrinciple.Contracts;

public interface ITradeStorage
{
    Task Persist(IEnumerable<TradeRecord> trades);  // Asynchronous method
}
