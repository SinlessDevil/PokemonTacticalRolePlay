using StaticData.Heroes;

namespace Services.Battle
{
    public interface IBattleStarter
    {
        void SetUpSlotSetup(SlotSetupBehaviour slotSetup);
        void StartRandomBattle();
        void Clear();
        HeroTypeId RandomHeroTypeId();
    }
}