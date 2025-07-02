namespace BankingSystem;

class TransferTransaction : Transaction
{
   private Account _fromAccount;
   private Account _toAccount;
   private DepositTransaction _deposit;
   private WithdrawTransaction _withdraw;

   public TransferTransaction(Account fromAccount, Account toAccount, decimal amount) : base(amount)
   {
      this._fromAccount = fromAccount;
      this._toAccount = toAccount;
      this._deposit = new DepositTransaction(toAccount, amount);
      this._withdraw = new WithdrawTransaction(fromAccount, amount);
   }

   public override void Execute()
   {
      base.Execute();

      this._withdraw.Execute();
      
      if(this._withdraw.Success())
      {
         this._deposit.Execute();
      }
      else
      {
         throw new InvalidOperationException("Transaction Failed!");
      }
      _success = Success();

      }

   public override bool Success()
   {
      return this._withdraw.Success() && this._deposit.Success();
   }


   public override void Print()
   {
      if(Reversed == false)
      {
         Console.WriteLine("Transaction Status: ({0} to {1} - {2})", this._fromAccount.Name(), this._toAccount.Name(), this.DateStamp);

         if(Success())
         {
            Console.WriteLine("Transferred {0} from {1}'s account to {2}'s account", this._amount.ToString("C2"), this._fromAccount.Name(), this._toAccount.Name());
         }
         else
         {
            Console.WriteLine("Transaction Failed");
         }
      }
      else if(Reversed == true)
      {
         Console.WriteLine("Transaction Status: ({0} to {1} - {2})", this._toAccount.Name(), this._fromAccount.Name(), this.DateStamp);

         if(this._deposit.Reversed  && this._withdraw.Reversed)
         {
            Console.WriteLine("Transaction Undo! Transferred {0} from {1}'s account to {2}'s account", this._amount.ToString("C2"), this._toAccount.Name(), this._fromAccount.Name());
         }
         else
         {
            Console.WriteLine("Transaction Failed");
         }
      }
      
   }

   public override void Rollback()
   {
      base.Rollback();

      this._deposit.Rollback();
      this._withdraw.Rollback();

      this.Reversed = this._deposit.Reversed && this._withdraw.Reversed;

      Console.WriteLine("Rollback successful");
   }
}