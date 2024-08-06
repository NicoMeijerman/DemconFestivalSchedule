using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemconFestivalSchedule
{
    internal class CSchedule
    {
        private readonly List<CStage> CStages = [];

        public void ReadFromFile(string fileName)
        {
            using StreamReader sw = File.OpenText(fileName);
            while (!sw.EndOfStream)
            {
                string? s = sw.ReadLine();
                if (s != null)
                {
                    CStage stage = new();
                    stage.AddShowFromString(s);
                    CStages.Add(stage);
                }
            }
            sw.Close();
        }

        public void WriteToFile(string fileName)
        {
            MergeAllStages();

            using StreamWriter sw = File.CreateText(fileName);
            foreach (CStage cStage in CStages)
            {
                sw.WriteLine(cStage.WriteToString());
            }
            sw.Close();
        }

        public void MergeAllStages()
        {
            // Merges all stages to one. Only used for test purposes

            List<CShow> allShows = [];

            foreach (CStage cStage in CStages)
            {
                List<CShow> shows = cStage.GetShows();
                allShows.AddRange(shows);
            }

            CStages.Clear();
            CStage stage = new ();
            stage.SetShows(allShows);
            CStages.Add(stage);
        }

    }
}
