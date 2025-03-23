using System;
using System.Linq;
using Services.StaticData;
using StaticData.Heroes;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;

namespace Window.HeroSetUpWindow
{
    public class HeroCard : MonoBehaviour
    {
        [Space(10)] [Header("Info Components")]
        [SerializeField] private Image _iconImage;
        [SerializeField] private Text _nameText;
        [SerializeField] private Text _hpText;
        [SerializeField] private Text _armorText;
        [SerializeField] private Text _initiativeText;
        [SerializeField] private Text _skillsText;
        [SerializeField] private Image _bgImage;
        [Space(10)] [Header("Additional Components")]
        [SerializeField] private Button _button;
        
        private HeroTypeId _heroTypeId;
        
        private IStaticDataService _staticDataService;
        
        [Inject]
        public void Construct(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public event Action<HeroCard> SelectedHeroCard;
        
        public HeroTypeId HeroTypeId => _heroTypeId;
        
        public void Initialize(HeroTypeId heroTypeId)
        {
            _heroTypeId = heroTypeId;
            
            SubscribeEvents();

            SetInfo();
        }

        public void PlayAnimationHide(Action callback = null)
        {
            transform.DOScale(0f, 0.35f)
                .SetEase(Ease.OutElastic)
                .OnComplete(() =>
                {
                    callback?.Invoke();
                });
        }
        
        public void PlayAnimationShow()
        {
            transform.DOScale(1f, 0.35f)
                .SetEase(Ease.InElastic);
        }
        
        private void SetInfo()
        {
            HeroConfig heroConfig = _staticDataService.HeroConfigFor(_heroTypeId);
            
            _iconImage.sprite = heroConfig.IconImage;
            _bgImage.sprite = heroConfig.BgSprite;
            _nameText.text = heroConfig.TypeId.ToString();
            _hpText.text = $"HP : {heroConfig.Hp}";
            _armorText.text = $"Armor : {heroConfig.Armor}";
            _initiativeText.text = $"Initiative : {heroConfig.Initiative}";
            _skillsText.text = $"Skills : {string.Join(", ", heroConfig.Skills.Select(x => x.Kind))}";
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _button.onClick.AddListener(OnSelectedHeroCard);
        }

        private void UnsubscribeEvents()
        {
            _button.onClick.RemoveListener(OnSelectedHeroCard);
        }

        private void OnSelectedHeroCard()
        {
            SelectedHeroCard?.Invoke(this);
            _button.interactable = false;
        }
    }    
}
