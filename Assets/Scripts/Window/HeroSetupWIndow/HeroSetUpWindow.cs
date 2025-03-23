using System.Collections.Generic;
using Services.Battle;
using Services.Factories.HeroSetup;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace Window.HeroSetUpWindow
{
    public class HeroSetUpWindow : MonoBehaviour
    {
        [SerializeField] private RectTransform _containerChoice;
        [SerializeField] private RectTransform _containerSelection;
        [SerializeField] private Text _textCount;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private List<HeroCard> _heroCardsSelected = new();
        private List<HeroCard> _heroCardsChoice = new();
        
        private IBattleStarter _battleStarter;
        private IHeroSetUpFactory _heroSetUpFactory;
        
        [Inject]
        public void Construct(
            IBattleStarter battleStarter,
            IHeroSetUpFactory heroSetUpFactory)
        {
            _battleStarter = battleStarter;
        }

        public void Initialize()
        {
            AnimationShow();
            OnUpdateHeroCard();
        }

        private void AnimationShow()
        {
            _canvasGroup.DOFade(1f, 0.35f)
                .SetEase(Ease.Linear);
        }
        
        private async UniTask OnUpdateHeroCard()
        {
            await DestroyHeroCard();
            SetUpHeroCards();

        }

        private async UniTask DestroyHeroCard()
        {
            foreach (HeroCard _heroCard in _heroCardsSelected)
            {
                _heroCard.SelectedHeroCard -= OnSelectedHeroCard;
                _heroCard.PlayAnimationHide();
            }
            // await 
        }

        private void SetUpHeroCards()
        {
            for (int i = 0; i < 3; i++)
            {
                var heroCard = _heroSetUpFactory.CreateHeroCard(_containerChoice, _battleStarter.RandomHeroTypeId());
                _heroCardsChoice.Add(heroCard);
                heroCard.SelectedHeroCard += OnSelectedHeroCard;
                heroCard.PlayAnimationShow();
            }
        }

        private void OnSelectedHeroCard(HeroCard selected)
        {
            selected.SelectedHeroCard -= OnSelectedHeroCard;
            _heroCardsSelected.Add(selected);

            if (_heroCardsSelected.Count == 3)
            {
                StartGame().Forget();
            }
            else
            {
                OnUpdateHeroCard().Forget();
            }
        }
        
        private async UniTask StartGame()
        {
            
            // await 
            
            _battleStarter.StartRandomBattle();

            Hide();
        }

        private void PlayAnimationHide()
        {
            
        }
        
        private void Hide()
        {
            Destroy(gameObject);
        }
    }
}