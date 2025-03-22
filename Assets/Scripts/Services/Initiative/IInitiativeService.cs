namespace Services.Initiative
{
    public interface IInitiativeService
    {
        void ReplenishInitiativeTick();
        bool HeroIsReadyOnNextTick();
    }
}