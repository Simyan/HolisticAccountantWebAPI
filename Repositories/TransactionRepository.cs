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
       public IEnumerable<Transaction> GetTransactions(){
           return _context.Transactions.ToList();
       }
   } 
}