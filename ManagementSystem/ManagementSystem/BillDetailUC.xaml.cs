using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for BillDetailUC.xaml
    /// </summary>
    public partial class BillDetailUC : UserControl
    {
        Grid main;
        int ID_Bill;

        public BillDetailUC(Grid grid, int id)
        {
            InitializeComponent();
            main = grid;
            ID_Bill = id;
        }

       
        private void ProductGTD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SaleDTG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void loadSale(object sender, RoutedEventArgs e)
        {
            List<Sale> loadObject = DataProvider.Ins.DB.Sales.ToList();
            for (int i = 0; i < loadObject.Count; i++)
                DisplayNameSale.Items.Add(loadObject[i].DisplayName);
            DisplayNameSale.SelectedIndex = 1;
        }

        private void ProductGTD_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_AddProduct(object sender, RoutedEventArgs e)
        {

        }

        private void btn_DelProduct(object sender, RoutedEventArgs e)
        {

        }

        private void btn_EditProduct(object sender, RoutedEventArgs e)
        {

        }

        private void loadProduct(object sender, RoutedEventArgs e)
        {
            List<Product> loadObject = DataProvider.Ins.DB.Products.ToList();
            for (int i = 0; i < loadObject.Count; i++)
                DisplayNameProduct.Items.Add(loadObject[i].DisplayName);
            DisplayNameProduct.SelectedIndex = 1;
        }

        private void loadCustomer(object sender, RoutedEventArgs e)
        {
            List<Customer> loadObject = DataProvider.Ins.DB.Customers.ToList();
            for (int i = 0; i < loadObject.Count; i++)
                DisplayNameCustomer.Items.Add(loadObject[i].DisplayName);
            DisplayNameCustomer.SelectedIndex = 1;
        }

        private void loadTransport(object sender, RoutedEventArgs e)
        {
            List<Transport> loadObject = DataProvider.Ins.DB.Transports.ToList();
            for (int i = 0; i < loadObject.Count; i++)
                DisplayNameTransport.Items.Add(loadObject[i].DisplayName);
            DisplayNameTransport.SelectedIndex = 1;
        }

        private void btn_Export(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Back(object sender, RoutedEventArgs e)
        {
            main.Children.Clear();
            UserControl usc = new BillUC(main);
            main.Children.Add(usc);
        }

        private void DisplayNameSale_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DisplayNameCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
