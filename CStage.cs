using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DemconFestivalSchedule
{
    internal class CStage
    {
        // A stage contains a number of shows
        private readonly List<CShow> Shows = [];

        public CStage(string s)
        // Constructs CStage from s with only one Show, format expected name starttime endtime, all separated by spaces
        // No checking done so it can generate a runtime exception
        {
            CShow cShow = new(s);

            Shows.Add(cShow);
        }

        public override string ToString()
        {
            string s = "";

            foreach (CShow show in Shows) 
            { 
                s += show.ToString() + " <-> "; 
            }
            return s;
        }

        public bool ConflictingShows(CStage otherStage)
        // Checks whether the shows on otherStage conflict with the shows on this stage
        {
            // Create list of combined shows
            List<CShow> combinedShows = [];
            combinedShows.AddRange(this.Shows);
            combinedShows.AddRange(otherStage.Shows);

            // Order the shows on the stage in the correct order, using the start time
            combinedShows.Sort(CShow.SortByStartTimeAscending);

            // Check for overlap
            for (int i = 0; i < combinedShows.Count-1; i++)
            {
                if (CShow.OverlappingShows(combinedShows[i],combinedShows[i+1]))
                    return true;
            }

            return false;
        }

        public void Merge(CStage otherStage)
        // Merges otherStage with the current one
        // Only allowed if there are no overlapping shows (checked with ConflictingShows)
        {
            Shows.AddRange(otherStage.Shows);

            // Order the shows on the stage in the correct order, using the start time
            Shows.Sort(CShow.SortByStartTimeAscending);
        }

    }
}
