using System;
using CodeBase.Gameplay.Battle;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.Gameplay.UI
{
    public class NextHeroTurnButton : MonoBehaviour
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
            Button.onClick.AddListener(RunBattleConductor);
        }

        private void RunBattleConductor()
        {
            _battleConductor.ResumeTurnTimer();
        }

        private void Update()
        {
            Button.interactable = _battleConductor.Mode == BattleMode.Manual;
        }

        private void OnDestroy()
        {
            Button.onClick.RemoveListener(RunBattleConductor);
        }
    }
}