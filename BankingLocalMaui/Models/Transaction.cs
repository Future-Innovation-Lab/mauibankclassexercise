using SQLite;
using SQLiteNetExtensions.Attributes;

namespace BankingLocalMaui.Models
{
    public class Transaction
    {
        [PrimaryKey, AutoIncrement]
        public int TransactionId { get; set; }

        public decimal TransactionAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionDescription { get; set; }

        [ForeignKey(typeof(TransactionType))]
        public int TransactionTypeId { get; set; }

        [OneToOne]
        public TransactionType TransactionType { get; set; }


        [ForeignKey(typeof(BankAccount))]
        public int BankAccountId { get; set; }

        [ManyToOne]
        public BankAccount BankAccount { get; set; }
    }
}
