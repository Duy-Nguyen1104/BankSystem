class Account
{
    private decimal _balance;
    private String _name;

    public Account(decimal balance, string name)
    {
        this._balance = balance;
        this._name = name;
    }
    public bool Deposit(decimal amount)
    {
        if (amount >0)
        {
            this._balance += amount;
            return true;
        }
        else
        {
            Console.WriteLine("Deposit unsuccessful.");
            return false;
        }
    }
    public bool Withdraw(decimal amount)
    {
        if (amount > 0 && amount <= this._balance)
        {
            this._balance -= amount;
            return true;
        }
        else
        {
            Console.WriteLine("Withdraw unsuccessful.");
            return false;
        }
    }
    public void Print()
    {
        Console.WriteLine("Account name: " + this._name + ", Balance: " + this._balance);
    }
    public string Name
    {
        get { return _name; }
    }
    public decimal Balance
    {
        get { return _balance; }
    }

}