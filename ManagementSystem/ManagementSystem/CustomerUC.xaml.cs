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
    /// Interaction logic for CustomerUC.xaml
    /// </summary>
    public partial class CustomerUC : UserControl
    {
        public CustomerUC()
        {
            InitializeComponent();
            Load();
        }

        private void Load()
        {
            CustomerDTG.ItemsSource = DataProvider.Ins.DB.Customers.ToList();
        }

        private void setNull()
        {
            DisplayName.Text = "";
            Address.Text = "";
            Phone.Text = "";
            Email.Text = "";
            MoreInfo.Text = "";
        }

        private void btn_Add(object sender, RoutedEventArgs e)
        {
            if (checkInput())
            {
                Customer newCus = new Customer()
                {
                    DisplayName = DisplayName.Text,
                    AddressCus = Address.Text,
                    Phone = Phone.Text,
                    Mail = Email.Text,
                    MoreInfo = MoreInfo.Text
                };
                DataProvider.Ins.DB.Customers.Add(newCus);
                DataProvider.Ins.DB.SaveChanges();
                Load(); setNull();
            }
            else
            {
                
            }
        }

        bool checkInput()
        {
            if (DisplayName.Text == "")
                return false;
            if (Phone.Text!="")
                return Phone.Text.All(char.IsDigit);
            return true;
        }

        private void btn_Del(object sender, RoutedEventArgs e)
        {
            try
            {
                int ID = (CustomerDTG.SelectedItem as Customer).ID;
                var del = DataProvider.Ins.DB.Customers.Where(m => m.ID == ID).Single();
                DataProvider.Ins.DB.Customers.Remove(del);
                DataProvider.Ins.DB.SaveChanges();
                Load(); setNull();
            }
            catch
            {

            }
        }

        private void btn_Edit(object sender, RoutedEventArgs e)
        {
            if (checkInput())
            {
                int ID = (CustomerDTG.SelectedItem as Customer).ID;
                Customer updateUnit = (from m in DataProvider.Ins.DB.Customers
                                   where m.ID == ID
                                   select m).Single();
                updateUnit.DisplayName = DisplayName.Text;
                updateUnit.AddressCus = Address.Text;
                updateUnit.Phone = Phone.Text;
                updateUnit.Mail = Email.Text;
                updateUnit.MoreInfo = MoreInfo.Text;
                DataProvider.Ins.DB.SaveChanges();
                Load(); setNull();
            }
        }

        private void doubleClick(object sender, MouseButtonEventArgs e)
        {
            Customer tmp = CustomerDTG.SelectedItem as Customer;
            DisplayName.Text = tmp.DisplayName;
            Address.Text = tmp.AddressCus;
            Phone.Text = tmp.Phone;
            Email.Text = tmp.Mail;
            MoreInfo.Text = tmp.MoreInfo;
        }
    }
}
