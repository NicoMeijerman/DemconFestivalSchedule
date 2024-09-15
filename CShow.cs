using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemconFestivalSchedule
{
    internal class CShow
    {
        // Members are public readonly so they can be retrieved directly but not modified
        public readonly string name;
        public readonly int startTime;
        public readonly int endTime;

        public CShow(string s)
        // Constructs CShow using s, format expected "name starttime endtime", all separated by spaces
        // No checking done so it can generate a runtime exception
        {
            string[] subs = s.Split(" ");
            this.name = subs[0];
            this.startTime = int.Parse(subs[1]);
            this.endTime = int.Parse(subs[2]);
        }

        public override string ToString()
        // Converts CShow to string, format "name starttime endtime"
        {
            return name + " " + startTime + " " + endTime;
        }

        public static int SortByStartTimeAscending(CShow x, CShow y)
        // Function needed for sorting lists
        {
            return (x.startTime - y.startTime);
        }
    }
}
