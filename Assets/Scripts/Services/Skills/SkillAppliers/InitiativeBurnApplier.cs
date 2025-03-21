using CodeBase.Gameplay.Heroes;
using CodeBase.Gameplay.HeroRegistry;
using CodeBase.Gameplay.UI;
using CodeBase.StaticData.Heroes;
using CodeBase.StaticData.Skills;
using Services.StaticData;
using UnityEngine;

namespace CodeBase.Gameplay.Skills.SkillAppliers
{
    public class InitiativeBurnApplier : ISkillApplier
    {
        private const string FXPrefabPath = "Fx/energyBall/energyBalls";

        private readonly IStaticDataService _staticDataService;
        private readonly IHeroRegistry _heroRegistry;
        private readonly IBattleTextPlayer _battleTextPlayer;
        
        private GameObject _fXPrefab;

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
                PlayFx(target.transform.position);
            }
        }

        public void WarmUp()
        {
            _fXPrefab = Resources.Load<GameObject>(FXPrefabPath);
        }

        private void PlayFx(Vector3 targetPosition) =>
            Object.Instantiate(_fXPrefab, targetPosition, Quaternion.identity);
    }
}