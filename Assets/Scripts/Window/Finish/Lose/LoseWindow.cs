using System;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.Game.States;
using Services.SFX;
using Services.StaticData;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;

namespace Window.Finish.Lose
{
    public class LoseWindow : FinishWindow
    {
        [SerializeField] private Text _textLose;
        
        private string _lose;
        
        private IStateMachine<IGameState> _gameStateMachine;
        private IStaticDataService _staticDataService;
        private ISoundService _soundService;
        
        [Inject]
        public void Constructor(
            IStateMachine<IGameState> gameStateMachine,
            IStaticDataService staticDataService,
            ISoundService soundService)
        {
            _gameStateMachine = gameStateMachine;
            _staticDataService = staticDataService;
            _soundService = soundService;
        }

        public void OnDestroy()
        {
            UnsubscribeEvents();
        }
        
        public void Initialize()
        {
            SubscribeEvents();
        }

        public override void ResetWindow()
        {
            _mainContaienr.localScale = Vector3.zero;
            _canvasGroupBG.alpha = 0;
            _headerContainer.localScale = new Vector3(0, 1, 1);
            
            _lose = _textLose.text;
            
            _textLose.text = string.Empty;
            
            _buttonLoadLevel.transform.localScale = Vector3.zero;
            _buttonExitToMenu.transform.localScale = Vector3.zero;
        }

        public override void OpenWindow(Action onFinished)
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(_canvasGroupBG.DOFade(1f, 0.5f).SetEase(Ease.Linear));
            sequence.Join(_mainContaienr.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce));
            sequence.AppendInterval(0.2f);
            sequence.Append(_headerContainer.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce));
            sequence.AppendInterval(0.2f);
            sequence.Append(_textLose.DOText(_lose, 0.25f).SetEase(Ease.Linear));
            sequence.AppendInterval(0.2f);
            sequence.Append(_buttonLoadLevel.transform.DOScale(1, 0.5f).SetEase(Ease.OutBounce));
            sequence.Join(_buttonExitToMenu.transform.DOScale(1, 0.5f).SetEase(Ease.OutBounce));
            sequence.OnComplete(() =>
            {
                _buttonScalers.ForEach(x=> x.Reset());
            });
            
            sequence.OnComplete(() => onFinished?.Invoke());
        }

        protected override void OnLoadLevelButtonClick()
        {
            _soundService.ButtonClick();
            _gameStateMachine.Enter<LoadLevelState, string>(_staticDataService.GameConfig.GameScene);
        }

        protected override void OnExitToMenuButtonClick()
        {
            _soundService.ButtonClick();
            _gameStateMachine.Enter<LoadMenuState, string>(_staticDataService.GameConfig.MenuScene);
        }
    }
}