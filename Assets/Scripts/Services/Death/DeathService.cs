using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Logic.Heroes;
using Services.Battle;
using Services.HeroRegistry;
using UnityEngine;

namespace Services.Death
{
    public class DeathService : IDeathService
    {
        private const float DefaultDestroyTime = 3;
        
        private readonly IHeroRegistry _heroRegistry;
        private readonly IBattleFinisher _battleFinisher;
        
        public DeathService(IHeroRegistry heroRegistry, 
            IBattleFinisher battleFinisher)
        {
            _heroRegistry = heroRegistry;
            _battleFinisher = battleFinisher;
        }

        public void ProcessDeadHeroes()
        {
            foreach (string id in _heroRegistry.AllIds)
            {
                HeroBehaviour hero = _heroRegistry.GetHero(id);
                if (!hero.IsDead)
                    continue;

                _heroRegistry.Unregister(hero.Id);
                
                hero.Animator.PlayDeath();
                WaitDeathAsync(hero).Forget();
            }
        }

        private async UniTask WaitDeathAsync(HeroBehaviour hero)
        {
            await Task.Delay((int)DefaultDestroyTime * 1000);
            Object.Destroy(hero.gameObject);
            
            _battleFinisher.HandleFinish();
        }  
    }
}