﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HolisticAccountant.Interfaces;
using HolisticAccountant.Models.Entities;
using HolisticAccountant.Models.DTO;
using Serilog;
using AutoMapper;

namespace HolisticAccountant.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private ITransactionRepository _transactionRepository;
        private ISMSRepository _smsRepository;
        private readonly IMapper _mapper;
        public TransactionController(ITransactionRepository transactionRepository, ISMSRepository smsRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _smsRepository = smsRepository;
            _mapper = mapper;
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
            //for Testing
            //selectedMonth = new DateTime(2019, 7, 1);
            return _transactionRepository.GetMonthlyDailyExpenditure(selectedMonth);
        }

        [HttpGet("MonthlyCategoryExpenditure")]
        public IEnumerable<CategoryExpenseDTO> GetMonthlyCategoryExpenditure(DateTime? selectedMonth)
        {
            //for Testing
            //selectedMonth = new DateTime(2020, 5, 1);
            return _transactionRepository.GetMonthlyCategoryExpenditure(selectedMonth);
        }

        [HttpPost("PostSMSList")]
        public void PostSMSList(SMSListDTO request)
        {
            Log.Information("SMS list recieved from Android Service. {@x}", request);
            var transactionsDTO = _smsRepository.PostSMSList(request);
            _transactionRepository.SaveTransactions(_mapper.Map<List<Transaction>>(transactionsDTO));
        }
    }
}
