using System.Collections.Generic;
using Services.AI.UtilityAI;

namespace Services.AI.Reporting
{
    public class DecisionScores
    {
        public string HeroName;

        public string FormattedLine;

        public List<ScoredAction> Choices;
    }
}