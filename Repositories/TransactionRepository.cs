using System;
using HolisticAccountant.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using HolisticAccountant.Interfaces;
using HolisticAccountant.Models.DTO;
using Serilog;

namespace HolisticAccountant.Repositories
{
    /*Basic features - 2/July/2020
     *[1] Top 3 blocks - 1. Avg Monthly exp 2. Avg Daily. exp 3. Total daily exp
      [2] Bar Chart - 1. Chart showing daily expenses in a month 
      [3] Pie Chart - 1. Break down monthly expense based on categories
      [4] List of all transactions*/
    public class TransactionRepository : ITransactionRepository
    {
        private readonly HolisticAccountantContext _context;

        public TransactionRepository(HolisticAccountantContext context)
        {
            _context = context;
        }

        //Feature 1.1
        public double GetAverageMonthlyExpenditure()
        {
            var result = from row in _context.Transactions
                         group row by new { month = row.PurchasedOn.Month, year = row.PurchasedOn.Year } into monthly
                         select new { month = monthly.Key.month, year = monthly.Key.year, monthlySum = monthly.Sum(y => y.Amount) };

            var amount = result.Where(x => !(x.month == DateTime.Now.Month && x.year == DateTime.Now.Year)).Average(avg => avg.monthlySum);
            
            return Math.Round(amount, 2, MidpointRounding.AwayFromZero);
        }

        //Feature 1.2
        public double GetAverageExpenditure()
        {
            //BUG - Average is being calculated based on total number of entries, it should instead be based on total number of days elapsed between first and last transaction.
            //var total = _context.Transactions.Where(x => x.PurchasedOn > DateTime.Now).Sum(x => x.Amount);
            var first = _context.Transactions.OrderBy(x => x.PurchasedOn).First().PurchasedOn;
            var last = _context.Transactions.OrderBy(x => x.PurchasedOn).Last().PurchasedOn;
            var diff = (last - first).Days;
            var amountSum = _context.Transactions.Where(x => x.PurchasedOn < DateTime.Now).Sum(x => x.Amount);
            var amountAverage = amountSum / diff;
            return Math.Round(amountAverage, 2, MidpointRounding.AwayFromZero);
        }
        
        //Feature 1.3
        public double GetCurrentDayTotalExpenditure()
        {
            //var total = _context.Transactions.Where(x => x.PurchasedOn > DateTime.Now).Sum(x => x.Amount);
            var amount = _context.Transactions.Where(x => x.PurchasedOn == DateTime.Now).Sum(x => x.Amount);
            return Math.Round(amount, 2, MidpointRounding.AwayFromZero);
        }

        //Feature 2.1
        public IEnumerable<DailyExpenseChartDTO> GetMonthlyDailyExpenditure(DateTime? selectedMonth)
        {
            
            selectedMonth = selectedMonth.HasValue ? selectedMonth : DateTime.Now; 

            var result = from row in _context.Transactions
                         group row by new { year = row.PurchasedOn.Year, month = row.PurchasedOn.Month, day = row.PurchasedOn.Day }  into monthly
                         select new DailyExpenseChartDTO { Month = monthly.Key.month, Day = new DateTime(monthly.Key.year, monthly.Key.month, monthly.Key.day), Amount = monthly.Sum( y => y.Amount)};

            var finalResult = result.ToList().Where(x => x.Month == selectedMonth.Value.Month && x.Day.Year == selectedMonth.Value.Year)
                .Select(x => new DailyExpenseChartDTO
                {
                    Amount = x.Amount,
                    DayText = x.Day.Day.ToString(),
                    MonthText = x.Month.ToString()
                });
            Log.Information("{@finalResult}", finalResult);
            return finalResult;
        }

        //Feature 3.1
        public IEnumerable<CategoryExpenseDTO> GetMonthlyCategoryExpenditure(DateTime? selectedMonth)
        {
            selectedMonth = selectedMonth.HasValue ? selectedMonth : DateTime.Now;

            var result = from row in _context.Transactions
                         group row by new { month = row.PurchasedOn.Month, year = row.PurchasedOn.Year, category = row.Type } into monthly
                         select new CategoryExpenseDTO  { 
                             Month = monthly.Key.month, Year = monthly.Key.year, Category = monthly.Key.category, 
                             CategoryMonthlyAmount = monthly.Sum(y => y.Amount) };

            return result.Where(x => x.Month == selectedMonth.Value.Month && x.Year == selectedMonth.Value.Year);
        }



        //Feature 4
        public IEnumerable<Transaction> GetTransactions()
        {
            return _context.Transactions.ToList();
        }

        public void SaveTransactions(List<Transaction> transactions)
        {
           _context.AddRange(transactions);
           var result = _context.SaveChanges();
        }
    }
}