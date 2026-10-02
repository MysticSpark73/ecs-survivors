using System;
using System.Collections.Generic;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Features.Effects;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Gameplay.Features.Enemies.Factory
{
    public class EnemyFactory : IEnemyFactory
    {
        private const string EnemyViewPath = "Gameplay/Enemies/Goblins/Torch/goblin_torch_blue";
        
        private readonly IIdentifierService _identifierService;

        public EnemyFactory(IIdentifierService identifierService)
        {
            _identifierService = identifierService;
        }
        
        public GameEntity CreateEnemy(EnemyTypeId typeId, Vector3 position)
        {
            switch (typeId)
            {
                case EnemyTypeId.Goblin:
                    return CreateGoblin(position);
                default:
                    throw new NotImplementedException($"Enemy with typeId {typeId} does not exist!");
            }
        }

        private GameEntity CreateGoblin(Vector3 position)
        {
            return CreateEntity.Empty()
                .AddID(_identifierService.Next())
                .AddEnemyTypeId(EnemyTypeId.Goblin)
                .AddWorldPosition(position)
                .AddDirection(Vector2.zero)
                .AddSpeed(1)
                .AddCurrentHP(3)
                .AddMaxHP(3)
                .AddEffectSetups(new List<EffectSetup>()
                    { new EffectSetup() 
                        { EffectTypeId = EffectTypeId.Damage, Value = 1 } 
                    })
                .AddTargetsBuffer(new List<int>(1))
                .AddRadius(.3f)
                .AddCollectTargetsInterval(.5f)
                .AddCollectTargetsTimer(0)
                .AddLayerMask(CollisionLayer.Hero.AsMask())
                .AddViewPath(EnemyViewPath)
                .With(e => e.isEnemy = true)
                .With(e => e.isTurnedAlongDirection = true)
                .With(e => e.isMovementAvailable = true);
        }
    }
}