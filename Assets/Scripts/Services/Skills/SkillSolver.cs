using System.Collections.Generic;
using Logic.Heroes;
using Services.Battle;
using Services.HeroRegistry;
using Services.Skills.SkillApplier;
using Services.StaticData;
using StaticData.Heroes;
using StaticData.Skills;
using UI.Game;
using UnityEngine;
using Zenject;

namespace Services.Skills
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

        public float CalculateSkillValue(string casterId, SkillTypeId skillTypeId, string targetId)
        {
            HeroBehaviour caster = _heroRegistry.GetHero(casterId);
            SkillKind kind = _staticDataService.HeroSkillFor(skillTypeId, caster.TypeId).Kind;

            return _appliers.Find(applier => applier.SkillKind == kind)
                .CalculateSkillValue(casterId, skillTypeId, targetId);
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