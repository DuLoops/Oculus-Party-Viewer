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


namespace Test.Views
{
    /// <summary>
    /// Interaction logic for main.xaml
    /// </summary>
    /// 


    public partial class main : UserControl
    {


        Party currentParty;

        

        public main()
        {
            InitializeComponent();
        }
        public main(Party party)
        {
            currentParty = party;
            InitializeComponent();
            createParty();

        }

        private void createParty()
        {
            foreach (Player_Character pc in currentParty.Members)
            {
                createPlayerCard(pc);
            }
        }

        private void createPlayerCard(Player_Character pc)
        {
            System.Windows.Controls.StackPanel playerCard = new StackPanel();

            System.Windows.Controls.TextBox name = new TextBox();
            name.Text = pc.Name;
            name.Background = Brushes.Transparent;
            name.BorderBrush = Brushes.Transparent;
            name.IsReadOnly = true;

            System.Windows.Controls.TextBox hp = new TextBox();
            string hpText = "HP: " + pc.HitPoints;
            hp.Text = hpText;
            hp.Background = Brushes.Transparent;
            hp.BorderBrush = Brushes.Transparent;

            System.Windows.Controls.ProgressBar pBar = new ProgressBar();
            pBar.Value = pc.HitPoints;
            pBar.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFE8443C");
            pBar.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFD1D1D1");
            pBar.Height = 30;


            System.Windows.Controls.StackPanel armorClass = createStackPanel("Armor Class", pc.ArmorClass);
            System.Windows.Controls.StackPanel thb = createStackPanel("To-Hit Bonus", pc.ToHit);
            System.Windows.Controls.StackPanel avgDmg = createStackPanel("Average Damage", pc.AverageDamage);
            System.Windows.Controls.StackPanel avgHeal = createStackPanel("Average Healing", pc.AverageHealing);
            System.Windows.Controls.StackPanel str_sv = createStackPanel("Strenght Save", pc.Saves[0]);
            System.Windows.Controls.StackPanel dex_sv = createStackPanel("Dexterity Save", pc.Saves[1]);
            System.Windows.Controls.StackPanel cst_sv = createStackPanel("Constitution Save", pc.Saves[2]);
            System.Windows.Controls.StackPanel int_sv = createStackPanel("Intelligence Save", pc.Saves[3]);
            System.Windows.Controls.StackPanel wis_sv = createStackPanel("Wisdom Save", pc.Saves[4]);
            System.Windows.Controls.StackPanel chr_sv = createStackPanel("Charisma Save", pc.Saves[5]);
            System.Windows.Controls.StackPanel avd = createStackPanel("Avoidance", pc.Avoidance);

            playerCard.Children.Add(name);
            playerCard.Children.Add(hp);
            playerCard.Children.Add(pBar);
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
            playerCard.Margin = new Thickness(20, 30, 20, 50);
            playerCard.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#7FA34C50");
            this.mainPlayerPanel.Children.Add(playerCard);
            Trace.WriteLine("player created");

        }

        private System.Windows.Controls.StackPanel createStackPanel(string content, float value)
        {
            System.Windows.Controls.StackPanel sp = new StackPanel();
            System.Windows.Controls.RadioButton rb = new RadioButton();
            rb.Content = content;
            System.Windows.Controls.TextBox tb = new TextBox();
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
            Trace.WriteLine(sender.GetType());
        }
    }
    
}
