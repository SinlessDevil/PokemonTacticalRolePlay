using System.Collections.Generic;
using Services.AI.UtilityAI;

namespace Services.AI.Reporting
{
    public class DecisionDetails
    {
        public string CasterName;
        public string TargetName;
        public string SkillName;

        public string FormattedLine;

        public List<ScoreFactor> Scores;
    }
}