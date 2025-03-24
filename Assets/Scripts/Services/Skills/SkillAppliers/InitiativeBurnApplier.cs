using Logic.Heroes;
using Services.HeroRegistry;
using Services.StaticData;
using StaticData.Heroes;
using StaticData.Skills;
using UI.Game;
using UnityEngine;

namespace Services.Skills.SkillApplier
{
    public class InitiativeBurnApplier : ISkillApplier
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IHeroRegistry _heroRegistry;
        private readonly IBattleTextPlayer _battleTextPlayer;

        public SkillKind SkillKind => SkillKind.InitiativeBurn;

        public InitiativeBurnApplier(
            IStaticDataService staticDataService,
            IHeroRegistry heroRegistry,
            IBattleTextPlayer battleTextPlayer) 
        {
            _staticDataService = staticDataService;
            _heroRegistry = heroRegistry;
            _battleTextPlayer = battleTextPlayer;
        }

        public void ApplySkill(ActiveSkill activeSkill)
        {
            foreach (string targetId in activeSkill.TargetIds)
            {
                if (!_heroRegistry.IsAlive(targetId))
                    continue;

                HeroBehaviour caster = _heroRegistry.GetHero(activeSkill.CasterId);
                HeroSkill skill = _staticDataService.HeroSkillFor(activeSkill.Skill, caster.TypeId);

                HeroBehaviour target = _heroRegistry.GetHero(targetId);

                float burnt = target.State.MaxInitiative * skill.Value;
                target.State.CurrentInitiative -= burnt;
                if (target.State.CurrentInitiative < 0)
                    target.State.CurrentInitiative = 0;

                _battleTextPlayer.PlayText($"-{burnt}", new Color(0.7f, 0.4f, 0.2f, 1f), target.transform.position);
                PlayFx(skill.CustomTargetFx, target.transform.position);
            }
        }

        public void WarmUp()
        {
            
        }

        public float CalculateSkillValue(string casterId, SkillTypeId skillTypeId, string targetId)
        {
            HeroBehaviour caster = _heroRegistry.GetHero(casterId);
            HeroBehaviour target = _heroRegistry.GetHero(targetId);
            HeroSkill skill = _staticDataService.HeroSkillFor(skillTypeId, caster.TypeId);
      
            return target.State.MaxInitiative * skill.Value;
        }
        
        private void PlayFx(GameObject fxPrefab, Vector3 position)
        {
            if (fxPrefab)
                Object.Instantiate(fxPrefab, position + Vector3.up * 1.5f, Quaternion.identity);
        }  
    }
}