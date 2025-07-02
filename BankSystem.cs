namespace BankingSystem;

enum MenuOption
{
    AddAccount,
    Withdraw,
    Deposit,
    Transfer,
    Print,
    PrintTransHistory,
    Quit
}

class BankSystem
{
    private static Account FindAccount(Bank bank)
    {
        String name;
        Account targetAccount;
        Console.WriteLine("Please enter the account name to perform the action: ");
        name = Console.ReadLine()!;

        targetAccount = bank.GetAccount(name);

        if(targetAccount != null)
        {
            Console.WriteLine("Account found!");
            return targetAccount;
        }
        else
        {
            throw new InvalidOperationException("Account name: " + name + " does not match any record");
        }
    }

    public static void AddAccount(Bank bank)
    {
        String _name = "";
        decimal _balance = 0;
        bool loop = true;

        do
        {
            try
            {
                Console.WriteLine("Please enter a name for the account: ");
                _name = Console.ReadLine()!;
                if(_name == "" || _name == null)
                {
                    throw new InvalidOperationException("Please enter a valid name!");
                }

    
                Console.WriteLine("Please enter a starting balance: ");
                _balance = Convert.ToDecimal(Console.ReadLine());
                if(_balance <= 0)
                {
                    throw new InvalidOperationException("Please enter a valid balance!");
                }
                else
                {
                    loop = false;
                }
            }
            catch(InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
            catch(FormatException)
            {
                Console.WriteLine("Please enter a valid balance");
            }
        }while(loop);

        Account newAccount = new Account(_name, _balance);

        bank.AddAccount(newAccount);
    }

    public static void DoTransfer(Bank bank)
    {
        Account? fromAccount = null;
        Account? toAccount = null;
        TransferTransaction T_Transaction; 
        Decimal amount;

        try
        {
            Console.Write("From: ");
            fromAccount = FindAccount(bank);
            Console.Write("To: ");
            toAccount = FindAccount(bank);
            if(fromAccount == toAccount)
            {
                fromAccount = null;
                toAccount = null;
                throw new InvalidOperationException("You cannot transfer money to the same account!");
            }
        }
        catch(InvalidOperationException e)
        {
            Console.WriteLine(e.Message);
        }

        if(fromAccount != null && toAccount != null)
        {
            try
            {
                Console.WriteLine("Please enter the amount you want to transfer: ");
                amount = Convert.ToDecimal(Console.ReadLine());
            }
            catch(FormatException)
            {
                Console.WriteLine("Please enter an correct amount");
                amount = -999;
            }

            T_Transaction = new TransferTransaction(fromAccount, toAccount, amount);

            try
            {
                bank.ExecuteTransaction(T_Transaction);
            }
            catch(InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }

            T_Transaction.Print();
        }
    }  

    public static void DoDeposit(Bank bank)
    {
        Account? targetAccount = null;
        DepositTransaction D_Transaction;
        Decimal amount;

        try
        {
            targetAccount = FindAccount(bank);
        }
        catch(InvalidOperationException e)
        {
            Console.WriteLine(e.Message);
        }
        
        if(targetAccount != null)
        {
            try
            {
                Console.WriteLine("Please enter the amount you want to deposit: ");
                amount = Convert.ToDecimal(Console.ReadLine());
            }
            catch(FormatException)
            {
                Console.WriteLine("Please enter a correct amount");
                amount = -999;
            }
            
            D_Transaction = new DepositTransaction(targetAccount, amount);
            try
            {
                bank.ExecuteTransaction(D_Transaction);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }

            D_Transaction.Print();
        }
        
    }

    public static void DoWithdraw(Bank bank)
    {
        Account? targetAccount = null;
        WithdrawTransaction W_Transaction;
        Decimal amount;

        try
        {
            targetAccount = FindAccount(bank);
        }
        catch(InvalidOperationException e)
        {
            Console.WriteLine(e.Message);
        }
        
        if(targetAccount != null)
        {
            try
            {
                Console.WriteLine("Please enter the amount you want to withdraw: ");
                amount = Convert.ToDecimal(Console.ReadLine());
            }
            catch(FormatException)
            {
                Console.WriteLine("Please enter an correct amount");
                amount = -999;
            }

            W_Transaction = new WithdrawTransaction(targetAccount, amount);

            try
            {
                bank.ExecuteTransaction(W_Transaction);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }

            W_Transaction.Print();
        }
    }   

    public static void DoPrint(Bank bank)
    {
        Account? targetAccount = null;
        try
        {
            targetAccount = FindAccount(bank);
            targetAccount.Print();
        }
        catch(InvalidOperationException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public static void DoRollback(Bank bank)
    {
        bank.PrintTranscationHistory();

        int input = 0;
        int index = -1;
        try
        {
            if(bank.Transactions.Count() != 0)
            {
                Console.WriteLine("Do you want to roll back a transaction? (0. No/ 1. Yes): ");
                input = Convert.ToInt32(Console.ReadLine());

                if(input == 1)
                {
                    Console.WriteLine("Please select index of the transaction you want to rollback : (integer)");
                    index = Convert.ToInt32(Console.ReadLine());

                    if(index <= 0 || index > bank.Transactions.Count())
                    {
                        throw new InvalidOperationException("Index out of range");
                    }

                    index -= 1;
                    bank.RollbackTransaction(bank.Transactions[index]);

                }
            }
            else
            {
                Console.WriteLine("There is no transaction history");
            }
        }
        catch(FormatException)
        {
            Console.WriteLine("Invalid input");
        }
        catch(InvalidOperationException e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Rollback failed");
        }
    }

    public static MenuOption ReadUserOption()
    {
        int input = -999;
        
        do
        {
            try
            {
                Console.WriteLine("==Select an action==");
                Console.WriteLine("1. Add new account");
                Console.WriteLine("2. Withdraw");
                Console.WriteLine("3. Deposit");
                Console.WriteLine("4. Transfer");
                Console.WriteLine("5. Print");
                Console.WriteLine("6. Print Transcation History");
                Console.WriteLine("7. Quit");

                string? U_input = Console.ReadLine();
                input = Convert.ToInt32(U_input);
                
            }
            catch(FormatException)
            {
                Console.WriteLine("Please enter a valid code");
            }
            
            if (input < 1 || input > Enum.GetValues(typeof(MenuOption)).Length)
            {
                Console.WriteLine("Please enter an valid integer");
                Console.WriteLine();
            }

            
        }while(input < 1 || input > Enum.GetValues(typeof(MenuOption)).Length);

        input -= 1;
        return (MenuOption)input;

    }

    static void Main(string[] args)
    {
        MenuOption option;
        Bank bank = new Bank();

        do
        {
            option = ReadUserOption();
            Console.WriteLine("You have selected: " + option);
            
            switch(option)
            {
                case MenuOption.AddAccount:
                AddAccount(bank);
                break;

                case MenuOption.Withdraw:
                DoWithdraw(bank);
                break;

                case MenuOption.Deposit:
                DoDeposit(bank);
                break;

                case MenuOption.Transfer:
                DoTransfer(bank);
                break;

                case MenuOption.Print:
                DoPrint(bank);
                break;

                case MenuOption.PrintTransHistory:
                DoRollback(bank);
                break;

                case MenuOption.Quit:
                Console.WriteLine("Thank you for choosing our bank service");
                break;
            }
        }while(option != MenuOption.Quit);
    }
}