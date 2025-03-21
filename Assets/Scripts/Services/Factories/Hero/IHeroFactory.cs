using CodeBase.Gameplay.Battle;
using CodeBase.Gameplay.Heroes;
using CodeBase.StaticData.Heroes;

namespace CodeBase.Gameplay.Factory
{
    public interface IHeroFactory
    {
        HeroBehaviour CreateHeroAt(HeroTypeId heroTypeId, Slot slot, bool turned);
    }
}