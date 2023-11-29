using Homework16.Models;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Common;

using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


//ToDO Забиндить данные покупателя к форме Update


namespace Homework16
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        
        SQLiteContext SQLiteDb ;
        SQLServerContext sqlDb;

        #region ПОЛЯ
        private ObservableCollection<Customer> customers;

        public ObservableCollection<Customer> Customers
        {
            get { return customers; }
            set
            {
                customers = value;
                OnPropertyChanged("Customers");
            }
        }

        private ObservableCollection<Purchase> purchases;

        public ObservableCollection<Purchase> Purchases
        {
            get { return purchases; }
            set
            {
                purchases = value;
                OnPropertyChanged("Purchases");
            }
        }

        //private Customer newCustomer;

        //public Customer NewCustomer
        //{
        //    get { return newCustomer; }
        //    set
        //    {
        //        newCustomer = value;
        //        OnPropertyChanged("NewCustomer");
        //    }
        //}

        public Purchase NewPurchase { get; set; }

        
        private Customer currentCustomer;

        public Customer CurrentCustomer
        {
            get { return currentCustomer; }
            set 
            { 
                currentCustomer = value;
                using (SQLiteDb = new SQLiteContext())
                {
                    Purchases = new ObservableCollection<Purchase>(SQLiteDb.Purchases.Where(p => p.CustomerEmail == CurrentCustomer.Email).ToList());
                }
                
                OnPropertyChanged("CurrentCustomer");
            }
        }


        
        private Customer templateCustomer;

        public Customer NewCustomer
        {
            get { return templateCustomer; }
            set
            {
                templateCustomer = value;
                OnPropertyChanged("TemplateCustomer");
            }   
        }


        private Purchase currentPurchase;

        public Purchase CurrentPurchase
        {
            get { return currentPurchase; }
            set { currentPurchase = value;
                OnPropertyChanged("CurrentPurchase");
            }
        }

        #endregion

        #region КОМАНДЫ
        private AppCommand deleteCustomerCommand;

        public AppCommand DeleteCustomerCommand
        {
           
            get
            {
                return deleteCustomerCommand = new AppCommand(obj =>
                    {
                        Customer customer = obj as Customer;
                        int index = Customers.IndexOf(customer);
                        Customers.Remove(customer);
                        using (sqlDb = new SQLServerContext())
                        {
                            sqlDb.Customers.Remove(customer);
                            CurrentCustomer = Customers[index-1] ;
                            sqlDb.SaveChanges();                        

                        }
                        using (SQLiteDb = new SQLiteContext())
                        {
                            List<Purchase> list = new List<Purchase>();
                            list = SQLiteDb.Purchases.Where(p => p.CustomerEmail == customer.Email).ToList();
                                foreach (var item in list)
                                {
                                    SQLiteDb.Purchases.Remove(item);
                                }
                            SQLiteDb.SaveChanges();                        
                        }


                    },
                    (obj) => obj!=null);
                
            }
            

        }

        private AppCommand deletePurchaseCommand;
        public AppCommand DeletePurchaseCommand
        {

            get
            {
                return deletePurchaseCommand = new AppCommand(obj =>
                {
                        Purchase purchase = obj as Purchase;
                    using (SQLiteDb = new SQLiteContext())
                    {
                       
                            SQLiteDb.Purchases.Remove(purchase);
                        
                        SQLiteDb.SaveChanges();
                    }
                    
                        Purchases.Remove(purchase);
                },
                    (obj) => obj!=null);

            }


        }
        private AppCommand deleteAllCustomerPurchasesCommand;
        public AppCommand DeleteAllCustomerPurchasesCommand
        {

            get
            {
                return deleteAllCustomerPurchasesCommand = new AppCommand(obj =>
                {
                    Purchase purchase = obj as Purchase;
                    using (SQLiteDb = new SQLiteContext())
                    {
                        List<Purchase> list = new List<Purchase>();
                        list = SQLiteDb.Purchases.Where(p => p.CustomerEmail == CurrentCustomer.Email).ToList();

                        SQLiteDb.Purchases.RemoveRange(list);
                        foreach (var item in list)
                        {
                            Purchases.Remove(item);
                        }
                        
                        SQLiteDb.SaveChanges();
                        CurrentCustomer = CurrentCustomer;
                        
                    }

                   
                },
                    (obj) => Customers.Count > 0);

            }


        }

        private AppCommand showAllPurchases;
        public AppCommand ShowAllPurchases
        {

            get
            {
                return showAllPurchases = new AppCommand(obj =>
                {
                    
                    using (SQLiteDb = new SQLiteContext())
                    {
                        Purchases = new ObservableCollection<Purchase>(SQLiteDb.Purchases);
                    }
                },
                    (obj) => true);

            }


        }

        private AppCommand updateCustomerCommand;
        public AppCommand UpdateCustomerCommand
        {

            get
            {
                return updateCustomerCommand = new AppCommand(obj =>
                {
                    UpdateCustomerWindow updateCustomerWindow = new UpdateCustomerWindow();
                    updateCustomerWindow.DataContext = this;
                    updateCustomerWindow.ShowDialog();

                    if (updateCustomerWindow.DialogResult == true)
                    {
                        UpdateCustomer(CurrentCustomer);
                    }
                },
                    (obj) => obj!=null);

            }


        }

        private AppCommand btnCommand;
        public AppCommand BtnCommand
        {
            get
            {
                return btnCommand = new AppCommand(obj =>
                {
                    MessageBox.Show(CurrentCustomer.LastName + " " + CurrentCustomer.Name);

                },
                    (obj) => Customers.Count > 0);

            }
        }


        private AppCommand addPurchaseCommand;

        public AppCommand AddPurchaseCommand
        {
            get
            {
                return addPurchaseCommand = new AppCommand(obj =>
                {



                    AddPurchaseWindow addPurchaseWindow = new AddPurchaseWindow();

                    addPurchaseWindow.DataContext = this;
                    addPurchaseWindow.ShowDialog();
                    if (addPurchaseWindow.DialogResult == true)
                    {
                        NewPurchase.CustomerEmail = CurrentCustomer.Email;
                        using (SQLiteDb = new SQLiteContext())
                        {
                            SQLiteDb.Purchases.Add(NewPurchase);
                            SQLiteDb.SaveChanges();
                            Purchases.Add(NewPurchase);
                            NewPurchaseInit();
                        }
                    }

                },
                    (obj) => CurrentCustomer!=null);
            }
        }

        private AppCommand addCustomerCommand;
        public AppCommand AddCustomerCommand
        {
            get
            {
                return addCustomerCommand = new AppCommand(obj =>
                {
                    AddCustomerWindow addCustomerWindow = new AddCustomerWindow();
                    addCustomerWindow.DataContext = this;
                    addCustomerWindow.ShowDialog();
                    Customer customer = new Customer();

                    

                    if (addCustomerWindow.DialogResult == true)
                    {
                        
                        AddCustomer(NewCustomer);

                    }
                },
                    (obj) => true);


            }
        }

        #endregion



        public void AddCustomer(Customer customer)
        {
            using (SQLServerContext sQLServerContext = new SQLServerContext())
            {
                sQLServerContext.Customers.Add(customer);
                sQLServerContext.SaveChanges();
                Customers.Add(customer);
            }
            NewCustomerInit();
        }

        public void UpdateCustomer(Customer customer)
        {
            using (SQLServerContext sQLServerContext = new SQLServerContext())
            {
                sQLServerContext.Customers.Update(customer);
                sQLServerContext.SaveChanges();
                Customers = new ObservableCollection<Customer>(sQLServerContext.Customers);

            }
        }

        public void NewPurchaseInit()
        {
            NewPurchase = new Purchase() { Name = "Магнитофон `Sony`", Code = 7777 };
        }

        public void NewCustomerInit()
        {
            NewCustomer = new Customer() { Email = "email@mail.com", LastName = "Иванов", Name = "Иван", Phone = "777777", SurName = "Иванович" };
        }


        public MainViewModel()
        {
            NewCustomerInit();
            NewPurchaseInit();

            using (SQLiteDb = new SQLiteContext())
            {
                Purchases = new ObservableCollection<Purchase>( SQLiteDb.Purchases.ToList());
            }
            using (sqlDb = new SQLServerContext())
            {
                Customers = new ObservableCollection<Customer>(sqlDb.Customers);
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            
        }

    }
}
