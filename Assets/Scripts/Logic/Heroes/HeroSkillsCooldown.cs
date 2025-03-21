using System.Collections.Generic;
using CodeBase.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Gameplay.Heroes
{
    public class HeroSkillsCooldown : MonoBehaviour
    {
        public Image SkillCDPrefab;
        public HeroBehaviour Hero;
        public Transform SkillsRoot;

        private List<Image> _skillCDs = new(3);

        private void Update()
        {
            if (Hero == null || Hero.State == null)
                return;

            for (var i = 0; i < Hero.State.SkillStates.Count; i++)
            {
                SkillState state = Hero.State.SkillStates[i];

                if (_skillCDs.Count < i + 1)
                {
                    _skillCDs.Add(
                        Instantiate(SkillCDPrefab, SkillsRoot)
                            .With(x => x.color = state.Color));
                }

                _skillCDs[i].fillAmount = (state.MaxCooldown - state.Cooldown) / state.MaxCooldown;
            }
        }
    }
}