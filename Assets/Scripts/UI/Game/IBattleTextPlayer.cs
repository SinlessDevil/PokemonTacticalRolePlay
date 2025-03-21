using UnityEngine;

namespace CodeBase.Gameplay.UI
{
    public interface IBattleTextPlayer
    {
        void PlayText(string text, Color color, Vector3 from);
        void SetRoot(Transform root);
    }
}