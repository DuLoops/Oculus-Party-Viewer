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
using Test.ViewModels;

namespace Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //testing
        }

        private void new_party_clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new new_partyViewModel();
        }


        private void main_clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new mainViewModel();

        }
    }
}
