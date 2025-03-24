using System;
using System.Collections.Generic;
using Logic.Heroes;
using Services.AI.UtilityAI;

namespace Services.AI.Reporting
{
    public interface IAIReporter
    {
        void ReportDecisionDetails(BattleSkill skill, IHero target, List<ScoreFactor> scoreFactors);
        void ReportDecisionScores(IHero readyHero, List<ScoredAction> choices);

        event Action<DecisionDetails> DecisionDetailsReported;
        event Action<DecisionScores> DecisionScoresReported;
    }
}