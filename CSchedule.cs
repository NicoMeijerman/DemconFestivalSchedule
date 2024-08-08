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
        private readonly List<CStage> Stages = [];

        public CSchedule()
        // Default constructor, creating empty schedule
        {
        }

        public CSchedule(CSchedule schedule)
        // Copy constructor
        {
            this.Stages.AddRange(schedule.Stages);
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

        public void MergeStages()
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

        public void ShuffleStages()
        // Puts the stages in the schedule in a random order
        // Also see https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle#The_modern_algorithm
        // -- To shuffle an array a of n elements(indices 0..n-1):
        // for i from 0 to n−2 do
        // j ← random integer such that i ≤ j <n
        // exchange a[i] and a[j]
        {
            Random random = new();

            for (int i = 0; i < Stages.Count-1; i++)
            {
                int j = i + random.Next(Stages.Count-i);

                (Stages[i], Stages[j]) = (Stages[j], Stages[i]);
            }
        }

        public bool TheOtherOneIsBetter(CSchedule otherOne)
        // Determines which schedule is better
        {
            return (otherOne.Stages.Count < this.Stages.Count);
        }
    }
}
