using Services.Finish;
using Services.HeroRegistry;

namespace Services.Battle
{
    public class BattleFinisher : IBattleFinisher
    {
        private readonly IHeroRegistry _heroRegistry;
        private readonly IFinishService _finishService;
        
        public BattleFinisher(IHeroRegistry heroRegistry, 
            IFinishService finishService)
        {
            _heroRegistry = heroRegistry;
            _finishService = finishService;
        }
        
        public void HandleFinish()
        {
            if (HasWinPlayer())
            {
                _finishService.Win();
                return;
            }

            if (HasWinEnemy())
            {
                _finishService.Lose();
                return;
            }
        }
        
        private bool HasWinEnemy() => _heroRegistry.PlayerTeam.Count == 0;
        private bool HasWinPlayer() => _heroRegistry.EnemyTeam.Count == 0;
    }
}