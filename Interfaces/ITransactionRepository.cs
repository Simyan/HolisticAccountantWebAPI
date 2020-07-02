using System.Collections.Generic;
using HolisticAccountant.Models.Entities;

namespace HolisticAccountant.Interfaces
{
    public interface ITransactionRepository
    {
        IEnumerable<Transaction> GetTransactions(); 
        double GetTotalExpenditure(); 
        double GetAverageExpenditure();
        double GetAverageMonthlyExpenditure();
    }    
}