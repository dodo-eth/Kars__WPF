using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Xml;

using System.Xml.Linq;
namespace Kars
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        
        Canvas canvasObj = new Canvas();
        public MainWindow()
        {
            InitializeComponent();
            Textbox1.PreviewTextInput += new TextCompositionEventHandler(Textbox1_TextInput);
          

        }
          
        public void Btn_charts_click(object sender, RoutedEventArgs e)
        {
            Cor.X = new string[ListBox_X.Items.Count];
            for (int i = 0; i < ListBox_X.Items.Count; i++)
            {
                Cor.X[i] = ListBox_X.Items[i].ToString();
            }
            Cor.Y = new string[ListBox_Y.Items.Count];
            for (int i = 0; i < ListBox_Y.Items.Count; i++)
            {
                Cor.Y[i] = ListBox_Y.Items[i].ToString();
            }

            // Cor.Mastab_XY = Convert.ToDouble(Textbox1_mastab.Text);

           

            Wpf.CartesianChart.Bubbles.GridWindow f = new Wpf.CartesianChart.Bubbles.GridWindow();            
            f.Show();
        }




            private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            /*
            ThicknessAnimation anim = new ThicknessAnimation();
            anim.From = new Thickness(10, 20, 20, 30);
            anim.To = new Thickness(256, 20, 20, 30);
            anim.Duration = new Duration(TimeSpan.FromSeconds(1));
           Radio_X.BeginAnimation(RadioButton.MarginProperty, anim);
           */
            if (Radio_X.IsChecked == true)
            {
                string k=String.Empty, s = String.Empty;
                if (Textbox1.Text!="")
                {
                    k= Textbox1.Text;
                }
                else
                {

                    MessageBox.Show("Введите Количество!");
                }
                if (Textbox2.Text != "")
                {
                    s = Textbox2.Text;
                    
                }
                else
                {
                    MessageBox.Show("Введите шаг!");
                }
                if(s!=""& k!="")
                ListBox_X.Items.Add(k + ","+s);
            }
            if (Radio_Y.IsChecked == true)
            {
                string k = String.Empty, s = String.Empty;
                if (Textbox1.Text != "")
                {
                    k = Textbox1.Text;
                }
                else
                {

                    MessageBox.Show("Введите Количество!");
                }
                if (Textbox2.Text != "")
                {
                    s = Textbox2.Text;

                }
                else
                {
                    MessageBox.Show("Введите шаг!");
                }
                if (s != "" & k != "")
                    ListBox_Y.Items.Add(k + "," + s);
            }
        }

         

       
        private void Textbox1_GotMouseCapture(object sender, MouseEventArgs e)
        {
            if (Textbox1.Text == "Кол-во,шаг")
            {
                Textbox1.Text = "";
            }
          
        }

      

        private void Btn_open_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog openFileDialog2 = new Microsoft.Win32.OpenFileDialog();
                openFileDialog2.DefaultExt = ".txt";
                openFileDialog2.Filter = "Text documents (.txt)|*.txt";
                openFileDialog2.ShowDialog();

                string Open = openFileDialog2.FileName;
                using (System.IO.StreamReader read = new System.IO.StreamReader(Open))
                {
                    foreach (string str in read.ReadToEnd().Split(new Char[] { ' ', '\t' }))
                    {
                        ListBox_X.Items.Add(str);
                        ListBox_Y.Items.Add(str);
                    }
                }
            }
            catch
            {

            }



        }

      

        private void Textbox1_TextInput(object sender, TextCompositionEventArgs e)
        {
            string inputSymbol = e.Text.ToString();

            if (!Regex.Match(inputSymbol, @"[0-9]|\.").Success)
            {
                e.Handled = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListBoxOpity.Items.Clear();
            XmlDocument doc = new XmlDocument();
            doc.Load("data.xml");
            foreach (XmlNode node in doc.DocumentElement)
            {
                string name = node.Attributes[0].Value;
                string X = doc.GetElementsByTagName("X")[0].InnerText;
                string Y = doc.GetElementsByTagName("Y")[0].InnerText;
                string x_dz = doc.GetElementsByTagName("X_dz")[0].InnerText;
                string select = doc.GetElementsByTagName("Select")[0].InnerText;

                ListBoxOpity.Items.Add(new Employee(name, X, Y, x_dz, select));
            }
           
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string patertn2 = @"\w,\w";
            string patertn3 = @"[0-9]+\.?[0-9]*|-?[0-9]+\.?[0-9]*";
            string patertn1 = @"\w+";
            XmlDocument doc = new XmlDocument();
            doc.Load("data.xml");
            int i = ListBoxOpity.SelectedIndex;
            if (ListBoxOpity.SelectedIndex != -1)
            {
                ListBox_X.Items.Clear();
                ListBox_Y.Items.Clear();
                ListBox_dz.Items.Clear();
                ListBox_opori.Items.Clear();
                List_kars_x.Clear();
                List_kars_y.Clear();
                List_dz_foarm.Clear();


                foreach (Match match in Regex.Matches(doc.GetElementsByTagName("X")[i].InnerText, patertn2))
                {
                    ListBox_X.Items.Add(match);
                   
                }
                foreach (Match match in Regex.Matches(doc.GetElementsByTagName("Y")[i].InnerText, patertn2))
                {
                    ListBox_Y.Items.Add(match);
                }
                    
                foreach (Match match in Regex.Matches(doc.GetElementsByTagName("X_dz")[i].InnerText, patertn3))
                {
                    ListBox_dz.Items.Add(match);
                    List_dz_foarm.Add(double.Parse(match.Value, CultureInfo.InvariantCulture));
                }
                foreach (Match match in Regex.Matches(doc.GetElementsByTagName("Select")[i].InnerText, patertn2))
                {
                    ListBox_opori.Items.Add(match);
                    
                   
                }
                int s = 0;
                foreach (Match match2 in Regex.Matches(doc.GetElementsByTagName("Select")[i].InnerText, patertn1))
                {
                    s++;
                    if (s % 2 == 0)
                    {
                       List_kars_y.Add(double.Parse(match2.Value, CultureInfo.InvariantCulture));
                    }
                    else
                    {

                        List_kars_x.Add(double.Parse(match2.Value, CultureInfo.InvariantCulture)); 
                    }

                }
            }
        }

        public void Button_Click_2(object sender, RoutedEventArgs e)
        {
          
        }
        static public List<double> List_kars_x = new List<double>();
        static public List<double> List_dz_foarm = new List<double>();
        static public List<double> List_kars_y = new List<double>();

        public Wpf.CartesianChart.Bubbles.GridWindow GridWindow
        {
            get => default(Wpf.CartesianChart.Bubbles.GridWindow);
            set
            {
            }
        }
    }
}
