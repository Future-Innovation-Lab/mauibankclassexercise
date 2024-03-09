using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingLocalMaui.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;
using SQLitePCL;

namespace BankingLocalMaui.Services
{
    public class BankingLocalDatabase
    {
        private SQLiteConnection _dbConnection;
  
        public string GetDatabasePath()
        {
           string filename = "bankingdata.db";
           string pathToDb = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
           return Path.Combine(pathToDb, filename);  
        }

        public BankingLocalDatabase()
        {

            _dbConnection = new SQLiteConnection(GetDatabasePath());

            _dbConnection.CreateTable<ClientType>();
            _dbConnection.CreateTable<BankAccountType>();
            _dbConnection.CreateTable<TransactionType>();
            _dbConnection.CreateTable<Bank>();
            _dbConnection.CreateTable<Client>();
            _dbConnection.CreateTable<BankAccount>();
            _dbConnection.CreateTable<Transaction>();

            SeedDatabase();
        }

        public void SeedDatabase()
        {
            if (_dbConnection.Table<ClientType>().Count() == 0)
            {

                ClientType clientType = new ClientType()
                {
                    ClientTypeDescription = "Private"
                };

                _dbConnection.Insert(clientType);

                clientType = new ClientType()
                {
                    ClientTypeDescription = "MVP"
                };
                _dbConnection.Insert(clientType);

                clientType = new ClientType()
                {
                    ClientTypeDescription = "Standard"
                };

                _dbConnection.Insert(clientType);
            }

            if (_dbConnection.Table<BankAccountType>().Count() == 0)
            {
                BankAccountType bankAccountType = new BankAccountType()
                {
                    BankAccountTypeDescription = "Credit"
                };

                _dbConnection.Insert(bankAccountType);

                bankAccountType = new BankAccountType()
                {
                    BankAccountTypeDescription = "Savings"
                };


                _dbConnection.Insert(bankAccountType);

                bankAccountType = new BankAccountType()
                {
                    BankAccountTypeDescription = "Cheque"
                };


                _dbConnection.Insert(bankAccountType);
            }

            if (_dbConnection.Table<TransactionType>().Count() == 0)
            {

                TransactionType transactionType = new TransactionType()
                {
                    TransactionTypeDescription = "Deposit"
                };

                _dbConnection.Insert(transactionType);

                transactionType = new TransactionType()
                {
                    TransactionTypeDescription = "Withdraw"
                };

                _dbConnection.Insert(transactionType);
            }

            if (_dbConnection.Table<Client>().Count() == 0)
            {

                Client client = new Client()
                {
                    FirstName = "Brandon",
                    Surname = "Mack",
                    SaIdNumber = "1843877687",
                    ContactNumber = "0825551234",
                    EmailAddress = "me@computer.com",
                    PhysicalAddress = "Swellendam"
                };


                Bank bank = new Bank()
                {
                    BankName = "Nedbank",
                    BranchCode = "12345"
                };

                _dbConnection.Insert(client);
                _dbConnection.Insert(bank);

                bank.Clients.Add(client);
                _dbConnection.UpdateWithChildren(bank);

                BankAccount bankAccount = new BankAccount();
                bankAccount.BankAccountNumber = "0001";
                bankAccount.BankAccountTypeId = 1;
                _dbConnection.Insert(bankAccount);

                client.BankAccounts.Add(bankAccount);
                _dbConnection.UpdateWithChildren(client);

                Transaction transaction = new Transaction();
                transaction.TransactionDate = DateTime.Now;
                transaction.TransactionDescription = "Deposited Money for lunch";
                transaction.TransactionAmount = 1000;

                bankAccount.DepositMoney(transaction);

                SaveTransaction(bankAccount, transaction);

                try
                {

                    transaction = new Transaction();
                    transaction.TransactionDate = DateTime.Now;
                    transaction.TransactionDescription = "Withdraw for lunch";
                    transaction.TransactionAmount = 10;

                    bankAccount.WithdrawMoney(transaction);

                    SaveTransaction(bankAccount, transaction);
                }
                catch (InvalidOperationException ex)
                {
                    // This is where you do message stuff 
                }
            }
        }


        public List<ClientType> GetAllClientTypes()
        {
          var clientTypes = _dbConnection.Table<ClientType>().ToList();

            return clientTypes;
        }


        public void SaveTransaction(BankAccount account,Transaction transaction)
        {
            _dbConnection.Insert(transaction);
            _dbConnection.UpdateWithChildren(account);
        }

        public List<Client> GetAllClients()
        {
            return _dbConnection.Table<Client>().ToList();
        }

        public Client GetClientBySaId(string saId)
        {
            return _dbConnection.Table<Client>().Where(x => x.SaIdNumber == saId).FirstOrDefault();
        }

        public void UpdateClient(Client client)
        {
            _dbConnection.Update(client);
        }

        public Client GetClientById(int id)
        {
            Client client = _dbConnection.Table<Client>().Where(x => x.ClientId == id).FirstOrDefault();

            if (client != null)
                _dbConnection.GetChildren(client, true);
            
            return client;
        }

        public List<Transaction> GetAllTransactions()
        {
            return _dbConnection.Table<Transaction>().ToList();
        }

    }
}
