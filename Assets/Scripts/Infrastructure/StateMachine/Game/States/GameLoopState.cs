using Services.Factories.UIFactory;
using Services.HeroRegistry;
using Services.Input;
using Services.Levels;
using Services.LocalProgress;
using Services.Provides.Widgets;
using Services.Timer;
using Services.Window;
using UI.Game;
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

        public GameLoopState(
            IInputService inputService,
            IWidgetProvider widgetProvider,
            ILevelService levelService,
            ILevelLocalProgressService levelLocalProgressService,
            ITimeService timeService,
            IWindowService windowService,
            IUIFactory uiFactory,
            IBattleTextPlayer battleTextPlayer,
            IHeroRegistry heroRegistry)
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
        }
        
        public void Enter()
        {
            _battleTextPlayer.SetRoot(_uiFactory.UIRoot);
            
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
            _heroRegistry.CleanUp();
            
            _inputService.Cleanup();
            _widgetProvider.CleanupPool();
            _levelService.Cleanup();
            _levelLocalProgressService.Cleanup();
            
            _timeService.ResetTimer();
        }
    }
}