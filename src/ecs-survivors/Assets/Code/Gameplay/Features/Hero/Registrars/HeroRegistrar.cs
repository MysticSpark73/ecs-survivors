using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Features.Hero.Behaviours;
using UnityEngine;

namespace Code.Gameplay.Features.Hero.Registrars
{
    public class HeroRegistrar : MonoBehaviour
    {
        [SerializeField] private float _speed = 2;
        [SerializeField] private HeroAnimator _heroAnimator;
        
        private GameEntity _entity;

        private void Awake()
        {
            _entity = CreateEntity.
                Empty()
                .AddTransform(transform)
                .AddWorldPosition(transform.position)
                .AddDirection(Vector2.zero)
                .AddSpeed(_speed)
                .AddHeroAnimator(_heroAnimator)
                .AddSpriteRenderer(_heroAnimator.SpriteRenderer)
                .With(e => e.isHero = true)
                .With(e => e.isTurnedAlongDirection = true);
        }
    }
}