using CodeBase.Infrastructure;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.Game;
using Infrastructure.StateMachine.Game.States;
using Services.AI;
using Services.AI.Reporting;
using Services.Battle;
using Services.Cooldown;
using Services.Death;
using Services.Factories.Game;
using Services.Factories.Hero;
using Services.Factories.UIFactory;
using Services.Finish;
using Services.Finish.Lose;
using Services.Finish.Win;
using Services.HeroRegistry;
using Services.Initiative;
using Services.Input;
using Services.Levels;
using Services.LocalProgress;
using Services.PersistenceProgress;
using Services.Provides.Widgets;
using Services.Random;
using Services.SaveLoad;
using Services.SFX;
using Services.Skills;
using Services.Skills.Targeting;
using Services.StaticData;
using Services.Timer;
using Services.Window;
using UI.Game;
using UnityEngine;
using Zenject;
using Application = UnityEngine.Application;

namespace Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private CoroutineRunner _coroutineRunner;
        [SerializeField] private LoadingCurtain _curtain;
        [SerializeField] private SoundService _soundService;
        [SerializeField] private MusicService _musicService;
        [SerializeField] private TimeService _timeService;
        
        private RuntimePlatform Platform => Application.platform;

        public override void InstallBindings()
        {
            Debug.Log("Installer");

            BindMonoServices();
            BindSceneLoader();
            BindServices();
            BindGameStateMachine();
            MakeInitializable();
        }
        
        public void Initialize() => BootstrapGame();

        private void BindMonoServices()
        {
            Container.Bind<ICoroutineRunner>().FromMethod(() => Container.InstantiatePrefabForComponent<ICoroutineRunner>(_coroutineRunner)).AsSingle();
            Container.Bind<ILoadingCurtain>().FromMethod(() => Container.InstantiatePrefabForComponent<ILoadingCurtain>(_curtain)).AsSingle();
            Container.Bind<ISoundService>().FromMethod(() => Container.InstantiatePrefabForComponent<ISoundService>(_soundService)).AsSingle();
            Container.Bind<IMusicService>().FromMethod(() => Container.InstantiatePrefabForComponent<IMusicService>(_musicService)).AsSingle();
            Container.Bind<ITimeService>().FromMethod(() => Container.InstantiatePrefabForComponent<ITimeService>(_timeService)).AsSingle();
        }

        private void BindServices()
        {
            BindStaticDataService();
            
            BindUIFactory();
            BindGameFactory();

            Container.BindInterfacesTo<InputService>().AsSingle();
            
            BindDataServices();

            Container.BindInterfacesTo<RandomService>().AsSingle();
            Container.BindInterfacesTo<WidgetProvider>().AsSingle();
            Container.BindInterfacesTo<LevelService>().AsSingle();
            
            BindFinishServices();

            BindBattleService();

            Container.Bind<IAIReporter>().To<AIReporter>().AsSingle();
            
            Container.BindInterfacesTo<HeroRegistry>().AsSingle();
            Container.BindInterfacesTo<CooldownService>().AsSingle();
            Container.BindInterfacesTo<SkillSolver>().AsSingle();
            
            Container.Bind<IDeathService>().To<DeathService>().AsSingle();
            Container.Bind<IInitiativeService>().To<InitiativeService>().AsSingle();
            Container.Bind<ITargetPicker>().To<TargetPicker>().AsSingle();
            Container.Bind<IArtificialIntelligence>().To<StupidAI>().AsSingle();
        }

        private void BindFinishServices()
        {
            Container.BindInterfacesTo<FinishService>().AsSingle();
            Container.BindInterfacesTo<WinService>().AsSingle();
            Container.BindInterfacesTo<LoseService>().AsSingle();
        }

        private void BindBattleService()
        {
            Container.BindInterfacesTo<BattleAreaServiceInstaller>().AsSingle();
            Container.BindInterfacesTo<BattleTextPlayer>().AsSingle();
            Container.BindInterfacesTo<BattleConductor>().AsSingle();
        }

        private void BindDataServices()
        {
            Container.BindInterfacesTo<PersistenceProgressService>().AsSingle();
            Container.BindInterfacesTo<LevelLocalProgressService>().AsSingle();
            Container.BindInterfacesTo<SaveLoadService>().AsSingle();
        }

        private void BindGameFactory()
        {
            Container.BindInterfacesTo<GameFactory>().AsSingle();
            Container.Bind<IHeroFactory>().To<HeroFactory>().AsSingle();
        }

        private void BindUIFactory()
        {
            Container.BindInterfacesTo<UIFactory>().AsSingle();
            Container.BindInterfacesTo<WindowService>().AsSingle();
        }

        private void BindGameStateMachine()
        {
            Container.Bind<GameStateFactory>().AsSingle();
            Container.BindInterfacesTo<GameStateMachine>().AsSingle();
            
            BindGameStates();
        }

        private void MakeInitializable() => Container.Bind<IInitializable>().FromInstance(this);

        private void BindSceneLoader()
        {
            ISceneLoader sceneLoader = new SceneLoader(Container.Resolve<ICoroutineRunner>());
            Container.Bind<ISceneLoader>().FromInstance(sceneLoader).AsSingle();
        }

        private void BindStaticDataService()
        {
            IStaticDataService staticDataService = new StaticDataService();
            staticDataService.LoadData();
            Container.Bind<IStaticDataService>().FromInstance(staticDataService).AsSingle();
        }
        
        private void BindGameStates()
        {
            Container.Bind<BootstrapState>().AsSingle();
            Container.Bind<LoadProgressState>().AsSingle();
            Container.Bind<BootstrapAnalyticState>().AsSingle();
            Container.Bind<PreLoadGameState>().AsSingle();
            Container.Bind<LoadMenuState>().AsSingle();
            Container.Bind<LoadLevelState>().AsSingle();
            Container.Bind<GameLoopState>().AsSingle();
        }

        private void BootstrapGame() => 
            Container.Resolve<IStateMachine<IGameState>>().Enter<BootstrapState>();
    }
}