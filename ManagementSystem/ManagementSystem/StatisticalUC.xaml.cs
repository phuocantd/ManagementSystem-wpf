using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for StatisticalUC.xaml
    /// </summary>
    public partial class StatisticalUC : UserControl
    {
        List<Bar> _bar = new List<Bar>();

        public StatisticalUC()
        {
            InitializeComponent();
            //_bar.Add(new Bar() { BarName = "Rajesh", Value = 380 });
            //_bar.Add(new Bar() { BarName = "Suresh", Value = 60 });
            //_bar.Add(new Bar() { BarName = "Dan", Value = 40 });
            //_bar.Add(new Bar() { BarName = "Sevenx", Value = 67 });
            //_bar.Add(new Bar() { BarName = "Patel", Value = 15 });
            //_bar.Add(new Bar() { BarName = "Joe", Value = 70 });
            //_bar.Add(new Bar() { BarName = "Bill", Value = 90 });
            //_bar.Add(new Bar() { BarName = "Vlad", Value = 23 });
            //_bar.Add(new Bar() { BarName = "Steve", Value = 12 });
            //_bar.Add(new Bar() { BarName = "Pritam", Value = 100 });
            //_bar.Add(new Bar() { BarName = "Genis", Value = 54 });
            //_bar.Add(new Bar() { BarName = "Ponnan", Value = 84 });
            //_bar.Add(new Bar() { BarName = "Mathew", Value = 43 });
            Load();
            this.DataContext = new RecordCollection(_bar);
        }

        private void Load()
        {
            List<Product> products = DataProvider.Ins.DB.Products.ToList();
            List<BillDetail> billDetails = DataProvider.Ins.DB.BillDetails.ToList();
            for (int i = 0; i < products.Count; i++)
            {
                int input = products[i].Counts ?? 1;
                int output = 0;

                for(int j = 0; j < billDetails.Count; j++)
                {
                    if(billDetails[j].ID_Product == products[i].ID)
                    {
                        int tmp = billDetails[j].SumCount ?? 0;
                        output += tmp;
                    }
                }

                //List<int?> list = (from m in DataProvider.Ins.DB.BillDetails
                //                   where m.ID_Product == products[i].ID
                //                   select (m.SumCount)).ToList();
                //for (int j = 0; j < list.Count; j++)
                //{
                //    output += list[j] ?? 0;
                //}
                //try
                //{

                //}
                //catch
                //{

                //}

                int per = 100*output / (input + output);
                _bar.Add(new Bar() { BarName = products[i].DisplayName, Value = per });
            }
        }



    }

    class RecordCollection : ObservableCollection<Record>
    {

        public RecordCollection(List<Bar> barvalues)
        {
            Random rand = new Random();
            BrushCollection brushcoll = new BrushCollection();

            foreach (Bar barval in barvalues)
            {
                int num = rand.Next(brushcoll.Count / 3);
                Add(new Record(barval.Value, brushcoll[num], barval.BarName));
            }
        }

    }

    class BrushCollection : ObservableCollection<Brush>
    {
        public BrushCollection()
        {
            Type _brush = typeof(Brushes);
            PropertyInfo[] props = _brush.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                Brush _color = (Brush)prop.GetValue(null, null);
                if (_color != Brushes.LightSteelBlue && _color != Brushes.White &&
                     _color != Brushes.WhiteSmoke && _color != Brushes.LightCyan &&
                     _color != Brushes.LightYellow && _color != Brushes.Linen)
                    Add(_color);
            }
        }
    }

    class Bar
    {

        public string BarName { set; get; }

        public int Value { set; get; }

    }

    class Record : INotifyPropertyChanged
    {
        public Brush Color { set; get; }

        public string Name { set; get; }

        private int _data;
        public int Data
        {
            set
            {
                if (_data != value)
                {
                    _data = value;
                }
            }
            get
            {
                return _data;
            }
        }

        public int Height
        {
            get
            {
                return _data * 2;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Record(int value, Brush color, string name)
        {
            Data = value;
            Color = color;
            Name = name;
        }

        protected void PropertyOnChange(string propname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propname));
            }
        }
    }
}
