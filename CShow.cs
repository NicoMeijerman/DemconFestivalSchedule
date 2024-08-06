using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemconFestivalSchedule
{
    internal class CShow
    {
        private string name = "";
        private int startTime = -1;
        private int endTime = -1;

        public void ReadFromString(string s)
        {
            // Converts s to CShow, format expected "name starttime endtime", all separated by spaces
            // No checking done

            string[] subs = s.Split(" ");
            this.name = subs[0];
            this.startTime = int.Parse(subs[1]);
            this.endTime = int.Parse(subs[2]);
        }

        public string WriteToString()
        {
            return name + " " + startTime + " " + endTime;
        }

        public static int SortByStartTimeAscending(CShow x, CShow y)
        {
            return (x.startTime - y.startTime);
        }

        public static bool OverlappingShows(CShow x, CShow y)
        {
            return (x.endTime >= y.startTime && x.startTime <= y.endTime);
        }
    }
}
