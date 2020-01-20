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
using System.Windows.Shapes; 
using System.ComponentModel;
using System.Data;
using System.Drawing; 

namespace Kars
{
    /// <summary>
    /// Логика взаимодействия для Dz_dialog.xaml
    /// </summary>
    public partial class Dz_dialog : Window
    {
        double s = 2;
        public Dz_dialog()
        {
            
            InitializeComponent();
            textbox_dz.Clear();
            Labeldz.Content = "Ввод dz для опоры " + Cor.Dz_x+ ", " + Cor.Dz_y  ;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Cor.Ckeso = 1;

           Cor.Dz= textbox_dz.Text ;
            this.Close();  
        }

        private void Labeldz_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void Labeldz_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
           
        }

        private void Textbox_dz_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789 .".IndexOf(e.Text) < 0;
        }
    }
}
