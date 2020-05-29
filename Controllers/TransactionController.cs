using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HolisticAccountant.Interfaces;
using HolisticAccountant.Models.Entities;

namespace HolisticAccountant.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private ITransactionRepository _transactionRepository;
        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpGet]
        public IEnumerable<Transaction> Get()
        { 
            return _transactionRepository.GetTransactions();
        }

        [HttpGet("TotalExpenditure")]
        public double GetTotalExpenditure()
        {
            return _transactionRepository.GetTotalExpenditure();
        }

        [HttpGet("AverageExpenditure")]
        public double GetAverageExpenditure()
        {
            return _transactionRepository.GetAverageExpenditure();
        }
    }
}
