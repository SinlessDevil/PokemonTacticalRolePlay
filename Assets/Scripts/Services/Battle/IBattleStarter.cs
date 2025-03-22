using StaticData.Heroes;

namespace Services.Battle
{
    public interface IBattleStarter
    {
        void StartRandomBattle(SlotSetupBehaviour slotSetup);
        void Clear();
        HeroTypeId RandomHeroTypeId();
    }
}