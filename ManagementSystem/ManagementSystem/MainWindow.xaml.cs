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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_logout(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btn_ButtonOpenMenu(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void btn_ButtonCloseMenu(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            GridMain.Children.Clear();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemHome":
                    TitleFunction.Text = "Trang chủ";
                    usc = new HomeUC();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemBill":
                    TitleFunction.Text = "Thanh toán";
                    usc = new BillUC();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemProduct":
                    TitleFunction.Text = "Sản phẩm";
                    usc = new ProductUC();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemCustomer":
                    TitleFunction.Text = "Khách hàng";
                    usc = new CustomerUC();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemCategory":
                    TitleFunction.Text = "Loại sản phẩm";
                    usc = new CategoryUC();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemUnit":
                    TitleFunction.Text = "Đơn vị đo";
                    usc = new UnitUC();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemService":
                    TitleFunction.Text = "Dịch vụ";
                    usc = new ServiceUC();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemStatistical":
                    TitleFunction.Text = "Thống kê";
                    usc = new StatisticalUC();
                    GridMain.Children.Add(usc);
                    break;
                default:
                    break;
            }
        }
    }
}
