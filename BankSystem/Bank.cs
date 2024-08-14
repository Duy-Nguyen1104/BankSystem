using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

class Bank
{
    private List<Account> accounts;
    private List<Transaction> transactions;

    public List<Transaction> Transactions { get { return transactions; } }

    public Bank() 
    {
        accounts = new List<Account>();
        transactions = new List<Transaction>();
    }
    public void AddAccount (Account account)
    {
        accounts.Add(account);
    }
    public Account GetAccount (string name)
    {
        foreach (Account account in accounts)
        {
            if (account.Name.ToLower() == name)
            {
                return account;
            }            
        }
        return null;
    }    
    public void Execute(Transaction transaction)
    {
        transactions.Add(transaction);
        try
        {
            transaction.Execute();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("An error occured during execution: " + ex.Message);
        }
    }
    public void Rollback(Transaction transaction)
    {
        try
        {
            transaction.Rollback();
            Console.WriteLine("Rollback Succesful");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("An error occured during execution: " + ex.Message);
        }
    }
    
    public void PrintTransactionHistory()
    {
        Console.WriteLine(new String('-', 126));
        Console.WriteLine(
            "| {0,2} |{1,25} |{2,20} |{3,25} |{4,20} | {5,20}|",
            "#", "DateTime", "Type", "Account Name", "Amount", "Status");
        Console.WriteLine(new String('=', 126));
        for (int i = 0; i < transactions.Count; i++)
        {
            string transactionType = transactions[i].TransactionType();
            string transactionStatus = transactions[i].TransactionStatus();
            string transactionName = transactions[i].GetAccountName();
            string transactionAmount = transactions[i].Amount.ToString("C");
            Console.WriteLine(
            "| {0,2} |{1,25} |{2,20} |{3,25} |{4,20} | {5,20}|",
                i+1, transactions[i].DateStamp, transactionType, transactionName, transactionAmount, transactionStatus);
        }
        Console.WriteLine(new String('=', 126));
    }
}

    
