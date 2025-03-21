using CodeBase.Extensions;
using UnityEngine;

namespace CodeBase.Gameplay.Heroes
{
    public class HeroAnimator : MonoBehaviour
    {
        private readonly int _idleStateHash = Animator.StringToHash("Base Layer.Idle");
        private readonly int _skill1StateHash = Animator.StringToHash("Base Layer.Skill1");
        private readonly int _skill2StateHash = Animator.StringToHash("Base Layer.Skill2");
        private readonly int _skill3StateHash = Animator.StringToHash("Base Layer.Skill3");
        private readonly int _deathStateHash = Animator.StringToHash("Base Layer.Death");

        private readonly int _playDeathHash = Animator.StringToHash("die");
        private readonly int _playSkill1Hash = Animator.StringToHash("skill1");
        private readonly int _playSkill2Hash = Animator.StringToHash("skill2");
        private readonly int _playSkill3Hash = Animator.StringToHash("skill3");

        public Animator Animator;
        private int[] _skills;

        private void Awake()
        {
            if (Animator == null)
                Animator = GetComponent<Animator>();

            _skills = new[] { _playSkill1Hash, _playSkill2Hash, _playSkill3Hash };
        }

        public void PlaySkill(int index)
        {
            ResetAllTriggers();
            Animator.SetTrigger(_skills.ElementAtOrFirst(index - 1));
        }

        public void PlayDeath()
        {
            ResetAllTriggers();
            Animator.SetTrigger(_playDeathHash);
        }

        private void ResetAllTriggers()
        {
            if (Animator.runtimeAnimatorController == null)
                return;

            Animator.ResetTrigger(_playDeathHash);

            if (_skills != null)
            {
                foreach (int trigger in _skills)
                    Animator.ResetTrigger(trigger);
            }
        }
    }
}