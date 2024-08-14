using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DemconFestivalSchedule
{
    internal class CDrawSchedule
    {
        // Several constants to create proper layout
        const int borderWidth = 1;
        const int firstRowHeight = 30;
        const int rowHeight = 30;
        const int firstColumnWidth = 80;
        const int columnWidth = 20;

        readonly CSchedule schedule;
        readonly int numberOfStages;
        readonly int startTime;
        readonly int endTime;

        public CDrawSchedule(CSchedule schedule)
        {
            this.schedule = schedule;
            numberOfStages = schedule.NumberOfStages;
            startTime = schedule.StartTime();
            endTime = schedule.EndTime();
        }

        private void DrawFirstColumn(Canvas canvas)
        // Create first column with all stages
        {
            for (int i = 0; i < numberOfStages; i++)
            {
                TextBlock textBlock = new()
                {
                    Text = "Stage " + (i + 1).ToString(),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                };

                Border border = new()
                {
                    Child = textBlock,
                    Height = rowHeight,
                    Width = firstColumnWidth,
                };

                Canvas.SetTop(border, firstRowHeight + i * rowHeight);
                Canvas.SetLeft(border, 0);
                canvas.Children.Add(border);
            }
        }

        private void DrawFirstRow(Canvas canvas)
        // Create first row with all times
        {
            for (int i = startTime; i <= endTime; i++)
            {
                TextBlock textBlock = new()
                {
                    Text = i.ToString(),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    LayoutTransform = new RotateTransform(-90),
                };

                Border border = new()
                {
                    Child = textBlock,
                    Height = firstRowHeight,
                    Width = columnWidth,
                };

                Canvas.SetTop(border, 0);
                Canvas.SetLeft(border, firstColumnWidth + (i - startTime) * columnWidth);
                canvas.Children.Add(border);
            }
        }

        private void DrawShows(Canvas canvas)
        // Draw all shows at their position
        {
            for (int i = 0; i < schedule.NumberOfStages; i++)
            {
                List<CShow> shows = schedule.Stages[i].Shows;

                for (int j = 0; j < shows.Count; j++)
                {
                    TextBlock textBlock = new()
                    {
                        Text = shows[j].name,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                    };

                    Border border = new()
                    {
                        Child = textBlock,
                        Height = rowHeight,
                        Width = (shows[j].endTime - shows[j].startTime + 1) * columnWidth,
                        BorderThickness = new Thickness(borderWidth),
                        BorderBrush = Brushes.Black,
                    };

                    Canvas.SetTop(border, firstRowHeight + i * rowHeight);
                    Canvas.SetLeft(border, firstColumnWidth + (shows[j].startTime - startTime) * columnWidth);
                    canvas.Children.Add(border);
                }
            }
        }

        private static Size FindCanvasSizeForAllObjects(Canvas canvas)
        // Returns a size that encloses all objects on the canvas.
        {
            double bottom = 0;
            double right = 0;

            foreach (FrameworkElement element in canvas.Children)
            {
                double posX = Canvas.GetLeft(element);
                right = Math.Max(posX + element.Width, right);
                double posY = Canvas.GetTop(element);
                bottom = Math.Max(posY + element.Height, bottom);
            }

            return new(right, bottom);
        }

        public void Draw(Canvas canvas)
        // Draws the schedule on the canvas.
        {
            DrawFirstColumn(canvas);
            DrawFirstRow(canvas);
            DrawShows(canvas);

            // Resize canvas
            Size size = FindCanvasSizeForAllObjects(canvas);
            canvas.Width = size.Width;
            canvas.Height = size.Height;
        }
    }
}
