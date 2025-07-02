namespace BankingSystem;

class Bank
{
   private List<Account> _account;
   private List<Transaction> _transactions;

   public Bank()
   {
      _account = new List<Account>();
      _transactions = new List<Transaction>();
   }

   public List<Transaction> Transactions
   {
      get{return _transactions;}
   }

   public void AddAccount(Account account)
   {
      _account.Add(account);
   }

   public Account GetAccount(String name)
   {
      for(int i = 0; i < _account.Count; i++)
      {
         if(_account[i].Name() == name)
         {
            return _account[i];
         }
      }
      return null!;
   }

   public void ExecuteTransaction(Transaction transaction)
   {
      _transactions.Add(transaction);

      transaction.Execute();
   }

   public void RollbackTransaction(Transaction transaction)
   {
      transaction.Rollback();
   }

   public void PrintTranscationHistory()
   {
      int l = 0;
      for(int i = 0; i < _transactions.Count(); i++)
      {
         l = i+1;
         Console.Write("{0}: ", l);
         _transactions[i].Print();
      }
   }

}