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

        private void btn_Add(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Del(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Edit(object sender, RoutedEventArgs e)
        {

        }

        private void doubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
