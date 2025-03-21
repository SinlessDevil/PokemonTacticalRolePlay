using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Gameplay.UI
{
    public class BattleText : MonoBehaviour
    {
        public Text Text;

        public void Initialize(string text, Color color)
        {
            Text.text = text;
            Text.color = color;
        }
    }
}