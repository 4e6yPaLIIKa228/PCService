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
using PCService.Pages;

namespace PCService.Pages
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();

        }

        private void btnremont_Click(object sender, RoutedEventArgs e)
        {
            ServicePages Aftoriz = new ServicePages();
            this.Close();
            Aftoriz.ShowDialog();
            

        }
    }
}
