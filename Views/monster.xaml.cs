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

namespace Test.Views
{
    /// <summary>
    /// Interaction logic for monster.xaml
    /// </summary>
    public partial class monster : UserControl
    {
        public monster()
        {
            InitializeComponent();
        }
        public monster(Party Adventurers)
        {
            InitializeComponent();
            Creature Boss = new Creature(Adventurers);
            DisplayBoss();
        }

        private void DisplayBoss()
        {

        }
    }
}
