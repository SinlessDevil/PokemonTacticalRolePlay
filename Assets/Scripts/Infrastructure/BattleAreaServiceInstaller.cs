using Services.Battle;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class BattleAreaServiceInstaller : MonoInstaller, IInitializable
    {
        public SlotSetupBehaviour SlotSetup;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<BattleAreaServiceInstaller>().FromInstance(this).AsSingle();
        }

        public void Initialize()
        {
            Container.Resolve<IBattleStarter>().SetUpSlotSetup(SlotSetup);
        }
    }
}