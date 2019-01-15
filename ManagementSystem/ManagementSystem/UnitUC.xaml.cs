using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for UnitUC.xaml
    /// </summary>
    public partial class UnitUC : UserControl
    {
        public UnitUC()
        {
            InitializeComponent();
            Load();
        }

        private void Load()
        {
            UnitDataGrid.ItemsSource = DataProvider.Ins.DB.Units.ToList();
        }

        private void setNull()
        {
            UnitDisplayName.Text = "";
        }

        bool checkInput()
        {
            if (UnitDisplayName.Text == "")
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
                id = (from m in DataProvider.Ins.DB.Units
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
            if (checkInput() && checkExist(UnitDisplayName.Text))
            {
                Unit newUnit = new Unit() { DisplayName = UnitDisplayName.Text };
                DataProvider.Ins.DB.Units.Add(newUnit);
                DataProvider.Ins.DB.SaveChanges();
                Load(); setNull();
            }

        }

        private void btn_Edit(object sender, RoutedEventArgs e)
        {
            if (checkInput() && checkExist(UnitDisplayName.Text))
            {
                int ID = (UnitDataGrid.SelectedItem as Unit).ID;
                Unit updateUnit = (from m in DataProvider.Ins.DB.Units
                                   where m.ID == ID
                                   select m).Single();
                updateUnit.DisplayName = UnitDisplayName.Text;

                DataProvider.Ins.DB.SaveChanges();
                Load(); setNull();
            }
            
        }

        private void btn_Del(object sender, RoutedEventArgs e)
        {
            try
            {
                int ID = (UnitDataGrid.SelectedItem as Unit).ID;
                var deleteUnit = DataProvider.Ins.DB.Units.Where(m => m.ID == ID).Single();
                DataProvider.Ins.DB.Units.Remove(deleteUnit);
                DataProvider.Ins.DB.SaveChanges();
                Load(); setNull();
            }
            catch{

            }
        }


        private void doubleClick(object sender, MouseButtonEventArgs e)
        {
            Unit tmp = (UnitDataGrid.SelectedItem as Unit);
            UnitDisplayName.Text = tmp.DisplayName;
        }
    }
}
