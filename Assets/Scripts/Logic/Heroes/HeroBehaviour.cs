using CodeBase.StaticData.Heroes;
using UnityEngine;

namespace CodeBase.Gameplay.Heroes
{
    public class HeroBehaviour : MonoBehaviour, IHero
    {
        public HeroAnimator Animator;
        public Transform Sprite;
        public GameObject NextTurnPointer;

        private HeroState _state;
        public HeroState State => _state;

        public string Id { get; set; }
        public HeroTypeId TypeId { get; set; }
        public int SlotNumber { get; set; }

        public bool IsDead => State.CurrentHp <= 0;
        public bool IsReady => State.CurrentInitiative >= State.MaxInitiative;

        public void InitializeWithState(HeroState state, bool turn, int slotSlotNumber)
        {
            _state = state;

            SlotNumber = slotSlotNumber;

            if (turn)
                Turn(Sprite);
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