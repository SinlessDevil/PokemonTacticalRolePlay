using UnityEngine;
using UnityEngine.UI;

namespace Window.Finish
{
    public abstract class FinishWindow : MonoBehaviour
    {
        [SerializeField] protected Button _buttonLoadLevel;
        [SerializeField] protected Button _buttonExitToMenu;
        [SerializeField] protected Button _buttonRestartLevel;
        
        protected virtual void SubscribeEvents()
        {
            _buttonLoadLevel?.onClick.AddListener(OnLoadLevelButtonClick);
            _buttonExitToMenu?.onClick.AddListener(OnExitToMenuButtonClick);
            _buttonRestartLevel?.onClick.AddListener(OnRestartLevelButtonClick);
        }
        
        protected virtual void UnsubscribeEvents()
        {
            _buttonLoadLevel?.onClick.RemoveListener(OnLoadLevelButtonClick);
            _buttonExitToMenu?.onClick.RemoveListener(OnExitToMenuButtonClick);
            _buttonRestartLevel?.onClick.RemoveListener(OnRestartLevelButtonClick);
        }
        
        protected abstract void OnLoadLevelButtonClick();
        protected abstract void OnExitToMenuButtonClick();
        protected abstract void OnRestartLevelButtonClick();
    }   
}