using Code.Common.Extensions;
using Code.Infrastructure.View.Registrars;
using UnityEngine;

namespace Code.Gameplay.Features.Hero.Registrars
{
    public class HeroRegistrar : EntityComponentRegistrar
    {
        [SerializeField] private float _speed = 2;
        [SerializeField] private float _maxHp = 100;

        public override void RegisterComponents()
        {
            Entity
                .AddWorldPosition(transform.position)
                .AddDirection(Vector2.zero)
                .AddSpeed(_speed)
                .AddCurrentHP(_maxHp)
                .AddMaxHP(_maxHp)
                .With(e => e.isHero = true)
                .With(e => e.isTurnedAlongDirection = true);
        }

        public override void UnregisterComponents()
        {
        }
    }
}