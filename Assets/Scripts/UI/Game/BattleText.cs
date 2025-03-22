using UnityEngine;
using UnityEngine.UI;

namespace UI.Game
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