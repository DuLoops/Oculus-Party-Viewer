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
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.IO;
using NAudio.Wave;
using System.ComponentModel;

namespace Test
{
    /// <summary>
    /// Interaction logic for monster.xaml
    /// </summary>
    public partial class monster : Window
    {
        public monster()
        {
            InitializeComponent();

        }

        Mp3FileReader reader = new Mp3FileReader("../../../music/battleThemeA.mp3");
        WaveOut waveOut = new WaveOut(); // or WaveOutEvent()

        public monster(Party Adventurers, bool mute)
        {
            InitializeComponent();
            Creature Boss = new Creature(Adventurers);
            DisplayBoss(Boss);
            if (!mute) {
                waveOut.Init(reader);
                waveOut.Play();
            }
        }

        private void DisplayBoss(Creature Boss)
        {
            string bossInfo = "";
            bossInfo += "HP: " + Boss.HitPoints
                + "\nArmor Class: " + Boss.ArmorClass
                + "\nAverage Damage: " + Boss.AverageDamage
                + "\nFlat Damage: " + Boss.FlatDamage
                + "\nAverage Healing: " + Boss.AverageHealing
                + "\nStrenght: " + Boss.Saves[0].ToString()
                + "\nDexterity: " + Boss.Saves[1]
                + "\nConstitution: " + Boss.Saves[2]
                + "\nIntelligence: " + Boss.Saves[3]
                + "\nWisdom: " + Boss.Saves[4]
                + "\nCharisma: " + Boss.Saves[5]
                + "\nPrimary Save: " + Boss.PrimarySave.ToString();
            if (Boss.UsesSaves)
            {
                bossInfo += "\nUses saves";
            } else
            {
                bossInfo += "\nDo not use saves";
            }
            if(Boss.HalfDamage)
            {
                bossInfo += "\nHalf damage enabled";
            } else
            {
                bossInfo += "\nHalf damage disabled";
            }
            bossText.Text = bossInfo;
            
        }

        void DataWindow_Closing(object sender, CancelEventArgs e)
        {   
            waveOut.Stop();

        }
    }       
}           
            
            