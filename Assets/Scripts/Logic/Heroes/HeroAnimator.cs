using System;
using CodeBase.Extensions;
using UnityEngine;

namespace CodeBase.Gameplay.Heroes
{
    public class HeroAnimator : MonoBehaviour
    {
        private readonly int _idleStateHash = Animator.StringToHash("Idle");
        private readonly int _playDeathHash = Animator.StringToHash("Die");
        private readonly int _playTakeDamageHash = Animator.StringToHash("TakeDamage");
        private readonly int _playSkill1Hash = Animator.StringToHash("CastSpell");
        private readonly int _playSkill2Hash = Animator.StringToHash("ProjectileAttack");

        public Animator Animator;
        private int[] _skills;

        private void OnValidate()
        {
            if (Animator == null)
            {
                Animator = GetComponentInChildren<Animator>();
            }
        }

        private void Awake()
        {
            if (Animator == null)
                Animator = GetComponent<Animator>();

            _skills = new[] { _playSkill1Hash, _playSkill2Hash, };
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

        public void PlayTakeDamage()
        {
            ResetAllTriggers();
            Animator.SetTrigger(_playTakeDamageHash);
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