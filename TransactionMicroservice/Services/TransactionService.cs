using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TransactionMicroservice.Models;
using TransactionMicroservice.Repositories;

namespace TransactionMicroservice.Services
{
    public class TransactionService : ITransactionService
    {
        private IHttpClientFactory _httpClientFactory;
        private ITransactionRepository transactionRepository;

        public TransactionService(IHttpClientFactory httpClientFactory, ITransactionRepository _transactionRepository)
        {
            _httpClientFactory = httpClientFactory;
            transactionRepository = _transactionRepository;
        }

        public async Task<TransactionStatus> Deposit(int accountId, double amount)
        {
            TransactionStatus status1 = new TransactionStatus
            {
                Message = "Failed!",
                Updated_Balance = 0
            };

            HttpClient httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("https://localhost:44376/api/transaction");

            HttpResponseMessage response1 = httpClient.GetAsync("https://localhost:44386/api/account/getAccount?accountid=" + accountId).Result;
            if (response1.IsSuccessStatusCode)
            {
                Account account = await response1.Content.ReadAsAsync<Account>();
                if(account != null)
                {
                    status1.Updated_Balance = account.CurrentBalance;

                    var message = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44386/api/account/deposit?accountid=" + accountId + "&amount=" + amount);
                    var response = await httpClient.SendAsync(message);
                    if (response.IsSuccessStatusCode)
                    {
                        status1 = await response.Content.ReadAsAsync<TransactionStatus>();
                        transactionRepository.Deposit(accountId,amount);
                        return status1;
                    }
                    else
                    {
                        return status1;
                    }
                }
                else
                {
                    status1.Message = "Check if your account exists";
                    return status1;
                }
            }
            else
            {
                status1.Message = "Check if your account exists";
                return status1;
            }
        }

        public async Task<TransactionStatus> Withdraw(int accountId, double amount)
        {
            TransactionStatus status1 = new TransactionStatus
            {
                Message = "Failed!",
                Updated_Balance = 0
            };

            HttpClient httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("https://localhost:44376/api/transaction");
            HttpResponseMessage response1 = httpClient.GetAsync("https://localhost:44386/api/account/getAccount?accountid=" + accountId).Result;
            if (response1.IsSuccessStatusCode)
            {
                Account account = await response1.Content.ReadAsAsync<Account>();
                if(account != null)
                {
                    status1.Updated_Balance = account.CurrentBalance;
                    if (account.CurrentBalance > amount)
                    {
                        HttpResponseMessage response2 = httpClient.GetAsync("https://localhost:44374/api/rules" + "/?balance=" + account.CurrentBalance + "&accountid=" + accountId).Result;

                        if (response2.IsSuccessStatusCode)
                        {
                            string transactionStatus = await response2.Content.ReadAsStringAsync();
                            if (transactionStatus.Equals("Allowed"))
                            {
                                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44386/api/account/withdraw?accountid=" + accountId + "&amount=" + amount);
                                var response3 = await httpClient.SendAsync(message);

                                if (response3.IsSuccessStatusCode)
                                {
                                    status1 = await response3.Content.ReadAsAsync<TransactionStatus>();
                                    transactionRepository.Withdraw(accountId, amount);
                                    return status1;
                                }
                                else
                                {
                                    status1.Message = "Some error occurred.Withdrawal failed!";
                                    return status1;
                                }
                            }
                            else
                            {
                                status1.Message = "You are not allowed to perform transaction. Minimum balance criteria should be followed.";
                                return status1;
                            }
                        }
                        else
                        {
                            status1.Message = "Error in Connection with Rules Service.";
                            return status1;
                        }
                    }
                    else
                    {
                        status1.Message = "You do not suffiecient balance to withdraw amount!";
                        return status1;
                    }
                }
                else
                {
                    status1.Message = "Check if your account exists";
                    return status1;
                }
            }
            else
            {
                status1.Message = "Check if your account exists";
                return status1;
            }
        }

        public async Task<IEnumerable<TransactionStatus>> Transfer(int sourceAccountId, int targetAccountId, double amount)
        {
            List<TransactionStatus> statuses = new List<TransactionStatus>();
            TransactionStatus withdrawStatus = await Withdraw(sourceAccountId, amount);
            statuses.Add(withdrawStatus);
            if (withdrawStatus.Message.Equals("Completed"))
            {
                TransactionStatus depositStatus = await Deposit(targetAccountId, amount);
                statuses.Add(depositStatus);
                if (depositStatus.Message.Equals("Completed"))
                {
                    return statuses;
                }
                else
                {
                    TransactionStatus depositInSource = await Deposit(sourceAccountId, amount);
                    statuses.Remove(withdrawStatus);
                    return statuses;
                }
            }
            else
            {
                return statuses;
            }
        }

        public async Task<IEnumerable<Financial_Transactions>> GetTransactions(int customerId)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("https://localhost:44376/api/transaction");
            HttpResponseMessage response1 = httpClient.GetAsync("https://localhost:44386/api/account/GetCustomerAccountDetails?customerId=" + customerId).Result;
            List<Financial_Transactions> transactions = new List<Financial_Transactions>();
            if (response1.IsSuccessStatusCode)
            {
                IEnumerable<Account> accounts = await response1.Content.ReadAsAsync<IEnumerable<Account>>();
                if(accounts.Count() > 0)
                {
                    foreach (Account a in accounts)
                    {
                        IEnumerable<Financial_Transactions> fList = transactionRepository.GetTransactions(a.AccountId);
                        if (fList.Count() > 0)
                        {
                            foreach (Financial_Transactions f in fList)
                            {
                                transactions.Add(f);
                            }
                        }
                    }
                    return transactions;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
