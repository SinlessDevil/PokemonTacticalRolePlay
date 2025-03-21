using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Gameplay.Heroes
{
    public class HeroHP : MonoBehaviour
    {
        public HeroBehaviour Hero;
        public Slider HPBar;

        private void Update()
        {
            if (Hero != null && Hero.State != null)
                HPBar.value = Hero.State.CurrentHp / Hero.State.MaxHp;
        }
    }
}