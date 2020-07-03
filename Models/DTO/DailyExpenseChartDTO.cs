using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HolisticAccountant.Models.DTO
{
    public class DailyExpenseChartDTO
    {
        public int Month { get; set; }
        public DateTime Day { get; set; }
        public double Amount { get; set; } 
    }
}
