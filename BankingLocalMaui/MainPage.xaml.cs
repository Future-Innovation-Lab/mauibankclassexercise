using BankingLocalMaui.Models;
using BankingLocalMaui.Services;
using System.Collections.ObjectModel;

namespace BankingLocalMaui
{
    public partial class MainPage : ContentPage
    {
        private BankingLocalDatabase _database;

        private Client _currentClient;

        public Client CurrentClient
        {
            get { return _currentClient; }
            set {
                _currentClient = value;

                OnPropertyChanged();

            }
        }

        private BankAccount _bankAccount;

        public BankAccount FirstBankAccont
        {
            get { return _bankAccount; }
            set { _bankAccount = value;

                OnPropertyChanged();
            }
        }


        private ObservableCollection<Transaction> _transactions;

        public ObservableCollection<Transaction> Transactions
        {
            get { return _transactions; }
            set {
                _transactions = value;

                OnPropertyChanged();
            
            }
        }



        public MainPage()
        {
            InitializeComponent();

            _database = new BankingLocalDatabase();

            BindingContext = this;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LoadData();
        }

        private void ReloadButton_Clicked(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            Client client = _database.GetClientById(1);

            CurrentClient = client;

            FirstBankAccont = CurrentClient.BankAccounts.FirstOrDefault();

            Transactions = new ObservableCollection<Transaction>(_database.GetAllTransactions());
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            _database.UpdateClient(CurrentClient);
        }
    }

}
