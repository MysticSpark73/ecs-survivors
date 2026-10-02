using System.Collections.Generic;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Features.Abilities;
using Code.Gameplay.Features.Abilities.Configs;
using Code.Gameplay.StaticData;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Gameplay.Features.Armaments.Factory
{
    public class ArmamentFactory : IArmamentFactory
    {
        private const int TargetsBufferCapacity = 16;
        private readonly IIdentifierService _identifierService;
        private readonly IStaticDataService _staticDataService;

        public ArmamentFactory(IIdentifierService identifierService, IStaticDataService staticDataService)
        {
            _identifierService = identifierService;
            _staticDataService = staticDataService;
        }

        public GameEntity CreateVegetableBolt(int level, Vector3 position)
        {
            AbilityLevel abilityLevel = _staticDataService.GetAbilityLevel(AbilityId.VegetableBolt, level);
            ProjectileSetup projectileSetup = abilityLevel.ProjectileSetup;
            
            return CreateEntity.Empty()
                .AddID(_identifierService.Next())
                .With(e => e.isArmament = true)
                .AddViewPrefab(abilityLevel.ViewPrefab)
                .AddWorldPosition(position)
                .AddSpeed(projectileSetup.Speed)
                .AddEffectSetups(abilityLevel.EffectSetups)
                .AddRadius(projectileSetup.ContactRadius)
                .AddTargetsBuffer(new List<int>(TargetsBufferCapacity))
                .AddProcessedTargets(new List<int>(TargetsBufferCapacity))
                .AddTargetsLimit(projectileSetup.Pierce)
                .AddLayerMask(CollisionLayer.Enemy.AsMask())
                .With(e => e.isMovementAvailable = true)
                .With(e => e.isReadyToCollectTargets = true)
                .With(e => e.isCollectingTargetsContinuously = true)
                .With(e => e.isRotatedAlongDirection = true)
                .AddSelfDestructTimer(projectileSetup.LifeTime);
        }
    }
}