using System.Security.Principal;
using System.Xml.Linq;

class BankSystem
{
    static void Main(string[] args)
    {
        //Testing
        Bank bank = new Bank();
        //bank.AddAccount(new Account(10000, "Chris"));
        //bank.AddAccount(new Account(14000, "Bruce"));


        MenuOption userOption;
        do
        {
            userOption = ReadUserOption();
            switch (userOption)
            {
                case MenuOption.CreateAccount:
                    Console.WriteLine("You have selected create account option");
                    CreateAccount(bank);
                    break;
                case MenuOption.Withdraw:
                    Console.WriteLine("You have selected withdraw option");
                    DoWithdraw(bank);
                    break;
                case MenuOption.Deposit:
                    Console.WriteLine("You have selected deposit option");
                    DoDeposit(bank);
                    break;
                case MenuOption.Transfer:
                    Console.WriteLine("You have selected transfer option");
                    DoTransfer(bank);
                    break;
                case MenuOption.Rollback:
                    Console.WriteLine("You have selected rollback option");
                    DoRollback(bank);
                    break;
                case MenuOption.Print:
                    Console.WriteLine("You have selected print option");
                    DoPrint(bank);
                    break;
                case MenuOption.Quit:
                    Console.WriteLine("Thank you. Have a good day");
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        } while (userOption != MenuOption.Quit);
    }
    public static MenuOption ReadUserOption()
    {
        int choice;
        do
        {
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. Add Account");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Deposit");
            Console.WriteLine("4. Transfer");
            Console.WriteLine("5. Rollback");
            Console.WriteLine("6. Print");
            Console.WriteLine("7. Quit");
            Console.Write("Enter your choice (1-7): ");

            // Read user input as a string
            string userInput = Console.ReadLine();

            // Convert the string input to an integer
            try
            {
                choice = Convert.ToInt32(userInput);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a number (1-7).");
                continue;
            }
            catch (OverflowException)
            {
                Console.WriteLine("Input out of range. Please enter a number (1-7).");
                continue;
            }

            // Check if the choice is within the valid range of MenuOption
            if (Enum.IsDefined(typeof(MenuOption), choice))
            {
                return (MenuOption)choice;
            }
            else
            {
                Console.WriteLine("Invalid option. Please choose a valid option (1-7).");
            }
        } while (true); // Repeat until a valid option is chosen
    }
    public static void DoDeposit(Bank bank)
    {
        Account account = FindAccount(bank);
        if (account != null)
        {
            Console.Write("Enter amount to deposit: ");
            decimal amount = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine();

            DepositTransaction transaction = new DepositTransaction(account, amount);
            try
            {
                bank.Execute(transaction);
            }
            catch (InvalidOperationException)
            {
                transaction.Print();
                return;
            }
            transaction.Print();
        }
    }
    public static void DoWithdraw(Bank bank)
    {
        Account account = FindAccount(bank);
        if (account != null)
        {
            Console.Write("Enter amount to withdraw: ");
            decimal amount = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine();

            WithdrawTransaction transaction = new WithdrawTransaction(account, amount);
            try
            {
                bank.Execute(transaction);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Transfer transactions failed. Insufficient funds.");
                return;
            }
            transaction.Print();
        }
        
    }
    public static void DoTransfer(Bank bank)
    {
        Console.Write("For withdrawal: ");
        Account fromAccount = FindAccount(bank);
        if (fromAccount != null)
        {
            Console.Write("For deposit: ");
            Account toAccount = FindAccount(bank);
            if (toAccount != null)
            {
                Console.Write("Enter amount to transfer: ");
                decimal amount = Convert.ToDecimal(Console.ReadLine());
                TransferTransaction transaction = new TransferTransaction(fromAccount, toAccount, amount);

                try
                {
                    //transfer.Execute();
                    bank.Execute(transaction);
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Transfer transactions failed. Insufficient funds.");
                    return;
                }
                transaction.Print();
            }
        
        }
        
    }
    public static void DoPrint(Bank bank)
    {
        Account account = FindAccount(bank);
        if (account != null)
        {
            account.Print();
        }
    }
    public static void DoRollback(Bank bank)
    {
        bank.PrintTransactionHistory();
        Console.Write("Enter transaction # to rollback: ");
        int result = Convert.ToInt32(Console.ReadLine());

        if (result > 0 && result <= bank.Transactions.Count)
        {
            bank.Rollback(bank.Transactions[result - 1]);
        }
        else
        {
            Console.WriteLine("Inavlid number");
            return;
        }
    }
    public static void CreateAccount(Bank bank)
    {
        Console.Write("Enter account name: ");
        string name = Console.ReadLine();
        // Check if the account already exists
        if (bank.GetAccount(name) != null)
        {
            Console.WriteLine("Account already exists");
            return;
        }
        Console.Write("Enter balance: ");
        decimal balance = Convert.ToDecimal(Console.ReadLine());
        bank.AddAccount(new Account(balance, name));
    }
    private static Account FindAccount(Bank bank)
    {
        Account account = null;
        Console.Write("Enter account name: ");
        string name = Console.ReadLine();
        account = bank.GetAccount(name);
        if (account == null)
        {
            Console.WriteLine("Account does not exist");
        }
        return account;
    }

}
public enum MenuOption
{
    CreateAccount = 1,
    Withdraw,
    Deposit,
    Transfer,
    Rollback,
    Print,    
    Quit
}

