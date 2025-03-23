using Services.SFX;
using Services.Window;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Window.DebugWindow
{
    public class DebugButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        
        private DebugWindow _debugWindow;

        private ISoundService _soundService;
        private IWindowService _windowService;
        
        [Inject]
        public void Constructor(
            ISoundService soundService,
            IWindowService windowService)
        {
            _soundService = soundService;
            _windowService = windowService;
        }
        
        private void OnEnable() => _button.onClick.AddListener(OpenPauseWindow);
        private void OnDisable() => _button.onClick.RemoveListener(OpenPauseWindow);

        private void OpenPauseWindow()
        {
            _soundService.ButtonClick();

            if(_debugWindow == null)
                return;
            
            var prefab = _windowService.Open(WindowTypeId.Debug);
            _debugWindow = prefab.GetComponent<DebugWindow>();
            _debugWindow.Initialize();
        }
    }
}