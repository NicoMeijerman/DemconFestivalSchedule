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
            Stopwatch stopWatch = new();

            CreateSchedule.IsEnabled = false;

            int NumberOfIterations = int.Parse(TextBoxNumberOfIterations.Text);

            stopWatch.Start();
            Schedule.Improve(NumberOfIterations);
            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;

            TextBoxProcessingTime.Text = ts.TotalMilliseconds.ToString("F0");
        }

        private void InitializeScheduleTable(int numberOfColumns, int numberOfRows)
        {
            const int borderWidth = 1;

            UniformGrid grid = new()
            {
                Rows = numberOfRows,
                Columns = numberOfColumns
            };

            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = 0; j < numberOfColumns; j++)
                {
                    Border border = new()
                    {
                        Height = 30,
                        BorderThickness = new Thickness(borderWidth, borderWidth, borderWidth, borderWidth),
                        BorderBrush = Brushes.Black,
                        //Background = background,
                    };

                    TextBlock textBlock = new()
                    {
                        Text = i.ToString() + "+" + j.ToString(),
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        FontSize = 8,
                    };

                    border.Child = textBlock;

                    grid.Children.Add(border);
                }
            }

            ScheduleTable.Child = grid;
        }

        private void ShowSchedule_Click(object sender, RoutedEventArgs e)
        {

            int st = Schedule.StartTime();
            int et = Schedule.EndTime();

            InitializeScheduleTable(1+et-st,1+Schedule.NumberOfStages);

        }
    }
}