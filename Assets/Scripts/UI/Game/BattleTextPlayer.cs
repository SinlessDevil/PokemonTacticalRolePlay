using UnityEngine;
using Zenject;

namespace UI.Game
{
    public class BattleTextPlayer : IBattleTextPlayer, IInitializable
    {
        private const string BattleTextPath = "UI/BattleTexts/BattleText";

        public Transform TextRoot { get; set; }

        public int RandomX = 50;
        public int RandomY = 50;

        private BattleText _battleTextPrefab;

        public void Initialize()
        {
            _battleTextPrefab = Resources.Load<BattleText>(BattleTextPath);
        }

        public void SetRoot(Transform root) =>
            TextRoot = root;

        public void PlayText(string text, Color color, Vector3 from)
        {
            Vector3 position = Camera.main.WorldToScreenPoint(from);

            position += new Vector3(Random.Range(-RandomX, RandomX), Random.Range(-RandomY, RandomY));

            BattleText battleText = Object.Instantiate(_battleTextPrefab, position, Quaternion.identity, TextRoot);
            battleText.Initialize(text, color);
        }
    }
}