using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HolisticAccountant.Interfaces;
using HolisticAccountant.Models.Entities;
using HolisticAccountant.Models.DTO;

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

        [HttpGet("CurrentDayTotalExpenditure")]
        public double GetTotalExpenditure()
        {
            return _transactionRepository.GetCurrentDayTotalExpenditure();
        }

        [HttpGet("AverageExpenditure")]
        public double GetAverageExpenditure()
        {
            return _transactionRepository.GetAverageExpenditure();
        }

        [HttpGet("AverageMonthlyExpenditure")]
        public double GetAverageMonthlyExpenditure()
        {
            return _transactionRepository.GetAverageMonthlyExpenditure();
        }

        [HttpGet("MonthlyDailyExpenditure")]
        public IEnumerable<DailyExpenseChartDTO> GetMonthlyDailyExpenditure(DateTime? selectedMonth)
        {
            selectedMonth = new DateTime(2019, 7, 1);
            return _transactionRepository.GetMonthlyDailyExpenditure(selectedMonth);
        }
    }
}
