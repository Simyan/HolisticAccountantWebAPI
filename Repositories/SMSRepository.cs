using HolisticAccountant.Interfaces;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HolisticAccountant.Models.DTO;
using Serilog;
using HolisticAccountant.Models.Entities;

namespace HolisticAccountant.Repositories
{
    public class SMSRepository : ISMSRepository
    {
        const string PurchaseKey = "purchase";
        const string SalaryKey = "salary";
        const string DebitedKey = "debited";
        const string WithdrawalKey = "withdrawal";
        const string RefundedKey = "refunded";
        const string CreditedKey = "credited";
        public List<TransactionDTO> PostSMSList(SMSListDTO request)
        {
            List<TransactionDTO> transactions = new List<TransactionDTO>();
            Log.Information("Hi from SMS Repository");
            foreach (var sms in request.Messages)
            {
                var transaction = ExtractFromSMS(sms.Body);
                transaction.PurchasedOn = Convert.ToDateTime(sms.SMSDate);
                transactions.Add(transaction);
            }
            return transactions;
        }

        static TransactionDTO ExtractFromSMS(string msg)
        {
            //msg = "Purchase of AED 53 with Debit Card ending 9133 at STEAMGAMES.COM, 425-952-2985. Avl Balance is AED 28,303.";
            string[] smsDetails = new string[] { };
            string selectedKey = "";

            if (msg.Contains("Purchase of"))
            {
                selectedKey = PurchaseKey;
            }
            else if (msg.Contains("Salary of"))
            {
                selectedKey = SalaryKey;
            }
            else if (msg.Contains("has been debited"))
            {
                selectedKey = DebitedKey;
            }
            else if (msg.Contains("Cash Withdrawal of"))
            {
                selectedKey = WithdrawalKey;
            }
            else if (msg.Contains("has been refunded"))
            {
                selectedKey = RefundedKey;
            }
            else if (msg.Contains("has been credited to"))
            {
                selectedKey = CreditedKey;
            }

            smsDetails = GetDetails(msg, GetIdentifiers(selectedKey));
            return MapSMSDetails(smsDetails, selectedKey);
        }

        static TransactionDTO MapSMSDetails(string[] smsDetails, string key)
        {
            TransactionDTO transaction = new TransactionDTO();
            switch (key)
            {
                case "purchase":
                    transaction.Amount = Double.Parse(smsDetails[0]);
                    var merchant = smsDetails.Skip(2).Take(smsDetails.Length - 3);
                    transaction.Merchant = String.Join(" ", merchant);
                    transaction.Balance = Double.Parse(smsDetails.Last().TrimEnd('.'));
                    break;
                case "salary":
                    transaction.Amount = Double.Parse(smsDetails[0]);
                    transaction.Balance = Double.Parse(smsDetails.Last().TrimEnd('.'));
                    break;
                case "debited":
                    transaction.Amount = Double.Parse(smsDetails[0]);
                    transaction.Balance = Double.Parse(smsDetails.Last().TrimEnd('.'));
                    break;
                case "withdrawal":
                    transaction.Amount = Double.Parse(smsDetails[0]);
                    transaction.Balance = Double.Parse(smsDetails.Last().TrimEnd('.'));
                    break;
                case "refunded":
                    transaction.Amount = Double.Parse(smsDetails[0]);
                    transaction.Balance = Double.Parse(smsDetails.Last().TrimEnd('.'));
                    break;
                case "credited":
                    transaction.Amount = Double.Parse(smsDetails[0]);
                    transaction.Balance = Double.Parse(smsDetails.Last().TrimEnd('.'));
                    break;
            }
            return transaction;
        }

        static string[] GetDetails(string msg, string[] identifiers)
        {
            return msg.Split(" ").Except(identifiers).ToArray();
        }

        static string[] GetIdentifiers(string key)
        {
            string purchaseIdentifiersString = "Purchase of AED with Debit Card ending at Avl Balance is AED";
            string salaryIdentifiersString = "Salary of AED has been credited into your account 101-XXX73XXX-01. The available balance is";
            string debitedIdentifiersString = "AED has been debited from your account no. 101-XXX73XXX-01 INWARD CLEARING. The available balance is AED";
            string withdrawalIdentifiersString = "Cash Withdrawal of AED with Debit Card ending 2932 at . Avl Bal is AED";
            string refundedIdentifiersString = "Purchase amount of AED at on your Debit Card has been refunded to your card account. Avl Bal is AED";
            string creditedIdentifiersString = "AED has been credited to account 101-XXX73XXX-01. Current balance is AED . Credits post cut-offs will be available next day.";

            Dictionary<string, string[]> identifiers = new Dictionary<string, string[]>();
            identifiers.Add("purchase", purchaseIdentifiersString.Split());
            identifiers.Add("salary", salaryIdentifiersString.Split());
            identifiers.Add("debited", debitedIdentifiersString.Split());
            identifiers.Add("withdrawal", withdrawalIdentifiersString.Split());
            identifiers.Add("refunded", refundedIdentifiersString.Split());
            identifiers.Add("credited", creditedIdentifiersString.Split());

            return identifiers[key];
        }


    }
}
