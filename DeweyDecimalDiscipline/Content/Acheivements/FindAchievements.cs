using DeweyDecimalDiscipline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeweyDecimalDiscipline.Content.Acheivements
{
    public static class FindAchievements
    {
        public static List<Achievement> Achievements = new List<Achievement>()
        {
            new Achievement(
                "Librarian I",
                "Complete an finding call number task with 33% or higher.",
                new List<Condition>() { new Condition(AchievementConstants.Type.Score, AchievementConstants.Comparison.More, 0.3) }
                ),
            new Achievement(
                "Librarian II",
                "Complete an finding call number task with 66% or higher.",
                new List<Condition>() { new Condition(AchievementConstants.Type.Score, AchievementConstants.Comparison.More, 0.6) }
                ),
            new Achievement(
                "Librarian III",
                "Complete an finding call number task with 100%.",
                new List<Condition>() { new Condition(AchievementConstants.Type.Score, AchievementConstants.Comparison.More, 0.9) }
                )
        };

        public static List<Achievement> GetAchievements(List<Finding> scores)
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

        private static bool IsConditionPassed(Condition condition, List<Finding> scores)
        {
            foreach (Finding s in scores)
            {
                double value = -1;
                switch (condition.Type)
                {
                    case AchievementConstants.Type.Score:
                        value = s.Score / 3.0;
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
