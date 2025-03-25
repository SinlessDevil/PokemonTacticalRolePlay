using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Window.HeroSetUpWIndow
{
    public class HorizontalLayoutSelecter : MonoBehaviour
    {
        [SerializeField] private HorizontalLayoutGroup _horizontalLayoutGroup;
        [SerializeField] private List<TargetContainer> _targetContainer;
        
        public TargetContainer GetFreeTargetContainer()
        {
            return _targetContainer.Find(x => !x.Selected);
        }
    }
}