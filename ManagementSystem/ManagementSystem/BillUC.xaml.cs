using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ManagementSystem
{
    /// <summary>
    /// Interaction logic for BillUC.xaml
    /// </summary>
    public partial class BillUC : UserControl
    {
        Grid main;
        public BillUC(Grid grid)
        {
            InitializeComponent();
            main = grid;
            Load();
        }

        private void Load()
        {
            BillDTG.ItemsSource = DataProvider.Ins.DB.Bills.ToList();
        }

        private void setNull()
        {
            DisplayNameCustomer.SelectedIndex = 1;
            DisplayNameSale.SelectedIndex = 1;
            DisplayNameTransport.SelectedIndex = 1;
        }

        bool checkInput()
        {
            if (DisplayNameCustomer.SelectedIndex == -1 || DateBill.SelectedDate.HasValue == false)
            {
                return false;
            }
            return true;
        }

        private void btn_Add(object sender, RoutedEventArgs e)
        {
            if (checkInput())
            {
                Bill newObject = new Bill()
                {
                    DateBill = DateBill.SelectedDate,
                    ID_Customer = (from m in DataProvider.Ins.DB.Customers
                                   where m.DisplayName == DisplayNameCustomer.SelectedItem.ToString()
                                   select m.ID).Single(),
                    
                };
                if (DisplayNameSale.SelectedIndex != -1)
                {
                    newObject.ID_Sale = (from m in DataProvider.Ins.DB.Sales
                           where m.DisplayName == DisplayNameSale.SelectedItem.ToString()
                           select m.ID).Single();
                }
                if (DisplayNameTransport.SelectedIndex != -1)
                {
                    newObject.ID_Transport = (from m in DataProvider.Ins.DB.Transports
                                              where m.DisplayName == DisplayNameTransport.SelectedItem.ToString()
                                              select m.ID).Single();
                }

                DataProvider.Ins.DB.Bills.Add(newObject);
                DataProvider.Ins.DB.SaveChanges();
                Load(); setNull();


            }
        }

        private void btn_Del(object sender, RoutedEventArgs e)
        {
            try
            {
                int ID = (BillDTG.SelectedItem as Bill).ID;
                var deleteObject = DataProvider.Ins.DB.Bills.Where(m => m.ID == ID).Single();
                DataProvider.Ins.DB.Bills.Remove(deleteObject);
                DataProvider.Ins.DB.SaveChanges();
                Load(); setNull();
            }
            catch
            {

            }
        }

        private void btn_Edit(object sender, RoutedEventArgs e)
        {
            if (checkInput() )
            {
                int ID = (BillDTG.SelectedItem as Bill).ID;
                Bill updateObject = (from m in DataProvider.Ins.DB.Bills
                                        where m.ID == ID
                                        select m).Single();
                updateObject.DateBill = DateBill.SelectedDate;
                updateObject.ID_Customer = (from m in DataProvider.Ins.DB.Customers
                               where m.DisplayName == DisplayNameCustomer.SelectedItem.ToString()
                               select m.ID).Single();

                if (DisplayNameSale.SelectedIndex != -1)
                {
                    updateObject.ID_Sale = (from m in DataProvider.Ins.DB.Sales
                                         where m.DisplayName == DisplayNameSale.SelectedItem.ToString()
                                         select m.ID).Single();
                }
                if (DisplayNameTransport.SelectedIndex != -1)
                {
                    updateObject.ID = (from m in DataProvider.Ins.DB.Transports
                                              where m.DisplayName == DisplayNameTransport.SelectedItem.ToString()
                                              select m.ID).Single();
                }

                DataProvider.Ins.DB.SaveChanges();
                Load(); setNull();
            }
        }

        private void btn_BillDetail(object sender, RoutedEventArgs e)
        {
            try
            {
                int ID = (BillDTG.SelectedItem as Bill).ID;
                main.Children.Clear();
                UserControl usc = new BillDetailUC(main, ID);
                main.Children.Add(usc);
            }
            catch
            {

            }
            
        }

        private void BillDTG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Bill tmp = (BillDTG.SelectedItem as Bill);
                DisplayNameCustomer.Text = tmp.Customer.DisplayName;
                if (tmp.ID_Sale != null)
                    DisplayNameSale.Text = tmp.Sale.DisplayName;
                else
                    DisplayNameSale.SelectedIndex = -1;
                if (tmp.ID_Transport != null)
                    DisplayNameTransport.Text = tmp.Transport.DisplayName;
                else
                    DisplayNameTransport.SelectedIndex = -1;
                DateBill.SelectedDate = tmp.DateBill;
            }
            catch
            {

            }
            
        }

        private void loadSale(object sender, RoutedEventArgs e)
        {
            List<Sale> loadObject = DataProvider.Ins.DB.Sales.ToList();
            for (int i = 0; i < loadObject.Count; i++)
                DisplayNameSale.Items.Add(loadObject[i].DisplayName);
            //DisplayNameSale.SelectedIndex = 1;
        }

        private void loadTransport(object sender, RoutedEventArgs e)
        {
            List<Transport> loadObject = DataProvider.Ins.DB.Transports.ToList();
            for (int i = 0; i < loadObject.Count; i++)
                DisplayNameTransport.Items.Add(loadObject[i].DisplayName);
            //DisplayNameTransport.SelectedIndex = 1;
        }

        private void loadCustomer(object sender, RoutedEventArgs e)
        {
            List<Customer> loadObject = DataProvider.Ins.DB.Customers.ToList();
            for (int i = 0; i < loadObject.Count; i++)
                DisplayNameCustomer.Items.Add(loadObject[i].DisplayName);
        }

       
    }
}
