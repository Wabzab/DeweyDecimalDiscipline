using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeweyDecimalDiscipline.Content.Acheivements
{

    public static class AchievementConstants {
        public enum Type { Time, Score }
        public enum Comparison { Less, More, Equal, NotEqual }
    }

    public class Achievement
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Condition> Conditions { get; set; }

        public Achievement(string name, string description, List<Condition> conditions)
        {
            Name = name;
            Description = description;
            Conditions = conditions;
        }
    }

    public class Condition
    {
        public AchievementConstants.Type Type { get; set; }
        public AchievementConstants.Comparison Comparison { get; set; }
        public double Value { get; set; }

        public Condition(AchievementConstants.Type type, AchievementConstants.Comparison comparison, double value)
        {
            Type = type;
            Comparison = comparison;
            Value = value;
        }
    }
}
