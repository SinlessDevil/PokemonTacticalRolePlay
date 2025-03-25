using Services.Finish;
using Services.HeroRegistry;

namespace Services.Battle
{
    public class BattleFinisher : IBattleFinisher
    {
        private readonly IHeroRegistry _heroRegistry;
        private readonly IFinishService _finishService;
        
        private bool _isFinishing = false;
        
        public BattleFinisher(
            IHeroRegistry heroRegistry, 
            IFinishService finishService)
        {
            _heroRegistry = heroRegistry;
            _finishService = finishService;
        }
        
        public void HandleFinish()
        {
            if(_isFinishing)
                return;
            
            if (HasWinPlayer())
            {
                _isFinishing = true;
                _finishService.Win();
                return;
            }

            if (HasWinEnemy())
            {
                _isFinishing = true;
                _finishService.Lose();
                return;
            }
        }

        public void CleanUp()
        {
            _isFinishing = false;
        }
        
        private bool HasWinEnemy() => _heroRegistry.PlayerTeam.Count == 0;
        private bool HasWinPlayer() => _heroRegistry.EnemyTeam.Count == 0;
    }
}