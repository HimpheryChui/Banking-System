namespace BankingSystem;

class DepositTransaction : Transaction
{ 
   private Account _account;

   public DepositTransaction(Account account, decimal amount) : base(amount)
   {
      this._account = account;
   }

   public override void Print()
   {
      Console.WriteLine("Transaction Status: ({0} - {1})", this._account.Name(), this.DateStamp);

      if(this._success && !Reversed)
      {
         Console.WriteLine("Transaction successful! You have deposit " + this._amount.ToString("C2") + " to " + this._account.Name() + "'s account");
      }
      else if (this._success && Reversed)
      {
         Console.WriteLine("Transaction Undo! You have returned {0}", this._amount.ToString("C2"));
      }
      else if (!this._success && Executed)
      {
         Console.WriteLine("Transaction Failed!");
      }
      else
      {
         Console.WriteLine("Transaction Failed! Please contact us for further assistance");
      }
   }

   public override void Execute()
   {
      base.Execute();

      this._success = this._account.Deposit(this._amount);

      if(this._success == false)
      {
         throw new InvalidOperationException("Transaction Failed!");
      }
   }

   public override void Rollback()
   {
      base.Rollback();
      
      this.Reversed = this._account.Withdraw(this._amount);
      
      if(!Reversed)
      {
         throw new InvalidOperationException("Transaction Failed!");
      }

      Console.WriteLine("Funds returned");  
   }

}