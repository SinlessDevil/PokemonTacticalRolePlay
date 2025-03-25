using System;

namespace Services.Battle
{
    public interface IBattleConductor
    {
        void Start();
        void Stop();
        void ResumeTurnTimer();
        void SetMode(BattleMode mode);
        void CleanUp();
        BattleMode Mode { get; }

        event Action<HeroAction> HeroActionProduced;
    }
}