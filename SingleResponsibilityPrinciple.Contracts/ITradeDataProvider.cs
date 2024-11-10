using System.Collections.Generic;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple.Contracts
{
    public interface ITradeDataProvider
    {
        Task<IEnumerable<string>> GetTradeData(); // Ensure it returns Task
    }
}

