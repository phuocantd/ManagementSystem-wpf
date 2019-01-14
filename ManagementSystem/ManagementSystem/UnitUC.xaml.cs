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
        Unit tmp;

        public UnitUC()
        {
            InitializeComponent();
            Load();
        }

        private void Load()
        {
            UnitDataGrid.ItemsSource = DataProvider.Ins.DB.Units.ToList();
        }

        private void btn_Add(object sender, RoutedEventArgs e)
        {
            if (UnitDisplayName.Text == "")
            {

            }
            else
            {
                Unit newUnit = new Unit() { DisplayName = UnitDisplayName.Text };
                DataProvider.Ins.DB.Units.Add(newUnit);
                DataProvider.Ins.DB.SaveChanges();
                Load();
            }
            
        }

        private void btn_Edit(object sender, RoutedEventArgs e)
        {
            if (UnitDisplayName.Text == "")
            {

            }
            else
            {
                int ID = (UnitDataGrid.SelectedItem as Unit).ID;
                Unit updateUnit = (from m in DataProvider.Ins.DB.Units
                                   where m.ID == ID
                                   select m).Single();
                updateUnit.DisplayName = UnitDisplayName.Text;

                DataProvider.Ins.DB.SaveChanges();
                Load();
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
                Load();
            }
            catch{

            }
        }


        private void doubleClick(object sender, MouseButtonEventArgs e)
        {
            tmp = (UnitDataGrid.SelectedItem as Unit);
            UnitDisplayName.Text = tmp.DisplayName;
        }
    }
}
