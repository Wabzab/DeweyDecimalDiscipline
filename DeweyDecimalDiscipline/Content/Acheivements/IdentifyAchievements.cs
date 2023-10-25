using DeweyDecimalDiscipline.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeweyDecimalDiscipline.Content.Acheivements
{
    public static class IdentifyAchievements
    {
        public static List<Achievement> Achievements = new List<Achievement>()
        {
            new Achievement(
                "Dewey Guesser I",
                "Complete an identify areas task with 50% or higher.",
                new List<Condition>() { new Condition(AchievementConstants.Type.Score, AchievementConstants.Comparison.More, 0.5) }
                ),
            new Achievement(
                "Dewey Guesser II",
                "Complete an identify areas task with 75% or higher.",
                new List<Condition>() { new Condition(AchievementConstants.Type.Score, AchievementConstants.Comparison.More, 0.75) }
                ),
            new Achievement(
                "Dewey Guesser III",
                "Complete an identify areas task with 100%.",
                new List<Condition>() { new Condition(AchievementConstants.Type.Score, AchievementConstants.Comparison.More, 1.0) }
                )
        };

        public static List<Achievement> GetAchievements(List<Identifying> scores)
        {
            List<Achievement> result = new List<Achievement>();
            foreach (Achievement a in Achievements)
            {
                bool conditionsPassed = true;
                foreach (Condition c in a.Conditions)
                {
                    if (!IsConditionPassed(c, scores)) { conditionsPassed = false; break; }
                }
                if (conditionsPassed)
                {
                    result.Add(a);
                }
            }
            return result;
        }

        private static bool IsConditionPassed(Condition condition, List<Identifying> scores)
        {
            foreach (Identifying s in scores)
            {
                double value = -1;
                switch (condition.Type)
                {
                    case AchievementConstants.Type.Score:
                        value = s.Score / 4.0;
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
