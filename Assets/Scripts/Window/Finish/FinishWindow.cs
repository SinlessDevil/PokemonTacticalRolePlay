using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UI.Buttons;

namespace Window.Finish
{
    public abstract class FinishWindow : MonoBehaviour
    {
        [SerializeField] protected Button _buttonLoadLevel;
        [SerializeField] protected Button _buttonExitToMenu;
        [Space(10)] [Header("Addition Components")]
        [SerializeField] protected RectTransform _mainContaienr;
        [SerializeField] protected CanvasGroup _canvasGroupBG;
        [SerializeField] protected RectTransform _headerContainer;
        [SerializeField] protected List<ButtonScaler> _buttonScalers;
        

        protected virtual void SubscribeEvents()
        {
            _buttonLoadLevel.onClick.AddListener(OnLoadLevelButtonClick);
            _buttonExitToMenu.onClick.AddListener(OnExitToMenuButtonClick);
        }
        
        protected virtual void UnsubscribeEvents()
        {
            _buttonLoadLevel.onClick.RemoveListener(OnLoadLevelButtonClick);
            _buttonExitToMenu.onClick.RemoveListener(OnExitToMenuButtonClick);
        }

        public abstract void ResetWindow();

        public abstract void OpenWindow(Action onFinished);
        
        protected abstract void OnLoadLevelButtonClick();
        protected abstract void OnExitToMenuButtonClick();
    }
}