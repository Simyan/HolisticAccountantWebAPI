using System;
using System.Collections.Generic;
using HolisticAccountant.Models.DTO;
using HolisticAccountant.Models.Entities;

namespace HolisticAccountant.Interfaces
{
    public interface ITransactionRepository
    {
        IEnumerable<Transaction> GetTransactions(); 
        double GetCurrentDayTotalExpenditure(); 
        double GetAverageExpenditure();
        double GetAverageMonthlyExpenditure();

        IEnumerable<DailyExpenseChartDTO> GetMonthlyDailyExpenditure(DateTime? selectedMonth);
    }    
}