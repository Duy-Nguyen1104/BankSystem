abstract class Transaction
{
    protected decimal _amount;
    protected Boolean _success;
    private Boolean _executed;
    private Boolean _reversed;
    private DateTime _dateStamp;

    public bool Success
    {
        get { return _success; }
    }
    public bool Executed
    {
        get { return _executed; }
    }
    public bool Reversed
    {
        get { return _reversed; }
        set { _reversed = value; }
    }
    public DateTime DateStamp
    {
        get { return _dateStamp; }
    }
    public decimal Amount
    {
        get { return _amount; }
    }
    public Transaction(decimal amount)
    {
        this._amount = amount;
    }
    public abstract string GetAccountName();
    public abstract string TransactionType();

    public string TransactionStatus()
    {
        if (Reversed)
        {
            return "Reversed";
        }
        else if (Success)
        {
            return "Sucess";
        }
        else if (!Success)
        {
            return "Failed";
        }
        else
        {
            return "Not Executed";
        }
    }

    public virtual void Print()
    {
        Console.WriteLine("Transaction amount: {0}, Executed: {1}, Success: {2}, Reverse: {3}", _amount.ToString("C"), _executed, _success, _reversed);
    }
    public virtual void Execute()
    {
        if (_executed && _success)
        {
            throw new InvalidOperationException("Transaction already executed");
        }
        _dateStamp = DateTime.Now;
        _executed = true;
    }
    public virtual void Rollback()
    {
        if (_reversed)
        {
            throw new InvalidOperationException("Transaction already reversed");
        }
        else if (!_success)
        {
            throw new InvalidOperationException("Transaction has not executed successfully");
        }
        _dateStamp = DateTime.Now;
    }
}