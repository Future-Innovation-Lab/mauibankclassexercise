using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingLocalMaui.Models
{
    public class Client
    {
        [PrimaryKey,AutoIncrement]
        public int ClientId { get; set; }

        public string FirstName { get; set; }
        public string Surname { get; set; }

        public string EmailAddress { get; set; }

        public string SaIdNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string ContactNumber { get; set; }

        public string PhysicalAddress { get; set; }


        [ForeignKey(typeof(ClientType))]
        public int ClientTypeId { get; set; }

        
        [OneToOne]
        public ClientType ClientType { get; set; }


        [ForeignKey(typeof(Bank))]
        public int BankId { get; set; }

        [ManyToOne]
        public Bank Bank { get; set; }


        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<BankAccount> BankAccounts { get; set; }

        public Client() {

            BankAccounts = new List<BankAccount>();

        }
    }
}
