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
        int ID_Bills;

        public BillDetailUC(Grid grid, int id)
        {
            InitializeComponent();
            main = grid;
            ID_Bills = id;
            Load();
        }

        private void Load()
        {
            List<BillDetail> details = (from m in DataProvider.Ins.DB.BillDetails
                                        where m.ID_Bill == ID_Bills
                                        select m).ToList();
            BillDetailDTG.ItemsSource = details;
        }

        private void setNull()
        {
            DisplayNameProduct.SelectedIndex = -1;
            Count.Text = "";
        }

        private bool checkInput()
        {
            if (DisplayNameProduct.SelectedIndex == -1 || Count.Text=="")
            {
                return false;
            }

            Product tmp = (from m in DataProvider.Ins.DB.Products
                           where m.DisplayName == DisplayNameProduct.SelectedItem.ToString()
                           select m).Single();
            if (int.Parse(Count.Text) < 0 || int.Parse(Count.Text) > tmp.Counts) 
                return false;
            return true;
        }

        private void updatePriceBill()
        {
            //Update count for product
            Bill updateObject = (from m in DataProvider.Ins.DB.Bills
                                 where m.ID == ID_Bills
                                 select m).Single();

            List<long?> list = (from m in DataProvider.Ins.DB.BillDetails
                             where m.ID_Bill == ID_Bills
                             select (m.SumPrice)).ToList();
            long sum = 0;
            for(int i = 0; i < list.Count; i++)
            {
                sum += list[i] ?? 0;
            }
            
            updateObject.SumPrice = sum;
            DataProvider.Ins.DB.SaveChanges();
        }
       
        private void btn_AddProduct(object sender, RoutedEventArgs e)
        {
            if (checkInput())
            {
                Product tmp = (from m in DataProvider.Ins.DB.Products
                               where m.DisplayName == DisplayNameProduct.SelectedItem.ToString()
                               select m).Single();
                BillDetail newObject = new BillDetail()
                {
                    ID_Bill = ID_Bills,
                    ID_Product = (from m in DataProvider.Ins.DB.Products
                                  where m.DisplayName == DisplayNameProduct.SelectedItem.ToString()
                                  select m.ID).Single(),
                    SumCount = int.Parse(Count.Text),
                    SumPrice = int.Parse(Count.Text) * tmp.Price
                };
                DataProvider.Ins.DB.BillDetails.Add(newObject);
                DataProvider.Ins.DB.SaveChanges();
                Load(); setNull();

                //Update count for product
                Product updateObject = (from m in DataProvider.Ins.DB.Products
                                        where m.ID == newObject.ID_Product
                                        select m).Single();
                updateObject.Counts = updateObject.Counts - newObject.SumCount;
                DataProvider.Ins.DB.SaveChanges();
                updatePriceBill();
            }
        }

        private void btn_DelProduct(object sender, RoutedEventArgs e)
        {
            try
            {
                int ID = (BillDetailDTG.SelectedItem as BillDetail).ID;
                var deleteObject = DataProvider.Ins.DB.BillDetails.Where(m => m.ID == ID).Single();
                DataProvider.Ins.DB.BillDetails.Remove(deleteObject);
                DataProvider.Ins.DB.SaveChanges();
                Load(); setNull();

                //Update count for product
                Product updateProduct = (from m in DataProvider.Ins.DB.Products
                                         where m.ID == deleteObject.ID_Product
                                         select m).Single();
                updateProduct.Counts = updateProduct.Counts + deleteObject.SumCount;
                DataProvider.Ins.DB.SaveChanges();
                updatePriceBill();
            }
            catch
            {

            }
        }

        private void btn_EditProduct(object sender, RoutedEventArgs e)
        {
            if (checkInput())
            {
                Product tmp = (from m in DataProvider.Ins.DB.Products
                               where m.DisplayName == DisplayNameProduct.SelectedItem.ToString()
                               select m).Single();
                int ID = (BillDetailDTG.SelectedItem as BillDetail).ID;
                BillDetail updateObject = (from m in DataProvider.Ins.DB.BillDetails
                                     where m.ID == ID
                                     select m).Single();
                int oldCount = updateObject.SumCount ?? 0;
                updateObject.ID_Product = (from m in DataProvider.Ins.DB.Products
                                           where m.DisplayName == DisplayNameProduct.SelectedItem.ToString()
                                           select m.ID).Single();
                updateObject.SumCount = int.Parse(Count.Text);
                updateObject.SumPrice = int.Parse(Count.Text) * tmp.Price;

                DataProvider.Ins.DB.SaveChanges();
                Load(); setNull();

                //Update count for product
                Product updateProduct = (from m in DataProvider.Ins.DB.Products
                                        where m.ID == updateObject.ID_Product
                                        select m).Single();
                updateProduct.Counts = updateProduct.Counts - updateObject.SumCount + oldCount;
                DataProvider.Ins.DB.SaveChanges();

                updatePriceBill();
            }
        }

        private void loadProduct(object sender, RoutedEventArgs e)
        {
            List<Product> loadObject = DataProvider.Ins.DB.Products.ToList();
            for (int i = 0; i < loadObject.Count; i++)
                DisplayNameProduct.Items.Add(loadObject[i].DisplayName);
            DisplayNameProduct.SelectedIndex = 1;
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

        private void ProductGTD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                BillDetail tmp = (BillDetailDTG.SelectedItem as BillDetail);
                DisplayNameProduct.Text = tmp.Product.DisplayName;
                Count.Text = $"{tmp.SumCount}";
            }
            catch
            {

            }
        }
    }
}
