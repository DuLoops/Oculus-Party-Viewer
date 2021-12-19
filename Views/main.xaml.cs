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
using System.Diagnostics;
using Microsoft.Win32;

namespace Test.Views
{
    /// <summary>
    /// Interaction logic for main.xaml
    /// </summary>
    /// 


    public partial class main : UserControl
    {
        public Party currentParty;
        public main()
        {
            InitializeComponent();
        }
        public main(Party party)
        {
            currentParty = party;
            InitializeComponent(); 
            createParty();
            partyName.Text = party.name;
            System.Diagnostics.Trace.WriteLine(party.name);
        }

        private void createParty()
        {
            mainPlayerPanel.Children.Clear();
            foreach (Player_Character pc in currentParty.Members)
            {
                createPlayerCard(pc);
            }
        }

        private void createPlayerCard(Player_Character pc)
        {
            ScrollViewer ScrollView = new ScrollViewer();
            StackPanel playerCard = new StackPanel();
            playerCard.Name = pc.Name;

            TextBox name = new TextBox();
            name.Text = pc.Name;
            name.Background = Brushes.Transparent;
            name.BorderBrush = Brushes.Transparent;
            name.IsReadOnly = true;

            StackPanel hp = createStackPanel("HP", pc.HitPoints, "HP");
            StackPanel armorClass = createStackPanel("Armor Class", pc.ArmorClass, "ArmorClass");
            StackPanel thb = createStackPanel("To-Hit Bonus", pc.ToHit, "ToHit");
            StackPanel avgDmg = createStackPanel("Average Damage", pc.AverageDamage, "AverageDamage");
            StackPanel avgHeal = createStackPanel("Average Healing", pc.AverageHealing, "AverageHealing");
            StackPanel str_sv = createStackPanel("Strenght Save", pc.Saves[0], "Strenght");
            StackPanel dex_sv = createStackPanel("Dexterity Save", pc.Saves[1], "Dexterity");
            StackPanel cst_sv = createStackPanel("Constitution Save", pc.Saves[2], "Constitution");
            StackPanel int_sv = createStackPanel("Intelligence Save", pc.Saves[3], "Intelligence");
            StackPanel wis_sv = createStackPanel("Wisdom Save", pc.Saves[4], "Wisdom");
            StackPanel chr_sv = createStackPanel("Charisma Save", pc.Saves[5], "Charisma");
            StackPanel avd = createStackPanel("Avoidance", pc.Avoidance, "Avoidance");

            playerCard.Children.Add(name);
            playerCard.Children.Add(hp);
            playerCard.Children.Add(armorClass);
            playerCard.Children.Add(thb);
            playerCard.Children.Add(avgDmg);
            playerCard.Children.Add(avgHeal);
            playerCard.Children.Add(str_sv);
            playerCard.Children.Add(dex_sv);
            playerCard.Children.Add(cst_sv);
            playerCard.Children.Add(int_sv);
            playerCard.Children.Add(wis_sv);
            playerCard.Children.Add(chr_sv);
            playerCard.Children.Add(avd);
            playerCard.Width = 300;
            playerCard.Margin = new Thickness(30, 30, 10, 50);
            playerCard.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#7FA34C50");
            ScrollView.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            ScrollView.Content = playerCard;
            mainPlayerPanel.Children.Add(ScrollView);

        }

        private StackPanel createStackPanel(string content, float value, string statName)
        {
            StackPanel sp = new StackPanel();
            sp.Name = statName;
            RadioButton rb = new RadioButton();
            rb.Content = content;
            TextBox tb = new TextBox();
            tb.Text = value.ToString();
            tb.Background = Brushes.Transparent;
            tb.BorderBrush = Brushes.Transparent;
            tb.TextAlignment = TextAlignment.Center;
            tb.TextChanged += TextBox_TextChanged;
            sp.Children.Add(rb);
            sp.Children.Add(tb);
            sp.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFCEC2AE");
            sp.Margin = new Thickness(15);
            
            return sp;

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (!IsDigitsOnly(tb.Text))
            {
                MessageBox.Show("Please enter a number");
                return;
            }
            StackPanel stat = (StackPanel)VisualTreeHelper.GetParent((DependencyObject)sender);
            StackPanel player = (StackPanel)VisualTreeHelper.GetParent((DependencyObject)stat);
            foreach (Player_Character thisPc in currentParty.Members)
            {
                if (player.Name == thisPc.Name)
                {
                    setPlayerValue(stat.Name, float.Parse(tb.Text), thisPc);
                }
            }
        }

        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        private void setPlayerValue(string statName, float value, Player_Character pc)
        {
            switch(statName)
            {
                case "HP":
                    pc.HitPoints = (int)value;
                    break;
                case "ArmorClass":
                    pc.ArmorClass = (int)value;
                    break;
                case "ToHit":
                    pc.ToHit = (int)value;
                    break;
                case "AverageDamage":
                    pc.AverageDamage = value;
                    break;
                case "AverageHealing":
                    pc.AverageHealing = value;
                    break;
                case "Strenght":
                    pc.Saves[0] = (int)value;
                    break;
                case "Dexterity":
                    pc.Saves[1] = (int)value;
                    break;
                case "Constitution":
                    pc.Saves[2] = (int)value;
                    break;
                case "Intelligence":
                    pc.Saves[3] = (int)value;
                    break;
                case "Wisdom":
                    pc.Saves[4] = (int)value;
                    break;
                case "Charisma":
                    pc.Saves[5] = (int)value;
                    break;
                case "Avoidance":
                    pc.Avoidance = value;
                    break;
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            currentParty.Save();
        }

        private void Parties_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog PartyFinder = new OpenFileDialog()
            {
                Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*"
            };

            // Returns true when a file is opened. Return if not opened.
            if (PartyFinder.ShowDialog() != true) return;

            currentParty = new Party(PartyFinder.FileName);
            createParty();
        }
    }
    
}
