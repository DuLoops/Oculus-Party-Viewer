using Microsoft.Win32;
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
using Test.Views;
using System.Runtime.InteropServices;
using System.IO;
using NAudio.Wave;


namespace Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("nAudio.dll", CharSet = CharSet.Auto)]
        public static extern int start();



        private Party CurrentParty;

        Mp3FileReader reader = new Mp3FileReader("../../../music/A_Drink_Deserved_-_Hayden_McGowan.mp3");
        WaveOut waveOut = new WaveOut(); // or WaveOutEvent()

        public MainWindow()
        {
            InitializeComponent();
            queueMusic();
            //testing
        }
        private void queueMusic()
        {

            waveOut.Init(reader);
            waveOut.Play();
        }
        private void new_party_clicked(object sender, RoutedEventArgs e)
        {

            //DataContext = new ViewModels.new_partyViewModel();

            create_party createWindow = new create_party();
            createWindow.Show();
        }


        private void main_clicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog PartyFile = new OpenFileDialog
            {
                Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*"
            };

            // Returns true when a file is opened. Return if not opened.
            if (PartyFile.ShowDialog() != true) return;

            CurrentParty = new Party(PartyFile.FileName);
            DataContext = new main(CurrentParty, waveOut);

        }
    }
}
