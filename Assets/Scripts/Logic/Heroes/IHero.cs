using CodeBase.StaticData.Heroes;

namespace CodeBase.Gameplay.Heroes
{
    public interface IHero
    {
        HeroState State { get; }
        string Id { get; set; }
        HeroTypeId TypeId { get; set; }
        int SlotNumber { get; set; }
        bool IsDead { get; }
        bool IsReady { get; }
        void InitializeWithState(HeroState state, bool turn, int slotSlotNumber);
        void SwitchNextTurnPointer(bool on);
    }
}