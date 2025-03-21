namespace CodeBase.Gameplay.Initiative
{
    public interface IInitiativeService
    {
        void ReplenishInitiativeTick();
        bool HeroIsReadyOnNextTick();
    }
}