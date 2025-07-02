namespace BankingSystem;

class Account 
{ 
   //instance variable
   private decimal _balance;
   private String _name;

   //constructor
   public Account(String name, decimal balance)
   {
      this._name = name;
      this._balance = balance;
   }
   
   //accessor
   public String Name()
   {
      return this._name;
   }

   //mutator
   public bool Deposit(decimal amount)
   {
      if(amount > 0)
      {
         this._balance += amount;
         return true;
      }
      else
      {
         return false;
      }
   }

   public bool Withdraw(decimal amount)
   {
      if (amount > 0)
      {
         if(this._balance >= amount)
         {
            this._balance -= amount;
            return true;
         }
         else
         {
            return false;
         }
      }
      else
      {
         return false;
      }
   }

   //method
   public void Print() 
   {
      Console.WriteLine(Name() + ": " + this._balance.ToString("C2"));
   }

}