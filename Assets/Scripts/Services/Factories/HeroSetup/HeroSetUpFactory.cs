using Services.Factories.Paths;
using Services.Factories.UIFactory;
using StaticData.Heroes;
using UnityEngine;
using Window.HeroSetUpWindow;

namespace Services.Factories.HeroSetup
{
    public class HeroSetUpFactory : IHeroSetUpFactory
    {
        private IUIFactory _uiFactory;
        
        public HeroSetUpFactory(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }
        
        public HeroCard CreateHeroCard(RectTransform parent, HeroTypeId heroTypeId)
        {
            var rect = _uiFactory.CreateRectTransform(parent, ResourcePath.HeroCardPath);
            var heroCard = rect.gameObject.GetComponent<HeroCard>();
            heroCard.Initialize(heroTypeId);
            heroCard.transform.localScale = Vector3.zero;
            return heroCard;
        }
    }
}