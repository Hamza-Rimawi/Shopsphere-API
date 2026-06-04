using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business.DTOs.WalletTransactions
{
    public class WalletTransactionsViewDto
    {
        public int TransactionID { get; set; }
        public decimal Currentbalance { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; }
        public string PaymentMethod { get; set; }
        public int? ReferenceID { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
