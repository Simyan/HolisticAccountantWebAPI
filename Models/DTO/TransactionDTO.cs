using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HolisticAccountant.Models.DTO
{
    public class TransactionDTO
    {
        public long UId { get; set; }
        public double Amount { get; set; }
        public string Merchant { get; set; }
        public DateTime PurchasedOn { get; set; }
        public string Type { get; set; }
        public double Balance { get; set; }
    }
}
