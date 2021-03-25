using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionMicroservice.Models;
using TransactionMicroservice.Data;
using System.Net.Http;
using TransactionMicroservice.Services;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TransactionMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        //https://localhost:44376/api/transaction

        private ITransactionService service;

        public TransactionController(ITransactionService _service)
        {
            service = _service;
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit(int accountId, double amount)
        {
            if(accountId <= 0 || amount <= 0)
            {
                return BadRequest("AccountId or amount must be greater than zero");
            }
            else
            {
                TransactionStatus status = await service.Deposit(accountId,amount);
                if(status!=null)
                {
                    return Ok(status);
                }
                else
                {
                    return NoContent();
                }
                
            }
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw(int accountId, double amount)
        {
            if (accountId <= 0 || amount <= 0)
            {
                return BadRequest("AccountId or amount must be greater than zero");
            }
            else
            {
                TransactionStatus status = await service.Withdraw(accountId, amount);
                if(status!=null)
                {
                    return Ok(status);
                }
                else
                {
                    return NoContent();
                }
                
            }
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer(int sourceAccountId, int targetAccountId, double amount)
        {
            if (sourceAccountId <= 0 || targetAccountId <= 0 || amount <= 0)
            {
                return BadRequest("AccountId or amount must be greater than zero");
            }
            else
            {
                IEnumerable<TransactionStatus> status = await service.Transfer(sourceAccountId, targetAccountId, amount);
                if(status!=null)
                {
                    return Ok(status);
                }
                else
                {
                    return NoContent();
                }
                
            }
        } 

        [HttpGet("getTransactions")]
        public async Task<IActionResult> GetTransactions(int customerId)
        {
            if(customerId <= 0)
            {
                return BadRequest("Customer Id must be greater than zero");
            }
            else
            {
                IEnumerable<Financial_Transactions> financial_Transactions = await service.GetTransactions(customerId);
                if(financial_Transactions == null)
                {
                    // return Content("Check your customer id again.");
                    return NoContent();
                }
                else if(financial_Transactions.Count() > 0)
                {
                    return Ok(financial_Transactions);
                }
                else
                {
                    return Content("No transactions found for your accounts");
                    //return NoContent();
                }
            }
        }

    }
}
