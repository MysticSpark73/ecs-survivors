using System.Collections.Generic;
using Code.Common.Extensions;
using Code.Infrastructure.View.Registrars;
using UnityEngine;

namespace Code.Gameplay.Features.Enemies.Registrars
{
    public class EnemyRegistrar : EntityComponentRegistrar
    {
        [SerializeField] private float _speed = 1;
        [SerializeField] private float _maxHp = 3;
        [SerializeField] private float _damage = 1;

        public override void RegisterComponents()
        {
            Entity
                .AddEnemyTypeId(EnemyTypeId.Goblin)
                .AddWorldPosition(transform.position)
                .AddDirection(Vector2.zero)
                .AddSpeed(_speed)
                .AddCurrentHP(_maxHp)
                .AddMaxHP(_maxHp)
                .AddDamage(_damage)
                .AddTargetsBuffer(new List<int>(1))
                .AddRadius(.3f)
                .AddCollectTargetsInterval(.5f)
                .AddCollectTargetsTimer(0)
                .AddLayerMask(CollisionLayer.Hero.AsMask())
                .With(e => e.isEnemy = true)
                .With(e => e.isTurnedAlongDirection = true);
        }

        public override void UnregisterComponents()
        {
        }
    }
}