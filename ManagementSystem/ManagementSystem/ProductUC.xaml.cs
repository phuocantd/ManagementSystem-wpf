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
    /// Interaction logic for ProductUC.xaml
    /// </summary>
    public partial class ProductUC : UserControl
    {
        public ProductUC()
        {
            InitializeComponent();
            Load();
        }

        private void Load()
        {
            ProductDTG.ItemsSource = DataProvider.Ins.DB.Products.ToList();
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

        private void loadUnit(object sender, RoutedEventArgs e)
        {
            List<Unit> loadObject = DataProvider.Ins.DB.Units.ToList();
            for (int i=0;i< loadObject.Count;i++)
                DisplayNameUnit.Items.Add(loadObject[i].DisplayName);
        }

        private void loadCategory(object sender, RoutedEventArgs e)
        {
            List<Category> loadObject = DataProvider.Ins.DB.Categories.ToList();
            for (int i = 0; i < loadObject.Count; i++)
                DisplayNameCategory.Items.Add(loadObject[i].DisplayName);
        }
    }
}
