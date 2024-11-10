using System.Collections.Generic;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple.Contracts
{
    public interface ITradeParser
    {
        IEnumerable<TradeRecord> Parse(IEnumerable<string> tradeData); // Ensure this exists
    }
}

