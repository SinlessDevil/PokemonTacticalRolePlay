using Logic.Heroes;
using Services.Battle;
using StaticData.Heroes;

namespace Services.Factories.Hero
{
    public interface IHeroFactory
    {
        HeroBehaviour CreateHeroAt(HeroTypeId heroTypeId, Slot slot, bool turned);
    }
}