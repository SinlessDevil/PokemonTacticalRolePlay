using System;
using System.Collections.Generic;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.Game.States;
using Services.SFX;
using Services.StaticData;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;

namespace Window.Finish.Win
{
    public class WinWindow : FinishWindow
    {
        [SerializeField] private Text _textGems;
        [SerializeField] private Text _textGolds;
        [SerializeField] private List<Start> _stars;
        [SerializeField] private Text _textWin;
        
        private string _win;
        private string _golds;
        private string _gems;
        private int _countStars;
        
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

        public void SetGold(string gold) => _golds = gold;

        public void SetGems(string gems) => _gems = gems;
        
        public void SetCountStars(int countStars) => _countStars = countStars;

        public override void ResetWindow()
        {
            _mainContaienr.localScale = Vector3.zero;
            _canvasGroupBG.alpha = 0;
            _headerContainer.localScale = new Vector3(0, 1, 1);
            
            _win = _textWin.text;
            
            _textWin.text = string.Empty;
            _textGolds.text = string.Empty;
            _textGems.text = string.Empty;
            
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
            sequence.AppendCallback(PlayStartShowAnimation);
            sequence.AppendInterval(0.35f);
            sequence.Append(_textWin.DOText(_win, 0.25f).SetEase(Ease.Linear));
            sequence.Append(_textGolds.DOText(_golds, 0.25f).SetEase(Ease.Linear));
            sequence.Append(_textGems.DOText(_gems, 0.25f).SetEase(Ease.Linear));
            sequence.AppendInterval(0.2f);
            sequence.Append(_buttonLoadLevel.transform.DOScale(1, 0.5f).SetEase(Ease.OutBounce));
            sequence.Join(_buttonExitToMenu.transform.DOScale(1, 0.5f).SetEase(Ease.OutBounce));
            sequence.OnComplete(() =>
            {
                _buttonScalers.ForEach(x=> x.Reset());
            });
            
            sequence.OnComplete(() => onFinished?.Invoke());
        }
        
        private void PlayStartShowAnimation()
        {
            for (int i = 0; i < _countStars; i++)
            {
                _stars[i].Filled();
                _stars[i].Play();
            }
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