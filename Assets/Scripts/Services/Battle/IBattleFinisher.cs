namespace Services.Battle
{
    public interface IBattleFinisher
    {
        void HandleFinish();
        void CleanUp();
    }
}