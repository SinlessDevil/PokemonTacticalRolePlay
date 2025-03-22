using System;
using Services.Battle;
using Services.HeroRegistry;
using UI.Game;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class BattleAreaServiceInstaller : MonoInstaller, IInitializable, IDisposable
    {
        public SlotSetupBehaviour SlotSetup;
        public Transform TextRoot;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<BattleAreaServiceInstaller>().FromInstance(this).AsSingle();
            Container.Bind<IBattleStarter>().To<BattleStarter>().AsSingle();
        }

        public void Initialize()
        {
            Container.Resolve<IBattleTextPlayer>().SetRoot(TextRoot);
            Container.Resolve<IBattleStarter>().StartRandomBattle(SlotSetup);
        }

        public void Dispose()
        {
            Container.Resolve<IHeroRegistry>().CleanUp();
        }
    }
}