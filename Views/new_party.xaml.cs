using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Test.Views;
using Test.ViewModels;

namespace Test.Views
{
    /// <summary>
    /// Interaction logic for new_party.xaml
    /// </summary>
    public partial class new_party : UserControl
    {
        public new_party()
        {
            InitializeComponent();
        }

        private void start_btn_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new main();

        }

        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new MainWindow();
            System.Diagnostics.Trace.WriteLine("sfhdl");
        }
    }
}
