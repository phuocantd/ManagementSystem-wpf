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

        private void setNull()
        {
            DisplayName.Text = "";
            Count.Text = "";
            Price.Text = "";
        }

        bool checkInput()
        {
            if (DisplayName.Text == "" || DisplayNameUnit.SelectedIndex==-1||DisplayNameCategory.SelectedIndex==-1||Count.Text==""||Price.Text=="")
            {
                return false;
            }
            return true;
        }

        bool checkExist()
        {
            try
            {
                if (!Price.Text.All(char.IsDigit) || !Count.Text.All(char.IsDigit))
                {
                    return false;
                }
                int id = 0;
                id = (from m in DataProvider.Ins.DB.Products
                      where m.DisplayName == DisplayName.Text
                      select 1).Single();
                return id == 1 ? false : true;
            }
            catch
            {
                return true;
            }
        }

        private void btn_Add(object sender, RoutedEventArgs e)
        {
            if (checkInput() && checkExist())
            {
                Product newObject = new Product()
                {
                    DisplayName = DisplayName.Text,
                    ID_Unit = (from m in DataProvider.Ins.DB.Units
                               where m.DisplayName == DisplayNameUnit.SelectedItem.ToString()
                               select m.ID).Single(),
                    ID_Category = (from m in DataProvider.Ins.DB.Categories
                                   where m.DisplayName == DisplayNameCategory.SelectedItem.ToString()
                                   select m.ID).Single(),
                    Price = double.Parse(Price.Text),
                    Counts = int.Parse(Count.Text)
                };
                DataProvider.Ins.DB.Products.Add(newObject);
                DataProvider.Ins.DB.SaveChanges();
                Load(); setNull();
            }
        }

        private void btn_Del(object sender, RoutedEventArgs e)
        {
            try
            {
                int ID = (ProductDTG.SelectedItem as Product).ID;
                var deleteObject = DataProvider.Ins.DB.Products.Where(m => m.ID == ID).Single();
                DataProvider.Ins.DB.Products.Remove(deleteObject);
                DataProvider.Ins.DB.SaveChanges();
                Load(); setNull();
            }
            catch
            {

            }
        }

        private void btn_Edit(object sender, RoutedEventArgs e)
        {
            if (checkInput() && checkExist())
            {
                int ID = (ProductDTG.SelectedItem as Product).ID;
                Product updateObject = (from m in DataProvider.Ins.DB.Products
                                         where m.ID == ID
                                         select m).Single();
                updateObject.DisplayName = DisplayName.Text;
                updateObject.ID_Unit = (from m in DataProvider.Ins.DB.Units
                           where m.DisplayName == DisplayNameUnit.SelectedItem.ToString()
                           select m.ID).Single();

                updateObject.ID_Category = (from m in DataProvider.Ins.DB.Categories
                               where m.DisplayName == DisplayNameCategory.SelectedItem.ToString()
                               select m.ID).Single();
                updateObject.Price = double.Parse(Price.Text);
                updateObject.Counts = int.Parse(Count.Text);

                DataProvider.Ins.DB.SaveChanges();
                Load(); setNull();
            }
        }

        private void loadUnit(object sender, RoutedEventArgs e)
        {
            List<Unit> loadObject = DataProvider.Ins.DB.Units.ToList();
            for (int i=0;i< loadObject.Count;i++)
                DisplayNameUnit.Items.Add(loadObject[i].DisplayName);
            DisplayNameUnit.SelectedIndex = 1;
        }

        private void loadCategory(object sender, RoutedEventArgs e)
        {
            List<Category> loadObject = DataProvider.Ins.DB.Categories.ToList();
            for (int i = 0; i < loadObject.Count; i++)
                DisplayNameCategory.Items.Add(loadObject[i].DisplayName);
            DisplayNameCategory.SelectedIndex = 1;
        }

        private void ProductDTG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Product tmp = (ProductDTG.SelectedItem as Product);
            DisplayName.Text = tmp.DisplayName;
            DisplayNameUnit.Text = tmp.Unit.DisplayName;
            DisplayNameCategory.Text = tmp.Category.DisplayName;
            Price.Text = $"{tmp.Price}";
            Count.Text = $"{tmp.Counts}";
        }
    }
}
