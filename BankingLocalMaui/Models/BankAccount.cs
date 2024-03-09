using SQLite;
using SQLiteNetExtensions.Attributes;

namespace BankingLocalMaui.Models
{
    public class BankAccount
    {
        [PrimaryKey,AutoIncrement]
        public int BankAccountId { get; set; }
        public decimal Balance { get; set; }
        public string BankAccountNumber { get; set; }

        [ForeignKey(typeof(BankAccountType))]
        public int BankAccountTypeId { get; set; }

        [OneToOne]
        public BankAccountType BankAccountType { get; set; }

        [ForeignKey(typeof(Client))]
        public int ClientId { get; set; }

        [ManyToOne]
        public Client Client { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Transaction> Transactions { get; set; }


        public BankAccount()
        {
            Transactions = new List<Transaction>();
        }

        public void DepositMoney(Transaction transaction)
        {
            Balance = transaction.TransactionAmount + Balance;
            transaction.TransactionTypeId = 1;
            Transactions.Add(transaction);

        }

        public void WithdrawMoney(Transaction transaction)
        {
            if (transaction.TransactionAmount <= Balance)
            {
                Balance -= transaction.TransactionAmount;
                transaction.TransactionTypeId = 2;

                Transactions.Add(transaction);

            }
            else
            {
                throw new InvalidOperationException("Insufficient Funds");
            }

          
        }

    }
}
