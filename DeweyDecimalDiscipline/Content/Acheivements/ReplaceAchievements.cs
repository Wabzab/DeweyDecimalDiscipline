﻿using DeweyDecimalDiscipline.Models;
using DeweyDecimalDiscipline.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeweyDecimalDiscipline.Content.Acheivements
{
    public static class ReplaceAchievements
    {
        public static List<Achievement> Achievements = new List<Achievement>()
        {
            new Achievement(
                "Speed Demon I",
                "Complete a replace books task in under 30 seconds.",
                new List<Condition>() { new Condition(AchievementConstants.Type.Time, AchievementConstants.Comparison.Less, 30) }
                ),
            new Achievement(
                "Speed Demon II",
                "Complete a replace books task in under 15 seconds.",
                new List<Condition>() { new Condition(AchievementConstants.Type.Time, AchievementConstants.Comparison.Less, 15) }
                ),
            new Achievement(
                "Speed Demon III",
                "Complete a replace books task in under 5 seconds.",
                new List<Condition>() { new Condition(AchievementConstants.Type.Time, AchievementConstants.Comparison.Less, 5) }
                ),
            new Achievement(
                "Book Worm I",
                "Complete a replace books task with 50% or higher.",
                new List<Condition>() { new Condition(AchievementConstants.Type.Score, AchievementConstants.Comparison.More, 0.5) }
                ),
            new Achievement(
                "Book Worm II",
                "Complete a replace books task with 80% or higher.",
                new List<Condition>() { new Condition(AchievementConstants.Type.Score, AchievementConstants.Comparison.More, 0.8) }
                ),
            new Achievement(
                "Book Worm III",
                "Complete a replace books task with 100% or higher.",
                new List<Condition>() { new Condition(AchievementConstants.Type.Score, AchievementConstants.Comparison.More, 1.0) }
                )
        };

        public static List<Achievement> GetAchievements(List<Replacement> scores)
        {
            List<Achievement> result = new List<Achievement>();
            foreach (Achievement a in Achievements)
            {
                bool conditionsPassed = true;
                foreach (Condition c in a.Conditions)
                {
                    if(!IsConditionPassed(c, scores)) { conditionsPassed = false; break; }
                }
                if (conditionsPassed)
                {
                    result.Add(a);
                }
            }
            return result;
        }

        private static bool IsConditionPassed(Condition condition, List<Replacement> scores)
        {
            foreach (Replacement s in scores)
            {
                double value = -1;
                switch (condition.Type) {
                    case AchievementConstants.Type.Score:
                        value = s.Score/10.0;
                        break;
                    case AchievementConstants.Type.Time:
                        value = s.Time.TotalSeconds;
                        break;
                }
                switch (condition.Comparison)
                {
                    case AchievementConstants.Comparison.Less:
                        if (value <= condition.Value)
                        {
                            return true;
                        }
                        break;
                    case AchievementConstants.Comparison.More:
                        if (value >= condition.Value)
                        {
                            return true;
                        }
                        break;
                    case AchievementConstants.Comparison.Equal:
                        if (value == condition.Value)
                        {
                            return true;
                        }
                        break;
                    case AchievementConstants.Comparison.NotEqual:
                        if (value != condition.Value)
                        {
                            return true;
                        }
                        break;
                }
            }
            return false;
        }
    }
}
