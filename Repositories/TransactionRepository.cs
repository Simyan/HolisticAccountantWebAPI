using System;
using HolisticAccountant.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using HolisticAccountant.Interfaces;

namespace HolisticAccountant.Repositories
{
    /*Basic features - 2/July/2020
     *[1] Top 3 blocks - 1. Avg Monthly exp 2. Avg Daily. exp 3. Total daily exp
      [2] Bar Chart - 1. Chart showing daily expensed in a month 
      [3] Pie Chart - Break down monthly expense based on categories
      [4] List of all transactions*/
    public class TransactionRepository : ITransactionRepository
    {
        private readonly HolisticAccountantContext _context;

        public TransactionRepository(HolisticAccountantContext context)
        {
            _context = context;
        }
        public IEnumerable<Transaction> GetTransactions()
        {
            return _context.Transactions.ToList();
        }

        //Get a paged list of transactions - Order by date?

        //Get total expenditure - daily/weekly/monthly/yearly

        public double GetTotalExpenditure()
        {
            //var total = _context.Transactions.Where(x => x.PurchasedOn > DateTime.Now).Sum(x => x.Amount);
            var total = _context.Transactions.Sum(x => x.Amount);
            return total;
        }

        //Get Total Expenditure per type - daily/weekly/monthly/yearly

        //Get Average Expenditure - daily/weekly/monthly/yearly
        public double GetAverageExpenditure()
        {
            //var total = _context.Transactions.Where(x => x.PurchasedOn > DateTime.Now).Sum(x => x.Amount);
            var total = _context.Transactions.Where(x => x.PurchasedOn < DateTime.Now).Average(x => x.Amount);
            return total;
        }

        //Get Average Expenditure per type - daily/weekly/monthly/yearly
       
        //Feature 1.1
        public double GetAverageMonthlyExpenditure()
        {
            var result = from row in _context.Transactions
                         group row by new { month = row.PurchasedOn.Month, year = row.PurchasedOn.Year } into monthly
                         select new { month = monthly.Key.month, year = monthly.Key.year, monthlySum = monthly.Sum(y => y.Amount) };
            
            return result.Where(x => !(x.month == DateTime.Now.Month && x.year == DateTime.Now.Year)).Average(avg => avg.monthlySum);
        }
        
    }
}