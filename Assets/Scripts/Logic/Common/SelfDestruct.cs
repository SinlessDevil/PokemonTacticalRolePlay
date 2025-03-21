using UnityEngine;

namespace CodeBase.Gameplay.Common
{
    public class SelfDestruct : MonoBehaviour
    {
        public float FuseSeconds = 0.1f;
        private float _destroyTime = 0.0f;

        private void Start()
        {
            _destroyTime = Time.time + FuseSeconds;
        }

        public void SetFuseTime(float time) =>
            FuseSeconds = time;

        private void Update()
        {
            if (Time.time > _destroyTime)
                Destroy(gameObject);
        }
    }
}