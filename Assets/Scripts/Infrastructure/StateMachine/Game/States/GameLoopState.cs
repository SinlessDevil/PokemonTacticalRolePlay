using Cysharp.Threading.Tasks;
using Services.Battle;
using Services.Factories.UIFactory;
using Services.HeroRegistry;
using Services.Input;
using Services.Levels;
using Services.LocalProgress;
using Services.Provides.Widgets;
using Services.Timer;
using Services.Window;
using UI.Game;
using UnityEngine;
using Window;
using Window.HeroSetUpWindow;

namespace Infrastructure.StateMachine.Game.States
{
    public class GameLoopState : IState, IGameState, IUpdatable
    {
        private readonly IInputService _inputService;
        private readonly IWidgetProvider _widgetProvider;
        private readonly ILevelService _levelService;
        private readonly ILevelLocalProgressService _levelLocalProgressService;
        private readonly ITimeService _timeService;
        private readonly IWindowService _windowService;
        private readonly IUIFactory _uiFactory;
        private readonly IBattleTextPlayer _battleTextPlayer;
        private readonly IHeroRegistry _heroRegistry;
        private readonly IBattleStarter _battleStarter;
        private readonly ILoadingCurtain _loadingCurtain;

        public GameLoopState(
            IInputService inputService,
            IWidgetProvider widgetProvider,
            ILevelService levelService,
            ILevelLocalProgressService levelLocalProgressService,
            ITimeService timeService,
            IWindowService windowService,
            IUIFactory uiFactory,
            IBattleTextPlayer battleTextPlayer,
            IHeroRegistry heroRegistry,
            IBattleStarter battleStarter,
            ILoadingCurtain loadingCurtain)
        {
            _inputService = inputService;
            _widgetProvider = widgetProvider;
            _levelService = levelService;
            _levelLocalProgressService = levelLocalProgressService;
            _timeService = timeService;
            _windowService = windowService;
            _uiFactory = uiFactory;
            _battleTextPlayer = battleTextPlayer;
            _heroRegistry = heroRegistry;
            _battleStarter = battleStarter;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter()
        {
            var slotSetupBehavior = Object.FindObjectOfType<SlotSetupBehaviour>();
            _battleStarter.SetUpSlotSetup(slotSetupBehavior);
            
            _battleTextPlayer.SetRoot(_uiFactory.UIRoot);
            
            WaitLoadingToShowHeroSetUpWindow().Forget();
        }

        private async UniTask WaitLoadingToShowHeroSetUpWindow()
        {
            await UniTask.WaitUntil(() => _loadingCurtain.IsActive == false);
            
            var heroSetUpWindow = _windowService
                .Open(WindowTypeId.HeroSetUpWindow)
                .GetComponent<HeroSetUpWindow>();

            heroSetUpWindow.Initialize();
        }
        
        public void Update()
        {
            
        }

        public void Exit()
        {
            _battleStarter.CleanUp();
            _heroRegistry.CleanUp();

            _inputService.CleanUp();
            _widgetProvider.CleanUpPool();
            _levelService.CleanUp();
            _levelLocalProgressService.CleanUp();

            _timeService.ResetTimer();
        }
    }
}