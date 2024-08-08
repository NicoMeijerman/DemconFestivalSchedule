using Microsoft.Win32;
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

namespace DemconFestivalSchedule
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal CSchedule Schedule;

        public MainWindow()
        {
            InitializeComponent();

            Schedule = new();
        }

        private void ReadFile_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                Filter = "(.txt)|*.txt"
            };

            if (fileDialog.ShowDialog() == true)
            {
                Schedule.ReadFromFile(fileDialog.FileName);
                ReadFile.IsEnabled = false;
            }
        }

        private void WriteFile_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new SaveFileDialog
            {
                Filter = "(.txt)|*.txt"
            };

            if (fileDialog.ShowDialog() == true)
            {
                Schedule.WriteToFile(fileDialog.FileName);
            }
        }

        private void CreateSchedule_Click(object sender, RoutedEventArgs e)
        {
            Schedule.Improve(10000);
        }
    }
}