using StaticData.Heroes;
using UnityEngine;
using Window.HeroSetUpWindow;

namespace Services.Factories.HeroSetup
{
    public interface IHeroSetUpFactory
    {
        HeroCard CreateHeroCard(RectTransform parent, HeroTypeId heroTypeId);
    }
}