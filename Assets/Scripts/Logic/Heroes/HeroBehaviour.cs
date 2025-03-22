using CodeBase.StaticData.Heroes;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Gameplay.Heroes
{
    public class HeroBehaviour : MonoBehaviour, IHero
    {
        public HeroAnimator Animator;
        public Transform Visual;
        public GameObject NextTurnPointer;

        private HeroState _state;
        
        public string Id { get; set; }
        public HeroTypeId TypeId { get; set; }
        public int SlotNumber { get; set; }
        
        public bool IsDead => State.CurrentHp <= 0;
        public bool IsReady => State.CurrentInitiative >= State.MaxInitiative;
        public HeroState State => _state;
        
        public void InitializeWithState(HeroState state, bool turn, int slotSlotNumber)
        {
            _state = state;

            SlotNumber = slotSlotNumber;

            if (turn)
                Turn(Visual);
        }

        public void SwitchNextTurnPointer(bool on) =>
            NextTurnPointer.SetActive(@on);

        private void Turn(Transform transformToTurn)
        {
            Vector3 scale = transformToTurn.localScale;
            scale.x = -scale.x;
            transformToTurn.localScale = scale;
        }
    }
}