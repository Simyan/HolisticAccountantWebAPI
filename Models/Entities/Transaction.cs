using System;
using System.ComponentModel.DataAnnotations;

namespace HolisticAccountant.Models.Entities
{
    public class Transaction {

        [Key]
        public long UId {get; set;}
        [MaxLength(100)]
        public string Merchant {get; set;}
        [MaxLength(50)]
        public string Type  {get; set;}
        public double Balance {get; set;}
        public double Amount {get; set;}

        public DateTime PurchasedOn {get; set;}
    }
}