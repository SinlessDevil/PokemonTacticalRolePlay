using System.Linq;
using CodeBase.Extensions;
using CodeBase.Gameplay.AI.Reporting;
using CodeBase.Gameplay.Battle;
using CodeBase.Gameplay.HeroRegistry;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.Gameplay.UI.DebugWindow
{
    public class DebugWindow : MonoBehaviour
    {
        public HeroActionEntry HeroActionEntryPrefab;
        public DecisionDetailsEntry DecisionDetailsEntryPrefab;
        public DecisionScoresEntry DecisionScoresEntryPrefab;
        public Transform EntriesRoot;
        public Button CloseButton;

        private IBattleConductor _battleConductor;
        private IHeroRegistry _registry;
        private IAIReporter _aiReporter;

        [Inject]
        private void Construct(
            IBattleConductor conductor,
            IHeroRegistry registry,
            IAIReporter aiReporter)
        {
            _aiReporter = aiReporter;
            _battleConductor = conductor;
            _registry = registry;
        }

        public void Initialize()
        {
            _battleConductor.HeroActionProduced += OnHeroActionProduced;
            _aiReporter.DecisionDetailsReported += OnDecisionDetailsProduced;
            _aiReporter.DecisionScoresReported += OnDecisionScoresProduced;
            CloseButton.onClick.AddListener(OnClose);
        }

        private void OnDestroy()
        {
            _battleConductor.HeroActionProduced -= OnHeroActionProduced;
            _aiReporter.DecisionDetailsReported -= OnDecisionDetailsProduced;
            _aiReporter.DecisionScoresReported -= OnDecisionScoresProduced;
            CloseButton.onClick.RemoveListener(OnClose);
        }

        public void SwitchState() =>
            gameObject.SetActive(!gameObject.activeSelf);

        private void OnClose() => Destroy(gameObject);
        
        private void OnHeroActionProduced(HeroAction action)
        {
            Instantiate(HeroActionEntryPrefab, EntriesRoot)
                .With(x => x.HeroName.text = $"{action.Caster.TypeId} [{action.Caster.SlotNumber}]")
                .With(x => x.SkillName.text = $"{action.Skill} ({action.SkillKind})")
                .With(x => x.TargetsLine.text = action.TargetIds
                    .Aggregate(
                        "",
                        (current, id) =>
                            current + $" {_registry.GetHero(id).TypeId} [{_registry.GetHero(id).SlotNumber}]"));
        }

        private void OnDecisionScoresProduced(DecisionScores scores)
        {
            Instantiate(DecisionScoresEntryPrefab, EntriesRoot)
                .With(x => x.HeroName.text = $"{scores.HeroName}")
                .With(x => x.SkillName.text = $"")
                .With(x => x.TargetsLine.text = scores.FormattedLine);
        }

        private void OnDecisionDetailsProduced(DecisionDetails details)
        {
            Instantiate(DecisionDetailsEntryPrefab, EntriesRoot)
                .With(x => x.HeroName.text = $"{details.CasterName}")
                .With(x => x.TargetName.text = $"{details.TargetName}")
                .With(x => x.SkillName.text = $"{details.SkillName}")
                .With(x => x.TargetsLine.text = details.FormattedLine);
        }
    }
}