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
        private List<CShow> CShows = [];

        public void AddShowFromString(string s)
        {
            // Converts s to CShow, format expected name starttime endtime, all separated by spaces
            // No checking done
            CShow cShow = new();

            cShow.ReadFromString(s);
            CShows.Add(cShow);
        }

        public string WriteToString()
        {
            string s = "";

            foreach (CShow cShow in CShows) 
            { 
                s += cShow.WriteToString() + " <-> "; 
            }
            return s;
        }

        public List<CShow> GetShows()
        {
            return CShows;
        }

        public void SetShows(List<CShow> shows)
        {
            CShows = shows;
        }

        public bool ConflictingShows(CStage stage)
        {
            // Checks whether the shows on stage conflict with the current shows

            List<CShow> combinedShows = [];
            combinedShows.AddRange(this.GetShows());
            combinedShows.AddRange(stage.GetShows());

            SortShows();

            for (int i = 0; i < combinedShows.Count-1; i++)
            {
                if (CShow.OverlappingShows(combinedShows[i],combinedShows[i+1]))
                    return true;
            }

            return false;
        }


        private void SortShows()
        {
            // Orders the shows on the stage in the correct order, using the start time
            CShows.Sort(CShow.SortByStartTimeAscending);
        }

    }
}
