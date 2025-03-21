using System;

namespace CodeBase.Gameplay.Battle
{
    public interface IBattleConductor
    {
        void Start();
        void Stop();
        void ResumeTurnTimer();
        void SetMode(BattleMode mode);
        BattleMode Mode { get; }

        event Action<HeroAction> HeroActionProduced;
    }
}