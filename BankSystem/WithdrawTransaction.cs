using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



class WithdrawTransaction : Transaction
{
    private Account _account;
    public Account Account { get { return _account; } }

    public WithdrawTransaction(Account account, decimal amount) : base(amount)
    {
        this._account = account;
    }
    public override string TransactionType()
    {
        return "Withdraw";
    }

    public override string GetAccountName()
    {
        return _account.Name;
    }

    public override void Print()
    {
        Console.WriteLine("Withdraw transaction details:");
        Console.WriteLine($"Account: {_account.Name}");
        Console.WriteLine($"Withdraw Amount: {_amount}");
        Console.WriteLine($"Account Balance: {_account.Balance}");
        Console.WriteLine();
    }

    public override void Execute()
    {
        base.Execute();

        _success = _account.Withdraw(_amount);
        if(_success )
        {
            Console.WriteLine("Withdrawal Successful");
        }

        if (!_success)
        {
            throw new InvalidOperationException("Insufficient funds for withdrawal");
        }
    }

    public override void Rollback()
    {
        base.Rollback();

        bool complete = _account.Deposit(_amount);
        if (!complete)
        {
            throw new InvalidOperationException("Insufficient funds for rollback");
        }
        Reversed = true;
    }
}


