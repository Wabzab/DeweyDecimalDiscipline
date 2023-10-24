using System;
using System.Collections.Generic;
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
    }
}
