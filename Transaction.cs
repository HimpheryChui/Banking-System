namespace BankingSystem;

abstract class Transaction { 
   protected decimal _amount;
   protected bool _success;
   private bool _executed;
   private bool _reversed;
   private DateTime _dateStamp;

   //construtor
   public Transaction (decimal amount)
   {
      this._amount = amount;
      this._executed = false;
      this._success = false;
      this._reversed = false;
   }

   //accessor
     public virtual bool Success()
   {
      return _success;
   }

   public bool Executed{
      get{return _executed;}
      private set{ _executed = value;}
   }

   public bool Reversed{
      get{return _reversed;}
      protected set{ _reversed = value;}
   }

   public DateTime DateStamp{
      get{return _dateStamp;}
      private set{ _dateStamp = value;}
   }

   //method
   public abstract void Print();

   public virtual void Execute(){
      
      if(!Executed)
      {
         Executed = true;
         _dateStamp = DateTime.Now;
      }
      else if (Executed)
      {
         throw new InvalidOperationException("Transaction has been executed!");
      }
      
   }

   public virtual void Rollback(){
      

      if(!this._success || Reversed)
      {
         throw new InvalidOperationException("Transaction has not performed or Roll back has already performed.");
      }
      else if(this._success && !Reversed)
      {
         _dateStamp = DateTime.Now;
      }
   }

}