using Microsoft.Win32;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
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
        internal CStage ShowsToBeScheduled;

        public MainWindow()
        {
            InitializeComponent();

            Schedule = new();
            ShowsToBeScheduled = new();
        }

        private void ReadFile_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                Filter = "(.txt)|*.txt"
            };

            if (fileDialog.ShowDialog() == true)
            {
                // Clear the schedule
                Schedule.Clear();
                ShowsToBeScheduled.ReadFromFile(fileDialog.FileName);
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

        private void CreateAndShowSchedule_Click(object sender, RoutedEventArgs e)
        {
            Schedule.CreateSchedule(ShowsToBeScheduled);

            ScheduleTable.Children.Clear();

            CDrawSchedule drawSchedule = new(Schedule);

            drawSchedule.Draw(ScheduleTable);
        }
    }
}