using System;
using CodeBase.Gameplay.Battle;
using CodeBase.Gameplay.HeroRegistry;
using CodeBase.Gameplay.UI;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class BattleAreaInstaller : MonoInstaller, IInitializable, IDisposable
    {
        public SlotSetupBehaviour SlotSetup;
        public Transform TextRoot;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<BattleAreaInstaller>().FromInstance(this).AsSingle();
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