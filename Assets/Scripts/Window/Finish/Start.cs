using UnityEngine;

namespace Window.Finish
{
    public class Start : MonoBehaviour
    {
        [SerializeField] private RectTransform _filledStar;
        [SerializeField] private RectTransform _emptyStar;
        [SerializeField] private RectTransform _target;
        [SerializeField] private Animator _animator;
        
        public RectTransform FilledStar => _filledStar;
        public RectTransform Target => _target;
        
        public void Filled()
        {
            _filledStar.gameObject.SetActive(true);
        }

        public void Empty()
        {
            _filledStar.gameObject.SetActive(false);
        }

        public void Play()
        {
            _animator.CrossFade("End", 0.25f);
        }
    }
}