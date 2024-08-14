class DepositTransaction : Transaction
{
    private Account _account;

    public Account Account { get { return _account; } }

    public DepositTransaction(Account account, decimal amount) : base(amount)
    {
        this._account = account;
    }
    public override string TransactionType()
    {
        return "Deposit";
    }

    public override string GetAccountName()
    {
        return _account.Name;
    }

    public override void Print()
    {
        Console.WriteLine("Deposit transaction details:");
        Console.WriteLine($"Account: {_account.Name}");
        Console.WriteLine($"Deposit Amount: {_amount}");
        Console.WriteLine($"Account Balance: {_account.Balance}");
        Console.WriteLine();
    }
    public override void Execute()
    {
        base.Execute();

        _success = _account.Deposit(_amount);
        if (_success )
        {
            Console.WriteLine("Deposit Successful");

        }
        if (!_success)
        {
            throw new InvalidOperationException("Deposit amount invalid");
        }
    }
    public override void Rollback()
    {
        base.Rollback();

        bool complete = _account.Withdraw(_amount);
        if (!complete)
        {
            throw new InvalidOperationException("Insufficient funds for rollback");
        }
        Reversed = true;
    }
}
