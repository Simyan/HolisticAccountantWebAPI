using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HolisticAccountant.Models.DTO
{
    public class CategoryExpenseDTO
    {
        public string Category {get; set;}
        public double CategoryMonthlyAmount {get; set;}

        public int Year { get; set; }
        public int Month { get; set; }
    }
}
