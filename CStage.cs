using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DemconFestivalSchedule
{
    internal class CStage
    {
        // A stage contains a number of shows
        // By using the get, it can only be read-accessed
        public List<CShow> Shows
        {
            get;
        } = [];

        public override string ToString()
        // Conversion method to string
        {
            string s = "";

            foreach (CShow show in Shows) 
            { 
                s += show.ToString() + " ++ "; 
            }
            return s;
        }

        public void ReadFromFile(string fileName)
        // Reads an initial schedule from file
        // Every stage contains only one show
        {
            Shows.Clear();
            using StreamReader sw = File.OpenText(fileName);
            while (!sw.EndOfStream)
            {
                string? s = sw.ReadLine();
                if (s != null)
                {
                    CShow show = new(s); 
                    Shows.Add(show);
                }
            }
            Shows.Sort(CShow.SortByStartTimeAscending);
            sw.Close();
        }

        public int StartTime()
        // Returns the start time of the shows on this stage
        {
            return (Shows.Count == 0) ? 0 : Shows[0].startTime;
        }

        public int EndTime()
        // Returns the end time of the shows on this stage
        {
            return (Shows.Count == 0) ? 0 : Shows[^1].endTime;
        }
    }
}
