using Services.Battle;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Game
{
    public class ManualModeButton : MonoBehaviour
    {
        public Button Button;
        private IBattleConductor _battleConductor;

        [Inject]
        private void Construct(IBattleConductor battleConductor)
        {
            _battleConductor = battleConductor;
        }

        private void Awake()
        {
            Button.onClick.AddListener(SetMode);
        }

        private void Update()
        {
            Button.interactable = _battleConductor.Mode == BattleMode.Auto;
        }

        private void SetMode()
        {
            _battleConductor.SetMode(BattleMode.Manual);
        }

        private void OnDestroy()
        {
            Button.onClick.RemoveListener(SetMode);
        }
    }
}