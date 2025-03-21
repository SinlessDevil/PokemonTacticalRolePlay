using System.Collections.Generic;
using CodeBase.Gameplay.Battle;
using CodeBase.Gameplay.Heroes;
using CodeBase.Gameplay.HeroRegistry;
using CodeBase.Gameplay.Skills.SkillAppliers;
using CodeBase.Gameplay.UI;
using CodeBase.StaticData.Heroes;
using CodeBase.StaticData.Skills;
using Services.StaticData;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Skills
{
    public class SkillSolver : ISkillSolver, IInitializable
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IHeroRegistry _heroRegistry;
        private readonly IBattleTextPlayer _battleTextPlayer;
        private readonly List<ActiveSkill> _activeSkills = new(16);

        private List<ISkillApplier> _appliers;

        public SkillSolver(
            IStaticDataService staticDataService,
            IHeroRegistry heroRegistry,
            IBattleTextPlayer battleTextPlayer)
        {
            _heroRegistry = heroRegistry;
            _battleTextPlayer = battleTextPlayer;
            _staticDataService = staticDataService;

            InitSkillAppliers();
        }

        public void ProcessHeroAction(HeroAction heroAction)
        {
            HeroSkill skill = Skill(heroAction);

            HeroBehaviour hero = _heroRegistry.GetHero(heroAction.Caster.Id);

            hero.Animator
                .PlaySkill(skill.Animation.AnimationIndex);

            hero.State.SkillStates
                .Find(x => x.TypeId == skill.TypeId)
                .PutOnCooldown();

            ShowSkillName(heroAction.Caster.Id, skill.TypeId);

            _activeSkills.Add(new ActiveSkill
            {
                Skill = heroAction.Skill,
                Kind = skill.Kind,
                CasterId = heroAction.Caster.Id,
                TargetIds = heroAction.TargetIds,
                DelayLeft = skill.Animation.Delay
            });
        }

        public void Initialize()
        {
            foreach (ISkillApplier skillApplier in _appliers)
                skillApplier.WarmUp();
        }

        public void SkillDelaysTick()
        {
            for (int i = _activeSkills.Count - 1; i >= 0; i--)
            {
                ActiveSkill activeSkill = _activeSkills[i];
                activeSkill.DelayLeft -= Time.deltaTime;

                if (activeSkill.DelayLeft <= 0)
                {
                    _activeSkills.Remove(activeSkill);
                    if (_heroRegistry.IsAlive(activeSkill.CasterId))
                    {
                        ApplySkill(activeSkill);
                    }
                }
            }
        }

        private void ShowSkillName(string casterId, SkillTypeId skillTypeId)
        {
            HeroBehaviour caster = _heroRegistry.GetHero(casterId);

            _battleTextPlayer.PlayText(SkillName(), Color.yellow, caster.transform.position);

            string SkillName()
            {
                return _staticDataService.HeroSkillFor(skillTypeId, caster.TypeId).Name;
            }
        }

        private void ApplySkill(ActiveSkill activeSkill)
        {
            foreach (ISkillApplier applier in _appliers)
            {
                if (applier.SkillKind == activeSkill.Kind)
                    applier.ApplySkill(activeSkill);
            }
        }

        private HeroSkill Skill(HeroAction heroAction) =>
            _staticDataService.HeroConfigFor(heroAction.Caster.TypeId)
                .Skills.Find(x => x.TypeId == heroAction.Skill);

        private void InitSkillAppliers()
        {
            _appliers = new List<ISkillApplier>
            {
                new HealApplier(_staticDataService, _heroRegistry, _battleTextPlayer),
                new DamageApplier(_staticDataService, _heroRegistry, _battleTextPlayer),
                new InitiativeBurnApplier(_staticDataService, _heroRegistry, _battleTextPlayer),
            };
        }
    }
}