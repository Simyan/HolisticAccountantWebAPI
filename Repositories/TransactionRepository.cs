using System;
using HolisticAccountant.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using HolisticAccountant.Interfaces;

namespace HolisticAccountant.Repositories
{
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
            var total = _context.Transactions.Average(x => x.Amount);
            return total;
        }

        //Get Average Expenditure per type - daily/weekly/monthly/yearly
    }
}