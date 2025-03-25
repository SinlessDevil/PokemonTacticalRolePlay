using System;
using UnityEngine;

namespace Window.HeroSetUpWIndow
{
    [Serializable]
    public class TargetContainer
    {
        public RectTransform RectTransform;
        [HideInInspector] public bool Selected;
    }
}