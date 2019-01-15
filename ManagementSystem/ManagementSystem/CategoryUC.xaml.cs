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
    /// Interaction logic for CategoryUC.xaml
    /// </summary>
    public partial class CategoryUC : UserControl
    {
        public CategoryUC()
        {
            InitializeComponent();
            Load();
        }

        private void Load()
        {
            CategoryDTG.ItemsSource = DataProvider.Ins.DB.Categories.ToList();
        }

        private void setNull()
        {
            DisplayName.Text = "";
        }

        bool checkInput()
        {
            if (DisplayName.Text == "") 
            {
                return false;
            }
            return true;
        }

        bool checkExist(string name)
        {
            try
            {
                int id = 0;
                id = (from m in DataProvider.Ins.DB.Categories
                      where m.DisplayName == name
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
            if (checkInput() && checkExist(DisplayName.Text))
            {
                Category newObject = new Category() { DisplayName = DisplayName.Text };
                DataProvider.Ins.DB.Categories.Add(newObject);
                DataProvider.Ins.DB.SaveChanges();
                Load(); setNull();
            }
        }

        private void btn_Del(object sender, RoutedEventArgs e)
        {
            try
            {
                int ID = (CategoryDTG.SelectedItem as Category).ID;
                var deleteObject = DataProvider.Ins.DB.Categories.Where(m => m.ID == ID).Single();
                DataProvider.Ins.DB.Categories.Remove(deleteObject);
                DataProvider.Ins.DB.SaveChanges();
                Load(); setNull();
            }
            catch
            {

            }
        }

        private void btn_Edit(object sender, RoutedEventArgs e)
        {
            if (checkInput() && checkExist(DisplayName.Text))
            {
                int ID = (CategoryDTG.SelectedItem as Category).ID;
                Category updateObject = (from m in DataProvider.Ins.DB.Categories
                                   where m.ID == ID
                                   select m).Single();
                updateObject.DisplayName = DisplayName.Text;

                DataProvider.Ins.DB.SaveChanges();
                Load(); setNull();
            }
        }

        private void doubleClick(object sender, MouseButtonEventArgs e)
        {
            Category tmp = (CategoryDTG.SelectedItem as Category);
            DisplayName.Text = tmp.DisplayName;
        }
    }
}
