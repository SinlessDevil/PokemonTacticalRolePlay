using StaticData.Heroes;

namespace Services.Battle
{
    public interface IBattleStarter
    {
        int GetMaxHeroesCount { get; }
        void SetUpSlotSetup(SlotSetupBehaviour slotSetup);
        void StartRandomBattle();
        void CleanUp();
        HeroTypeId RandomHeroTypeId();
        void AddPlayerHero(HeroTypeId heroTypeId);
    }
}