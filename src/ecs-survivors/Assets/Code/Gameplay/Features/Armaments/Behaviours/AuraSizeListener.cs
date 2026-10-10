using Code.Infrastructure.View;
using UnityEngine;

namespace Code.Gameplay.Features.Armaments.Behaviours
{
    public class AuraSizeListener : EntityDependent
    {
        [SerializeField] private Transform _container;
        private float _cachedRadius;

        private void Update()
        {
            if (Mathf.Abs(Entity.Radius - _cachedRadius) < Mathf.Epsilon) return;
            
            SetAuraScale();
        }

        private void SetAuraScale()
        {
            float scale = Entity.Radius * 2;
            _container.localScale = Vector3.one * scale;
            _cachedRadius = Entity.Radius;
        }
    }
}