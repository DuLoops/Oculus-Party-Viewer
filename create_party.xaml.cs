using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Test
{
    /// <summary>
    /// Interaction logic for create_party.xaml
    /// </summary>
    public partial class create_party : Window
    {

        List<string> names;

        public create_party()
        {
            InitializeComponent();
            names = new List<string>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            names.Add(playerName.Text);
            playerName.Text = "";
            nameList.ItemsSource = new List<Object>();
            nameList.ItemsSource = names;
        }

        private void start_btn_Click(object sender, RoutedEventArgs e)
        {
            Party party = new Party();
            party.name = partyName.Text;
            foreach (string name in names)
            {
                party.Members.Add(new Player_Character(name));
            }
            party.Save();
            this.Close();
        }
    }
}
