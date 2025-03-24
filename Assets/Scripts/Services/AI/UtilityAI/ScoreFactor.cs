namespace Services.AI.UtilityAI
{
    public class ScoreFactor
    {
        public string Name { get; }
        public float Score { get; }

        public ScoreFactor(string name, float score)
        {
            Name = name;
            Score = score;
        }
    }
}