using System;
using System.Linq; 
using System.Windows;
using System.Windows.Controls;
using LiveCharts; 
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Threading.Tasks; 
using System.Reflection;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Runtime.InteropServices;
using LiveCharts.Configurations; 
using System.Threading;
using System.Globalization;
  
using System.IO; 
 
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input; 
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

using System.Drawing;
using Size = System.Windows.Size;

namespace Wpf.CartesianChart.Bubbles
{


    public class ScreenCapture
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetDesktopWindow();

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

        public static System.Drawing.Image CaptureDesktop()
        {
            return CaptureWindow(GetDesktopWindow());
        }

        public static Bitmap CaptureActiveWindow()
        {
            return CaptureWindow(GetForegroundWindow());
        }

        public static Bitmap CaptureWindow(IntPtr handle)
        {
            var rect = new Rect();
            GetWindowRect(handle, ref rect);
            var bounds = new System.Drawing.Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
            var result = new Bitmap(bounds.Width, bounds.Height);

            using (var graphics = Graphics.FromImage(result))
            {
                graphics.CopyFromScreen(new System.Drawing.Point(bounds.Left, bounds.Top), System.Drawing.Point.Empty, bounds.Size);
            }

            return result;
        }
    }

    public partial class GridWindow : Window
    {

        public static double[] X_Y { get; set; }
        public static int count = 0;
        double x;
        double y;
        public GridWindow()
        {
            InitializeComponent(); 
            try
            {


                ValuesA = new ChartValues<ObservablePoint>();
                ValuesB = new ChartValues<ObservablePoint>();
                ValuesE = new ChartValues<ObservablePoint>();
                Value_Kars = new ChartValues<ObservablePoint>();
                Value_Centr = new ChartValues<ObservablePoint>();


                DataContext = this;

                Listbox_dz_x.Items.Clear();
                Listbox_dz_y.Items.Clear();
                Listbox_dz.Items.Clear();
                for (int sw = 0; sw < Kars.MainWindow.List_dz_foarm.Count; sw++)
                {

                    Listbox_dz.Items.Add(Kars.MainWindow.List_dz_foarm[sw]);


                }
                for (int sw = 0; sw < Kars.MainWindow.List_kars_x.Count; sw++)
                {

                    Listbox_dz_x.Items.Add(Kars.MainWindow.List_kars_x[sw]);
                    Listbox_dz_y.Items.Add(Kars.MainWindow.List_kars_y[sw]);
                    ValuesB.Add(new ObservablePoint(Kars.MainWindow.List_kars_x[sw], Kars.MainWindow.List_kars_y[sw]));



                }

                for (int i = 0; i < Kars.Cor.X.Count(); i++)
                {
                    Listishod_x.Items.Add(Kars.Cor.X[i]);
                }

                for (int i = 0; i < Kars.Cor.Y.Count(); i++)
                {
                    Listishod_y.Items.Add(Kars.Cor.Y[i]);
                }

                ////
                ///
                // Библиотеки
                Assembly a = Assembly.Load("dllcsharp");
                Object o = a.CreateInstance("vscode");
                Type t = a.GetType("vscode");


                Object[] zakon = new Object[1];
                MethodInfo mi = t.GetMethod("zakon_1");
                double zakon_1 = Convert.ToDouble(mi.Invoke(o, zakon));
                mi = t.GetMethod("zakon_2");
                double zakon_2 = Convert.ToDouble(mi.Invoke(o, zakon));
                mi = t.GetMethod("zakon_3");
                double zakon_3 = Convert.ToDouble(mi.Invoke(o, zakon));

                ListViewZakon.Items.Add(zakon_1);

                ListViewZakon.Items.Add(zakon_2);

                ListViewZakon.Items.Add(zakon_3);
                int r = 0;
                if (Kars.Cor.X.Length >= Kars.Cor.Y.Length)
                {
                    r = Kars.Cor.X.Length;
                }
                else
                {
                    r = Kars.Cor.Y.Length;
                }

                double Last = 0;
                List_XY_setka.Add(0 + "," + 0);
                for (int q = 0; Kars.Cor.X.Length > q; q++)
                {
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                    string Mas_X = Kars.Cor.X[q];
                    string kolvo;
                    string shag;
                    string shag_pas;

                    kolvo = System.Text.RegularExpressions.Regex.Match(Mas_X, @"\w+").Groups[0].Value;
                    shag_pas = System.Text.RegularExpressions.Regex.Match(Mas_X, @"[,]\w+").Groups[0].Value;
                    shag = System.Text.RegularExpressions.Regex.Match(shag_pas, @"\w+").Groups[0].Value;
                    Kars.Cor.Kolvo_x = Convert.ToInt32(kolvo);
                    Kars.Cor.Shag_x = Convert.ToDouble(shag);
                    for (double S = 1; Kars.Cor.Kolvo_x >= S; S++)
                    {
                        List_XY_setka.Add(Last + Kars.Cor.Shag_x + "," + 0);
                        Last_Cor.Add(Kars.Cor.Shag_x);
                        Last = Last_Cor.Sum();
                    }

                }


                for (int i = 0; List_XY_setka.Count > i; i++)
                {
                    string pre_X = List_XY_setka[i];
                    double X = Convert.ToDouble(System.Text.RegularExpressions.Regex.Match(pre_X, @"\w+").Groups[0].Value);
                    List_X.Add(X);

                }


                Kars.Cor.Check_live = 0;
                Kars.Cor.Count_S = 1;


                for (int q = 0; Kars.Cor.Y.Length > q; q++)
                {

                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");


                    string Mas_Y = Kars.Cor.Y[q];
                    string kolvo;
                    string shag;
                    string shag_pas;

                    kolvo = System.Text.RegularExpressions.Regex.Match(Mas_Y, @"\w+").Groups[0].Value;
                    shag_pas = System.Text.RegularExpressions.Regex.Match(Mas_Y, @"[,]\w+").Groups[0].Value;
                    shag = System.Text.RegularExpressions.Regex.Match(shag_pas, @"\w+").Groups[0].Value;

                    Kars.Cor.Kolvo_y = Convert.ToInt32(kolvo);
                    Kars.Cor.Shag_y = Convert.ToDouble(shag);




                    for (int Z = 1; Kars.Cor.Kolvo_y >= Z; Z++)
                    {


                        for (int W = 0; List_X.Count > W; W++)
                        {
                            List_XY_setka.Add(List_X[W] + "," + Kars.Cor.Count_S * Kars.Cor.Shag_y);

                            Console.WriteLine(List_XY_setka[Kars.Cor.Check_live]);
                            Kars.Cor.Check_live++;
                        }

                        Kars.Cor.Count_S++;
                    }


                }


                for (int i = 0; List_XY_setka.Count > i; i++)
                {
                    double Cor_X = Convert.ToDouble(System.Text.RegularExpressions.Regex.Match(List_XY_setka[i], @"\w+").Groups[0].Value);
                    string Cor_Y1 = System.Text.RegularExpressions.Regex.Match(List_XY_setka[i], @"[,]\w+").Groups[0].Value;
                    double Cor_Y = Convert.ToDouble(System.Text.RegularExpressions.Regex.Match(Cor_Y1, @"\w+").Groups[0].Value);

                    ValuesA.Add(new ObservablePoint(Cor_X, Cor_Y));
                }

                // Value_Kars.Add(new ObservablePoint(1, 1));
            }
            catch
            {
                MessageBox.Show("Ошибка библиотеки.");
            }

        }





        public void ChartsOnClick(object sender, RoutedEventArgs e)
        {

        }
        public void ResOnClick(object sender, RoutedEventArgs e)
        {
            


                List_zakon1_L.Clear();
                Value_Centr.Clear();
                Value_Kars.Clear();


                if (CheckOpora.IsChecked == true)
                {
                    for (int sw = 0; sw < Kars.MainWindow.List_kars_x.Count; sw++)
                    {
                        ValuesE.Add(new ObservablePoint(Kars.MainWindow.List_kars_x[sw], Kars.MainWindow.List_kars_y[sw]));

                    }
                }
                else
                {
                    ValuesE.Clear();
                }



                Assembly a = Assembly.Load("dllcsharp");
                Object o = a.CreateInstance("vscode");
                Type t = a.GetType("vscode");
                Object[] zakon = new Object[1];
                MethodInfo mi = t.GetMethod("zakon_1");
                double zakon_1 = Convert.ToDouble(mi.Invoke(o, zakon));
                mi = t.GetMethod("zakon_2");
                double zakon_2 = Convert.ToDouble(mi.Invoke(o, zakon));
                mi = t.GetMethod("zakon_3");
                double zakon_3 = Convert.ToDouble(mi.Invoke(o, zakon));


                int i = ListViewZakon.SelectedIndex;

                if (i == 0)
                {
                    Kars.Cor.Zakon = zakon_1;
                    Val_Kars.MinPointShapeDiameter = zakon_1 * 40;
                    Val_Kars.MaxPointShapeDiameter = zakon_1 * 40;
                }
                if (i == 1)
                {

                    Val_Kars.MinPointShapeDiameter = zakon_2 * 40;
                    Val_Kars.MaxPointShapeDiameter = zakon_2 * 40;

                    Kars.Cor.Zakon = zakon_2;
                }
                if (i == 2)
                {
                    Val_Kars.MinPointShapeDiameter = zakon_3 * 40;
                    Val_Kars.MaxPointShapeDiameter = zakon_3 * 40;
                    Kars.Cor.Zakon = zakon_3;
                }

                if (Kars.Cor.Zakon > 2)
                {


                    for (int q = 0; List_XY.Count > q; q++)
                    {
                        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                        string wzx = List_XY[q];


                        string X_pas = System.Text.RegularExpressions.Regex.Match(wzx, @"\w+").Groups[0].Value;
                        string Y_pas1 = System.Text.RegularExpressions.Regex.Match(wzx, @"[,]\w+").Groups[0].Value;
                        string Y_pas = System.Text.RegularExpressions.Regex.Match(Y_pas1, @"\w+").Groups[0].Value;
                        double X = Convert.ToDouble(X_pas);
                        double Y = Convert.ToDouble(Y_pas);


                        List_kars_x.Add(X);
                        List_kars_y.Add(Y);


                        //
                        /////
                        /// ДОБАВЛЕНИЕ КРУГОВ ВОКРУГ ОПОРЫ

                        if (CheckOpora.IsChecked == true)
                        {
                            ValuesE.Add(new ObservablePoint(X, Y));

                        }
                        else
                        {
                            ValuesE.Clear();
                        }


                    }
                    int sd = Listbox_dz.Items.Count;


                    for (int dx = 0; dx < sd; dx++)
                    {
                        if (i == 0)
                        {
                            Object[] zakon_1_1 = new Object[1];
                            zakon_1_1[0] = Convert.ToDouble(Listbox_dz.Items[dx]);
                            MethodInfo mi2 = t.GetMethod("zakon_1_1");
                            double radius_1 = Convert.ToDouble(mi2.Invoke(o, zakon_1_1));
                            List_zakon1_L.Add(radius_1);
                            //Передаем значения X

                        }
                        if (i == 1)
                        {
                            Object[] zakon_1_1 = new Object[1];
                            zakon_1_1[0] = Convert.ToDouble(Listbox_dz.Items[dx]);
                            MethodInfo mi2 = t.GetMethod("zakon_2_1");
                            double radius_1 = Convert.ToDouble(mi2.Invoke(o, zakon_1_1));
                            List_zakon1_L.Add(radius_1);
                            //Передаем значения X

                        }
                        if (i == 2)
                        {
                            Object[] zakon_1_1 = new Object[1];
                            zakon_1_1[0] = Convert.ToDouble(Listbox_dz.Items[dx]);
                            MethodInfo mi2 = t.GetMethod("zakon_3_1");
                            double radius_1 = Convert.ToDouble(mi2.Invoke(o, zakon_1_1));
                            List_zakon1_L.Add(radius_1);
                            //Передаем значения X

                        }


                    }



                    for (int xsd = 0; xsd < Listbox_dz_x.Items.Count; xsd++)
                    {
                        List_lib_x.Add(Convert.ToDouble(Listbox_dz_x.Items[xsd]));
                        List_lib_y.Add(Convert.ToDouble(Listbox_dz_y.Items[xsd]));
                        List_lib_dz.Add(Convert.ToDouble(Listbox_dz.Items[xsd]));
                        Console.WriteLine(List_zakon1_L[xsd]);
                    }

                    Object[] centr_x_y = new Object[5];
                    centr_x_y[0] = List_lib_x;
                    centr_x_y[1] = List_lib_y;
                    centr_x_y[2] = List_lib_dz;
                    centr_x_y[3] = List_zakon1_L;
                    centr_x_y[4] = i;

                    MethodInfo mi1 = t.GetMethod("centr_x_y");


                    string patertn = @"[0-9]+\.?[0-9]*|-?[0-9]+\.?[0-9]*";


                    foreach (Match match in Regex.Matches(Convert.ToString(mi1.Invoke(o, centr_x_y)), patertn))
                    {
                        final.Add(double.Parse(match.Value, CultureInfo.InvariantCulture));
                    }
                    Value_Kars.Add(new ObservablePoint(final[0], final[1]));
                    Value_Centr.Add(new ObservablePoint(final[0], final[1]));


                    List_kars_y.Clear();
                    List_kars_x.Clear();
                    List_lib_x.Clear();
                    List_lib_y.Clear();
                    List_lib_dz.Clear();
                    List_zakon1_L.Clear();
                    final.Clear();




                }
                else
                    MessageBox.Show("Нужно выбрать закон!");
           

        }
        public void ChartOnDataClick(object sender, ChartPoint p)
        {
            ///
            ////добавление кординат в листы для dz окна 




            var asPixels = Chart.ConvertToPixels(p.AsPoint());
            Console.WriteLine("[EVENT] You clicked (" + p.X + ", " + p.Y + ") in pixels (" +
                            asPixels.X + ", " + asPixels.Y + ")");
            //Tochka.Fill = Brushes.OrangeRed; 
            ValuesB.Add(new ObservablePoint(p.X, p.Y));
            ///


            Listbox_dz_x.Items.Add(p.X);
            Listbox_dz_y.Items.Add(p.Y);
            Kars.Cor.Dz_x = null;
            Kars.Cor.Dz_y = null;

            Kars.Cor.Dz_x = Convert.ToString(Listbox_dz_x.Items[count]);
            Kars.Cor.Dz_y = Convert.ToString(Listbox_dz_y.Items[count]);

            Kars.Cor.Ckeso = 0;
            count++;
            Kars.Dz_dialog f = new Kars.Dz_dialog();
            f.ShowDialog();
            Listbox_dz.Items.Add(Kars.Cor.Dz);

            if (List_XY.Contains(p.X + "," + p.Y))
            {
                MessageBox.Show("Опора уже добавлена", "Предупреждение");
            }
            else
            {
                List_XY.Add(p.X + "," + p.Y);
                x_dz += Kars.Cor.Dz + "; ";
                list_xy += p.X + "," + p.Y + "; ";
                ListBoxLog.Items.Add("Добавлена опора (X,Y): " + p.X + "," + p.Y);
                ListBoxLog.Items.Add("Добавлена dz " + Kars.Cor.Dz + " для опоры: " + p.X + "," + p.Y);



            }

        }
        public void CleanOnClick(object sender, RoutedEventArgs e)
        {
            Kars.MainWindow.List_dz_foarm.Clear();
            Kars.MainWindow.List_kars_x.Clear();
            Kars.MainWindow.List_kars_y.Clear();
            ValuesB.Clear();
            ValuesE.Clear();
            Value_Centr.Clear();
            Listbox_dz_x.Items.Clear();
            Listbox_dz.Items.Clear();
            Listbox_dz_y.Items.Clear();
            Value_Kars.Clear();
            List_XY.Clear();
            count = 0;
            DataContext = this;

            ListBoxLog.Items.Add("Очистка!");

        }



        public Func<double, string> Formatter { get; set; }
        public ChartValues<ObservableValue> Values { get; set; }
        public CartesianMapper<ObservableValue> Mapper { get; set; }
        public SeriesCollection SeriesCollection { get; set; }

        static public List<double> final = new List<double>();
        static public List<double> List_lib_x = new List<double>();
        static public List<double> List_lib_y = new List<double>();
        static public List<double> List_lib_dz = new List<double>();
        static public List<double> List_kars_x = new List<double>();
        static public List<double> List_dz_foarm = new List<double>();
        static public List<double> List_kars_y = new List<double>();
        static public List<double> dzs = new List<double>();

        static public List<string> List_XY = new List<string>();
        static public List<string> List_X_dz = new List<string>();
        string x_dz = "";
        string list_xy = "";
        public List<double> List_zakon1_L = new List<double>();
        public List<double> List_X = new List<double>();
        public List<double> List_y = new List<double>();

        public List<string> List_XY_setka = new List<string>();

        public List<double> Last_Cor = new List<double>();
        public ChartValues<ObservablePoint> ValuesA { get; set; }
        public ChartValues<ObservablePoint> ValuesB { get; set; }
        public ChartValues<ObservablePoint> ValuesE { get; set; }
        public ChartValues<ObservablePoint> Value_Kars { get; set; }

        public ChartValues<ObservablePoint> Value_Centr { get; set; }
        private void Chart_Loaded(object sender, RoutedEventArgs e)
        {

        }
        public class Experience
        {
            public string Name { get; set; }
            public string X { get; set; }
            public string Y { get; set; }
            public string X_dz { get; set; }
            public string Select { get; set; }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string x = "";
                for (int i = 0; i < Kars.Cor.X.Count(); i++)
                {
                    x += Convert.ToString(Kars.Cor.X[i]) + ";";
                }
                string y = "";
                for (int i = 0; i < Kars.Cor.Y.Count(); i++)
                {
                    y += Convert.ToString(Kars.Cor.Y[i]) + ";";
                }
                string x_dz = "";
                for (int i = 0; i < Listbox_dz.Items.Count; i++)
                {
                    x_dz += Convert.ToString(Listbox_dz.Items[i] + ";");
                }
                string select = "";
                for (int i = 0; i < Listbox_dz_x.Items.Count; i++)
                {
                    select += Convert.ToString(Listbox_dz_x.Items[i] + "," + Listbox_dz_y.Items[i] + ";");
                }

                XmlDocument xDoc = new XmlDocument();
                xDoc.Load("data.xml");
                XmlElement xRoot = xDoc.DocumentElement;
                // создаем новый элемент user
                XmlElement userElem = xDoc.CreateElement("Experience");
                // создаем атрибут name
                XmlAttribute nameAttr = xDoc.CreateAttribute("name");
                // создаем элементы company и age
                XmlElement X = xDoc.CreateElement("X");
                XmlElement Y = xDoc.CreateElement("Y");
                XmlElement X_dz = xDoc.CreateElement("X_dz");
                XmlElement Select = xDoc.CreateElement("Select");
                // создаем текстовые значения для элементов и атрибута
                XmlText nameText = xDoc.CreateTextNode("Опыт " + DateTime.Now);
                XmlText X_text = xDoc.CreateTextNode(x);
                XmlText Y_text = xDoc.CreateTextNode(y);
                XmlText X_dz_text = xDoc.CreateTextNode(x_dz);
                XmlText Select_text = xDoc.CreateTextNode(select);

                nameAttr.AppendChild(nameText);
                X.AppendChild(X_text);
                Y.AppendChild(Y_text);
                X_dz.AppendChild(X_dz_text);
                Select.AppendChild(Select_text);

                userElem.Attributes.Append(nameAttr);
                userElem.AppendChild(X);
                userElem.AppendChild(Y);
                userElem.AppendChild(X_dz);
                userElem.AppendChild(Select);
                xRoot.AppendChild(userElem);
                xDoc.Save("data.xml");
            }
            catch
            {

            }
            // var xml = CreateXML(new Experience { Name = "Опыт "+ DateTime.Now, X = x, Y = y, X_dz = x_dz, Select = list_xy });
            //xml.Save("data.xml");
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Listbox_dz_x.Items.Clear();
            Listbox_dz_y.Items.Clear();
            Listbox_dz.Items.Clear();
        }

    private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            String filename = "Отчет-" + DateTime.Now.ToString("ddMMyyyy-hhmmss") + ".Jpeg";
            var image = ScreenCapture.CaptureActiveWindow();
            image.Save("Отчеты\\" + filename, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
}
}
