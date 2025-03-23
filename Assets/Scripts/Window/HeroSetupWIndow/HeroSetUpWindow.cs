using System.Collections.Generic;
using Services.Battle;
using Services.Factories.HeroSetup;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Services.Levels;

namespace Window.HeroSetUpWindow
{
    public class HeroSetUpWindow : MonoBehaviour
    {
        [SerializeField] private HorizontalLayoutGroup _containerChoice;
        [SerializeField] private HorizontalLayoutGroup _containerSelection;
        [SerializeField] private HorizontalLayoutGroup _containerEnemies;
        [SerializeField] private Text _textCount;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private List<HeroCard> _heroCardsSelected = new();
        private List<HeroCard> _heroCardsChoice = new();
        
        private IBattleStarter _battleStarter;
        private IHeroSetUpFactory _heroSetUpFactory;
        private ILevelService _levelService;

        [Inject]
        public void Construct(
            IBattleStarter battleStarter,
            IHeroSetUpFactory heroSetUpFactory,
            ILevelService levelService)
        {
            _battleStarter = battleStarter;
            _heroSetUpFactory = heroSetUpFactory;
            _levelService = levelService;
        }

        public async UniTask Initialize()
        {
            await PlayAnimationShow().Play().ToUniTask();
            UpdateTextCount();
            OnInitEnemiesHeroCardAsync().Forget();
            OnUpdateHeroCardAsync().Forget();
        }

        private Tween PlayAnimationShow()
        {
            _canvasGroup.alpha = 0f;
            return _canvasGroup.DOFade(1f, 0.35f)
                .SetEase(Ease.Linear);
        }

        private Tween PlayAnimationHide()
        {
            _canvasGroup.alpha = 1f;
            return _canvasGroup.DOFade(0f, 0.35f)
                .SetEase(Ease.Linear);
        }
        
        private async UniTask OnInitEnemiesHeroCardAsync()
        {
            List<UniTask> animationTasks = new();

            var enemies = _levelService.GetCurrentLevelStaticData().Enemies;
            var enemiesHeroCard = new List<HeroCard>(3);
            
            for (int i = 0; i < _battleStarter.GetMaxHeroesCount; i++)
            {
                var heroCard = _heroSetUpFactory.CreateHeroCard((RectTransform)_containerEnemies.transform, enemies[i]);
                animationTasks.Add(heroCard.PlayAnimationShow().ToUniTask());
                enemiesHeroCard.Add(heroCard);
            }
            
            await UniTask.WhenAll(animationTasks);
            
            enemiesHeroCard.ForEach(x => x.Interactive(false));
        }
        
        private async UniTask OnUpdateHeroCardAsync()
        {
            await DestroyHeroCardAsync();
            await SetUpHeroCardAsync();
        }

        private async UniTask DestroyHeroCardAsync()
        {
            List<UniTask> animationTasks = new();

            foreach (var heroCard in _heroCardsChoice)
            {
                heroCard.SelectedHeroCard -= OnSelectedHeroCardWrapper;
                animationTasks.Add(heroCard.PlayAnimationHide().ToUniTask());
            }

            await UniTask.WhenAll(animationTasks);

            _heroCardsChoice.ForEach(x => Destroy(x.gameObject));
            _heroCardsChoice.Clear();
        }
        
        private async UniTask SetUpHeroCardAsync()
        {
            List<UniTask> animationTasks = new();
            
            for (int i = 0; i < _battleStarter.GetMaxHeroesCount; i++)
            {
                var heroCard = _heroSetUpFactory.CreateHeroCard((RectTransform)_containerChoice.transform, _battleStarter.RandomHeroTypeId());
                _heroCardsChoice.Add(heroCard);
                heroCard.SelectedHeroCard += OnSelectedHeroCardWrapper;
                animationTasks.Add(heroCard.PlayAnimationShow().ToUniTask());
            }
            
            await UniTask.WhenAll(animationTasks);
            
            _heroCardsChoice.ForEach(x => x.ResetButtonScale());
        }

        private void OnSelectedHeroCardWrapper(HeroCard selected)
        {
            OnSelectedHeroCardAsync(selected).Forget();
        }
        
        private async UniTask OnSelectedHeroCardAsync(HeroCard selected)
        {
            _battleStarter.AddPlayerHero(selected.HeroTypeId);
            
            _heroCardsChoice.ForEach(x => x.Interactive(false));
            selected.transform.SetParent(this.transform);
            _heroCardsChoice.Remove(selected);
            
            selected.SelectedHeroCard -= OnSelectedHeroCardWrapper;
            
            _heroCardsSelected.Add(selected);

            await PlayAnimationCardMoveAsync(selected);
            
            UpdateTextCount();
            
            if (_heroCardsSelected.Count == _battleStarter.GetMaxHeroesCount)
            {
                StartGameAsync().Forget();
            }
            else
            {
                OnUpdateHeroCardAsync().Forget();
            }
        }

        private void UpdateTextCount()
        {
            _textCount.text = "Selected Hero Card " + 
                              _heroCardsSelected.Count + " / " + 
                              _battleStarter.GetMaxHeroesCount;
        }

        private async UniTask PlayAnimationCardMoveAsync(HeroCard heroCard)
        {
            Vector3 startPosition = heroCard.transform.position;
            Vector3 endPosition = _containerSelection.transform.position;
    
            float offsetX = 400f;
            Vector3 middlePoint = (startPosition + endPosition) / 2 + Vector3.right * offsetX;
            Vector3[] path = { startPosition, middlePoint, endPosition };

            heroCard.transform.DOScale(0.4f, 0.75f)
                .SetEase(Ease.Linear);
            
            await heroCard.transform.DOPath(path, 0.75f, PathType.CatmullRom)
                .SetEase(Ease.Linear)
                .ToUniTask();
            
            heroCard.transform.SetParent(_containerSelection.transform, true);
            
            await heroCard.transform.DOScale(0.3f, 0.15f)
                .SetEase(Ease.Linear)
                .ToUniTask();

            await heroCard.transform.DOScale(0.4f, 0.15f)
                .SetEase(Ease.Linear)
                .ToUniTask();
        }
        
        private async UniTask StartGameAsync()
        {
            await PlayAnimationHide();
            
            _battleStarter.StartRandomBattle();

            Dispose();
        }
        
        private void Dispose()
        {
            Destroy(gameObject);
        }
    }
}