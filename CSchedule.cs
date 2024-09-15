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
        // A schedule contains a number of stages
        // By using the get, it can only be read-accessed
        public List<CStage> Stages
        {
            get;
        } = [];

        public int NumberOfStages
        { 
            get { return Stages.Count; } 
        }

        public void Clear()
        {
            Stages.Clear();
        }

        public void CreateSchedule(CStage ShowsToSchedule)
        {
            foreach (CShow show in ShowsToSchedule.Shows)
            {
                // Determine first available time/stage
                // By giving first available time a maximum value,
                // the first to be scheduled shows creates a stage
                int FirstAvailableTime = int.MaxValue;
                CStage FirstAvailableStage = new();
                foreach (CStage stage in Stages)
                {
                    if (stage.EndTime() < FirstAvailableTime)
                    {
                        FirstAvailableTime = stage.EndTime();
                        FirstAvailableStage = stage;
                    }
                }

                // Now add the show to the schedule
                if (show.startTime > FirstAvailableTime)
                { 
                    // Show fits in current schedule after a previous show
                    FirstAvailableStage.Shows.Add(show);
                }
                else
                {
                    // Show doesn't fit, create a new stage
                    CStage stage = new();
                    stage.Shows.Add(show);
                    Stages.Add(stage);
                }
            }
        }

        public void WriteToFile(string fileName)
        // Writes the schedule to a file, very simple format
        {
            using StreamWriter sw = File.CreateText(fileName);
            foreach (CStage cStage in Stages)
            {
                sw.WriteLine(cStage.ToString());
            }
            sw.Close();
        }

        public int StartTime()
        // Returns the start time of all shows in this schedule
        {
            if (Stages.Count == 0)
            {
                return 0;
            }
            else
            {
                int time = Stages[0].StartTime();
                foreach (CStage stage in Stages)
                {
                    time = Math.Min(time, stage.StartTime());
                }
                return time;
            }
        }

        public int EndTime()
        // Returns the end time of all shows in this schedule
        {
            if (Stages.Count == 0)
            {
                return 0;
            }
            else
            {
                int time = Stages[0].EndTime();
                foreach (CStage stage in Stages)
                {
                    time = Math.Max(time, stage.EndTime());
                }
                return time;
            }
        }
    }
}
