using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

class TransferTransaction : Transaction
{
    private Account _fromAccount;
    private Account _toAccount;
    private DepositTransaction _deposit;
    private WithdrawTransaction _withdraw;
    
    public TransferTransaction(Account fromAccount, Account toAccount, decimal amount) : base(amount)
    {
        _fromAccount = fromAccount;
        _toAccount = toAccount;
        _amount = amount;

        _withdraw = new WithdrawTransaction(fromAccount, amount);
        _deposit = new DepositTransaction(toAccount, amount);
    }

    public override string TransactionType()
    {
        return "Transfer";
    }

    public override string GetAccountName()
    {
        return $"From: {_fromAccount.Name}, To: {_toAccount.Name}";
    }


    public override void Print()
    {
        Console.WriteLine($"Transferred ${_amount} from {_fromAccount.Name}'s account to {_toAccount.Name}'s account");
        Console.WriteLine();
        _withdraw.Print();
        _deposit.Print();

    }


    public override void Execute()
    {
        base.Execute();

        try
        {
            _withdraw.Execute();
            _deposit.Execute();
            _success = true;
        }
        catch (InvalidOperationException)
        {
            Rollback(); 
            throw new InvalidOperationException("Failed to execute transfer transaction.");
        }
    }
    public override void Rollback()
    {
        base.Rollback();

        bool rollbackFailed = false; // Flag to track if any rollback operation failed

        try
        {
            _deposit.Rollback();
        }
        catch (InvalidOperationException ex)
        {
            rollbackFailed = true; // Set flag to true indicating rollback failure
            Console.WriteLine("Failed to rollback withdraw transaction: " + ex.Message);
        }

        

        if (rollbackFailed)
        {
            // If any rollback operation failed, throw an exception indicating the failure
            throw new InvalidOperationException("Failed to rollback transfer transaction.");
            Reversed = false;
        }
        else
        {
            try
            {
                _withdraw.Rollback();
                Reversed = true;
            }
            catch (InvalidOperationException ex)
            {
                rollbackFailed = true; // Set flag to true indicating rollback failure
                Console.WriteLine("Failed to rollback deposit transaction: " + ex.Message);
            }
        }
        
    }

}
