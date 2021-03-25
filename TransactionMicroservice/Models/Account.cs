using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionMicroservice.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public int CustomerId { get; set; }
        public DateTime AccountCreationDate { get; set; }
        public string AccountType { get; set; }
        public double CurrentBalance { get; set; }

    }
}
