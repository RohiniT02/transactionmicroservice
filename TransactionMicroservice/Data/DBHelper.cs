using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionMicroservice.Models;

namespace TransactionMicroservice.Data
{
    public static class DBHelper
    {
        public static List<Counterparties> Counterparties = new List<Counterparties> { 
            new Counterparties(){ Counterparty_ID = 1, Counterparty_Name = "Counterparty1"},
            new Counterparties(){ Counterparty_ID = 2, Counterparty_Name = "Counterparty2"},
            new Counterparties(){ Counterparty_ID = 3, Counterparty_Name = "Counterparty3"},
            new Counterparties(){ Counterparty_ID = 4, Counterparty_Name = "Counterparty4"},
            new Counterparties(){ Counterparty_ID = 5, Counterparty_Name = "Counterparty5"}
        };

        public static List<Ref_Payment_Methods> Payment_Methods = new List<Ref_Payment_Methods>
        {
            new Ref_Payment_Methods(){ Payment_Method_Code = "PMCode1", Payment_Method_Name = "Amex"},
            new Ref_Payment_Methods(){ Payment_Method_Code = "PMCode2", Payment_Method_Name = "Bank Transfer"},
            new Ref_Payment_Methods(){ Payment_Method_Code = "PMCode3", Payment_Method_Name = "Cash"},
            new Ref_Payment_Methods(){ Payment_Method_Code = "PMCode4", Payment_Method_Name = "Diners Club"},
            new Ref_Payment_Methods(){ Payment_Method_Code = "PMCode5", Payment_Method_Name = "MasterCard"},
            new Ref_Payment_Methods(){ Payment_Method_Code = "PMCode6", Payment_Method_Name = "Visa"}
        };

        public static List<Service> Services = new List<Service>
        {
            new Service{ Service_Id = 1, Date_Service_Provided = new DateTime(2020,02,21)},
            new Service{ Service_Id = 1, Date_Service_Provided = new DateTime(2020,07,15)},
            new Service{ Service_Id = 1, Date_Service_Provided = new DateTime(2021,01,30)},
            new Service{ Service_Id = 1, Date_Service_Provided = new DateTime(2020,11,02)},
            new Service{ Service_Id = 1, Date_Service_Provided = new DateTime(2021,02,27)}
        };

        public static List<Ref_Transaction_Types> Transaction_Types = new List<Ref_Transaction_Types>
        {
            new Ref_Transaction_Types { Transaction_Type_Code = "T01", Transaction_Type_Description = "Adjustment"},
            new Ref_Transaction_Types { Transaction_Type_Code = "T02", Transaction_Type_Description = "Payment"},
            new Ref_Transaction_Types { Transaction_Type_Code = "T03", Transaction_Type_Description = "Refund"}
        };

        public static List<Ref_Transaction_Status> Transaction_Statuses = new List<Ref_Transaction_Status>
        {
            new Ref_Transaction_Status{ Transaction_Status_Code = "TSC01", Transaction_Status_Description = "Cancelled"},
            new Ref_Transaction_Status{ Transaction_Status_Code = "TSC02", Transaction_Status_Description = "Completed"},
            new Ref_Transaction_Status{ Transaction_Status_Code = "TSC03", Transaction_Status_Description = "Disputed"},
            new Ref_Transaction_Status{ Transaction_Status_Code = "TSC04", Transaction_Status_Description = "Challenged"}
        };

        public static List<Financial_Transactions> Financial_Transactions = new List<Financial_Transactions>
        {
            new Financial_Transactions
            {
                Transaction_Id = 1000,
                Account_Id = 1001,
                Counterparty_Id = 1,
                Payment_Method_Code = "PMCode1",
                Service_Id = 1,
                Transaction_Status_Code = "TSC01",
                Transaction_Type_Code = "T01",
                Date_of_Transaction = new DateTime(2020,11,07),
                Amount_of_Transaction = 1000
            },
            new Financial_Transactions
            {
                Transaction_Id = 1001,
                Account_Id = 1002,
                Counterparty_Id = 1,
                Payment_Method_Code = "PMCode1",
                Service_Id = 1,
                Transaction_Status_Code = "TSC01",
                Transaction_Type_Code = "T01",
                Date_of_Transaction = new DateTime(2020,11,07),
                Amount_of_Transaction = 1000
            },
            new Financial_Transactions
            {
                Transaction_Id = 1002,
                Account_Id = 1003,
                Counterparty_Id = 1,
                Payment_Method_Code = "PMCode1",
                Service_Id = 1,
                Transaction_Status_Code = "TSC01",
                Transaction_Type_Code = "T01",
                Date_of_Transaction = new DateTime(2020,11,07),
                Amount_of_Transaction = 1000
            },
            new Financial_Transactions
            {
                Transaction_Id = 1003,
                Account_Id = 1004,
                Counterparty_Id = 1,
                Payment_Method_Code = "PMCode1",
                Service_Id = 1,
                Transaction_Status_Code = "TSC01",
                Transaction_Type_Code = "T01",
                Date_of_Transaction = new DateTime(2020,11,07),
                Amount_of_Transaction = 1000
            }
        };
    }
}
