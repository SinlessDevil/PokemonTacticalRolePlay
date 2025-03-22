using UnityEngine;
using UnityEngine.UI;

namespace Logic.Heroes
{
    public class HeroInitiative : MonoBehaviour
    {
        public HeroBehaviour Hero;
        public Slider InitiativeBar;

        private void Update()
        {
            if (Hero != null && Hero.State != null)
                InitiativeBar.value = Hero.State.CurrentInitiative / Hero.State.MaxInitiative;
        }
    }
}