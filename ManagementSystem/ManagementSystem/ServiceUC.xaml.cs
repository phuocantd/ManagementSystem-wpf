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
    /// Interaction logic for ServiceUC.xaml
    /// </summary>
    public partial class ServiceUC : UserControl
    {
        public ServiceUC()
        {
            InitializeComponent();
            LoadTransport();
            LoadSale();
        }
        private void LoadTransport()
        {
            TransportDTG.ItemsSource = DataProvider.Ins.DB.Transports.ToList();
        }
        private void LoadSale()
        {
            SaleDTG.ItemsSource = DataProvider.Ins.DB.Sales.ToList();
        }

        private void setNullTransport()
        {
            DisplayNameTransport.Text = "";
            PriceTransport.Text = "";
        }
        private void setNullSale()
        {
            DisplayNameSale.Text = "";
            PercentSale.Text = "";
        }

        bool checkInputTransport()
        {
            if (DisplayNameTransport.Text == "" || PriceTransport.Text=="")
            {
                return false;
            }
            return true;
        }
        bool checkInputSale()
        {
            if (DisplayNameSale.Text == "" || PercentSale.Text == "")
            {
                return false;
            }
            return true;
        }

        bool checkExistTransport()
        {
            try
            {
                if (!PriceTransport.Text.All(char.IsDigit))
                {
                    return false;
                }
                return true;
                //int id = 0;
                //id = (from m in DataProvider.Ins.DB.Transports
                //      where m.DisplayName == DisplayNameTransport.Text
                //      select 1).Single();
                //return id == 1 ? false : true;
            }
            catch
            {
                return true;
            }
        }
        bool checkExistSale()
        {
            try
            {
                if (!PercentSale.Text.All(char.IsDigit) || int.Parse(PercentSale.Text)>100)
                {
                    return false;
                }
                return true;
                //int id = 0;
                //Sale tmp = (SaleDTG.SelectedItem as Sale);
                //id = (from m in DataProvider.Ins.DB.Sales
                //      where m.DisplayName == DisplayNameSale.Text && m.ID!=tmp.ID
                //      select 1).Single();
                //return id == 1 ? false : true;
            }
            catch
            {
                return true;
            }
        }

        private void btn_AddTransport(object sender, RoutedEventArgs e)
        {
            if (checkInputTransport() && checkExistTransport())
            {
                Transport newObject = new Transport()
                {
                    DisplayName = DisplayNameTransport.Text,
                    Price = int.Parse(PriceTransport.Text),
                };
                DataProvider.Ins.DB.Transports.Add(newObject);
                DataProvider.Ins.DB.SaveChanges();
                LoadTransport(); setNullTransport();
            }
        }

        private void btn_DelTransport(object sender, RoutedEventArgs e)
        {
            try
            {
                int ID = (TransportDTG.SelectedItem as Transport).ID;
                var deleteObject = DataProvider.Ins.DB.Transports.Where(m => m.ID == ID).Single();
                DataProvider.Ins.DB.Transports.Remove(deleteObject);
                DataProvider.Ins.DB.SaveChanges();
                LoadTransport(); setNullTransport();
            }
            catch
            {

            }
        }

        private void btn_EditTransport(object sender, RoutedEventArgs e)
        {
            if (checkInputTransport() && checkExistTransport())
            {
                int ID = (TransportDTG.SelectedItem as Transport).ID;
                Transport updateObject = (from m in DataProvider.Ins.DB.Transports
                                        where m.ID == ID
                                        select m).Single();
                updateObject.DisplayName = DisplayNameTransport.Text;
                updateObject.Price = int.Parse(PriceTransport.Text);

                DataProvider.Ins.DB.SaveChanges();
                LoadTransport(); setNullTransport();
            }
        }

        private void TransportDTG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Transport tmp = (TransportDTG.SelectedItem as Transport);
                DisplayNameTransport.Text = tmp.DisplayName;
                PriceTransport.Text = $"{tmp.Price}";
            }
            catch
            {

            }
        }

        private void btn_AddSale(object sender, RoutedEventArgs e)
        {
            if (checkInputSale() && checkExistSale())
            {
                Sale newObject = new Sale()
                {
                    DisplayName = DisplayNameSale.Text,
                    PercentSale = int.Parse(PercentSale.Text),
                };
                DataProvider.Ins.DB.Sales.Add(newObject);
                DataProvider.Ins.DB.SaveChanges();
                LoadSale(); setNullSale();
            }
        }

        private void btn_DelSale(object sender, RoutedEventArgs e)
        {
            try
            {
                int ID = (SaleDTG.SelectedItem as Sale).ID;
                var deleteObject = DataProvider.Ins.DB.Sales.Where(m => m.ID == ID).Single();
                DataProvider.Ins.DB.Sales.Remove(deleteObject);
                DataProvider.Ins.DB.SaveChanges();
                LoadSale(); setNullSale();
            }
            catch
            {

            }
        }

        private void btn_EditSale(object sender, RoutedEventArgs e)
        {
            if (checkInputSale() && checkExistSale())
            {
                int ID = (SaleDTG.SelectedItem as Sale).ID;
                Sale updateObject = (from m in DataProvider.Ins.DB.Sales
                                          where m.ID == ID
                                          select m).Single();
                updateObject.DisplayName = DisplayNameSale.Text;
                updateObject.PercentSale = int.Parse(PercentSale.Text);

                DataProvider.Ins.DB.SaveChanges();
                LoadSale(); setNullSale();
            }
        }

        private void SaleDTG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Sale tmp = (SaleDTG.SelectedItem as Sale);
                DisplayNameSale.Text = tmp.DisplayName;
                PercentSale.Text = $"{tmp.PercentSale}";
            }
            catch
            {

            }
        }

        
    }
}
