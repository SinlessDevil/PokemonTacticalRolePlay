using UnityEngine;

namespace Window.Finish
{
    public class Start : MonoBehaviour
    {
        [SerializeField] private RectTransform _filledStar;
        [SerializeField] private RectTransform _emptyStar;
        
        public RectTransform FilledStar => _filledStar;
        
        public void Filled()
        {
            _filledStar.gameObject.SetActive(true);
        }

        public void Empty()
        {
            _filledStar.gameObject.SetActive(false);
        }
    }
}