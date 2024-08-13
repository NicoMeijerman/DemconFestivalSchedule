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
        internal CSchedule OriginalSchedule;
        internal CSchedule BestSchedule;

        public MainWindow()
        {
            InitializeComponent();

            OriginalSchedule = new();
            BestSchedule = OriginalSchedule;
        }

        private void ReadFile_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                Filter = "(.txt)|*.txt"
            };

            if (fileDialog.ShowDialog() == true)
            {
                OriginalSchedule.ReadFromFile(fileDialog.FileName);
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
                BestSchedule.WriteToFile(fileDialog.FileName);
            }
        }

        private void CreateSchedule_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch stopWatch = new();

            int NumberOfIterations = int.Parse(TextBoxNumberOfIterations.Text);

            stopWatch.Start();
            BestSchedule = OriginalSchedule.Revamp(NumberOfIterations);
            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;

            TextBoxProcessingTime.Text = ts.TotalMilliseconds.ToString("F0");
        }

        private void ShowSchedule_Click(object sender, RoutedEventArgs e)
        {
            ScheduleTable.Children.Clear();

            CDrawSchedule drawSchedule = new(BestSchedule);

            drawSchedule.Draw(ScheduleTable);
        }
    }
}