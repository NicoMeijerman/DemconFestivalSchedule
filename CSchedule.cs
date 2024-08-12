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
        public List<CStage> Stages
        {
            get;
        } = [];

        public int NumberOfStages
        { 
            get { return Stages.Count; } 
        }

        public CSchedule()
        // Default constructor, creating empty schedule
        {
        }

        public CSchedule(CSchedule schedule)
        // Copy constructor, copies schedule
        {
            for (int i = 0; i < schedule.NumberOfStages; i++)
            {
                Stages.Add(new());
                Stages[i].Shows.AddRange(schedule.Stages[i].Shows);
            }
        }

        public void ReadFromFile(string fileName)
        {
            using StreamReader sw = File.OpenText(fileName);
            while (!sw.EndOfStream)
            {
                string? s = sw.ReadLine();
                if (s != null)
                {
                    CStage stage = new(s);
                    Stages.Add(stage);
                }
            }
            sw.Close();
        }

        public void WriteToFile(string fileName)
        {
            using StreamWriter sw = File.CreateText(fileName);
            foreach (CStage cStage in Stages)
            {
                sw.WriteLine(cStage.ToString());
            }
            sw.Close();
        }

        private void MergeStages()
        // Merges stages whenever possible
        {
            // Start with the last stage
            int stageToMergeWithOthers = Stages.Count - 1;

            while (stageToMergeWithOthers > 1)
            {
                int i = 0;
                // Try all stages up to the one we want to merge with others
                while (i < stageToMergeWithOthers)
                {
                    if (Stages[i].ConflictingShows(Stages[stageToMergeWithOthers]))
                    {
                        // CStage[i] can not be merged, try next
                        i++;
                    }
                    else
                    {
                        // Merge stages and remove the one that is merged
                        Stages[i].Merge(Stages[stageToMergeWithOthers]);
                        Stages.RemoveAt(stageToMergeWithOthers);
                        stageToMergeWithOthers--;
                    }
                }

                // And now try the previous one
                stageToMergeWithOthers--;
            }
        }

        private void ShuffleStages()
        // Puts the stages in the schedule in a random order
        // Also see https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle#The_modern_algorithm
        {
            Random random = new();

            for (int i = 0; i < Stages.Count-1; i++)
            {
                int j = i + random.Next(Stages.Count-i);

                (Stages[i], Stages[j]) = (Stages[j], Stages[i]); // Swap items
            }
        }

        private bool IsBetterThan(CSchedule otherOne)
        // Determines whether the current schedule is better than the otherOne
        {
            return (this.Stages.Count < otherOne.Stages.Count);
        }

        public void Improve(int numberOfInterations)
        // Improves the schedule by repeatly shuffling and merges stages
        // and keeping the best one
        {
            CSchedule BestSchedule = new(this);

            for (int i = 0; i < numberOfInterations; i++)
            {
                // Create new schedule, is copy from this
                // Shuffle + merge it to see whether it is better
                CSchedule NewSchedule = new(this);

                NewSchedule.ShuffleStages();

                NewSchedule.MergeStages();

                if (NewSchedule.IsBetterThan(BestSchedule))
                {
                    BestSchedule = NewSchedule;
                }
            }

            // BestSchedule contains improved schedule, copy it to this
            Stages.Clear();
            Stages.AddRange(BestSchedule.Stages); 
        }

        public int StartTime()
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
