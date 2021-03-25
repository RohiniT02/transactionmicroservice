using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionMicroservice.Models;

namespace TransactionMicroservice.Data
{
    public class TransactionContext : DbContext
    {
        public TransactionContext(DbContextOptions<TransactionContext> options) : base(options)
        {
            
        }

        public DbSet<Financial_Transactions> Financial_Transactions { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Counterparties> Counterparties { get; set; }

        public DbSet<Ref_Payment_Methods> Payment_Methods { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Ref_Transaction_Types> Transaction_Types { get; set; }

        public DbSet<Ref_Transaction_Status> Transaction_Statuses { get; set; }
    }
}
