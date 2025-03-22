using System;
using System.Collections.Generic;
using System.Linq;
using Logic.Heroes;
using Services.AI;
using Services.Cooldown;
using Services.Death;
using Services.HeroRegistry;
using Services.Initiative;
using Services.Skills;
using StaticData.Skills;
using UnityEngine;
using Zenject;

namespace Services.Battle
{
    public class BattleConductor : IBattleConductor, ITickable
    {
        private const float TurnTickDuration = 0.3f;

        private readonly IHeroRegistry _heroRegistry;
        private readonly IDeathService _deathService;
        private readonly IInitiativeService _initiativeService;
        private readonly ICooldownService _cooldownService;
        private readonly ISkillSolver _skillSolver;
        private readonly IArtificialIntelligence _artificialIntelligence;

        private float _untilNextTurnTick;
        private bool _started;
        private bool _finished;
        private bool _turnTimerPaused;

        public BattleMode Mode { get; private set; } = BattleMode.Manual;

        public event Action<HeroAction> HeroActionProduced;

        public BattleConductor(
            IHeroRegistry heroRegistry,
            IDeathService deathService,
            IInitiativeService initiativeService,
            IArtificialIntelligence artificialIntelligence,
            ICooldownService cooldownService,
            ISkillSolver skillSolver)
        {
            _artificialIntelligence = artificialIntelligence;
            _skillSolver = skillSolver;
            _heroRegistry = heroRegistry;
            _deathService = deathService;
            _deathService = deathService;
            _initiativeService = initiativeService;
            _cooldownService = cooldownService;
        }

        public void Tick()
        {
            if (!_started || _finished)
                return;

            UpdateTurnTimer();
            _skillSolver.SkillDelaysTick();
            _deathService.ProcessDeadHeroes();
            CheckBattleEnd();
        }

        public void Start() => _started = true;
        public void Stop() => _started = false;
        public void ResumeTurnTimer() => _turnTimerPaused = false;

        public void SetMode(BattleMode mode) => Mode = mode;

        private void PauseInManualMode()
        {
            if (Mode == BattleMode.Manual)
                _turnTimerPaused = true;
        }

        private void Finish() => _finished = true;

        private void UpdateTurnTimer()
        {
            if (_turnTimerPaused)
                return;

            _untilNextTurnTick -= Time.deltaTime;
            if (_untilNextTurnTick <= 0)
            {
                TurnTick();
                _untilNextTurnTick = TurnTickDuration;
            }
        }

        private void TurnTick()
        {
            _cooldownService.CooldownTick(TurnTickDuration);
            _initiativeService.ReplenishInitiativeTick();
            ProcessReadyHeroes();

            if (_initiativeService.HeroIsReadyOnNextTick())
                PauseInManualMode();
        }

        private void CheckBattleEnd()
        {
            if (_heroRegistry.PlayerTeam.Count == 0 || _heroRegistry.EnemyTeam.Count == 0)
                Finish();
        }

        private void ProcessReadyHeroes()
        {
            foreach (HeroBehaviour hero in _heroRegistry.AllActiveHeroes())
            {
                if (hero.IsReady)
                {
                    hero.State.CurrentInitiative = 0;
                    PerformHeroAction(hero);
                }
            }
        }

        public void PerformHeroAction(HeroBehaviour readyHero)
        {
            HeroAction heroAction = _artificialIntelligence.MakeBestDecision(readyHero);

            // var chosenAction = new HeroAction
            // {
            //   Skill = TempSkill(readyHero),
            //   Caster = readyHero,
            //   TargetIds = TempTargets(readyHero) 
            // };

            _skillSolver.ProcessHeroAction(heroAction);

            HeroActionProduced?.Invoke(heroAction);
        }

        private SkillTypeId TempSkill(HeroBehaviour readyHero) =>
            readyHero.State.SkillStates[0].TypeId;

        private List<string> TempTargets(HeroBehaviour readyHero) =>
            _heroRegistry.EnemiesOf(readyHero.Id).ToList();
    }
}