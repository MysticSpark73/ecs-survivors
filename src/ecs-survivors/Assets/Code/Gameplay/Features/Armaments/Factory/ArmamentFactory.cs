using System.Collections.Generic;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Features.Abilities;
using Code.Gameplay.Features.Abilities.Configs;
using Code.Gameplay.Features.Enchants;
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

            return CreateProjectileEntity(position, abilityLevel, projectileSetup)
                .AddParentAbility(AbilityId.VegetableBolt)
                .With(e => e.isRotatedAlongDirection = true);
        }
        
        public GameEntity CreateOrbitingMushroom(int level, Vector3 position, float phase)
        {
            AbilityLevel abilityLevel = _staticDataService.GetAbilityLevel(AbilityId.OrbitingMushroom, level);
            ProjectileSetup projectileSetup = abilityLevel.ProjectileSetup;

            return CreateProjectileEntity(position, abilityLevel, projectileSetup)
                .AddParentAbility(AbilityId.OrbitingMushroom)
                .AddOrbitPhase(phase)
                .AddOrbitRadius(projectileSetup.OrbitRadius);
        }

        public GameEntity CreateEffectAura(AbilityId parentAbilityId, int producerId, int level)
        {
            AbilityLevel abilityLevel = _staticDataService.GetAbilityLevel(AbilityId.GarlicAura, level);
            AuraSetup auraSetup = abilityLevel.AuraSetup;

            return CreateEntity.Empty()
                .AddID(_identifierService.Next())
                .AddProducerId(producerId)
                .AddParentAbility(parentAbilityId)
                .AddViewPrefab(abilityLevel.ViewPrefab)
                .AddRadius(auraSetup.Radius)
                .With(e => e.AddEffectSetups(abilityLevel.EffectSetups), when: !abilityLevel.EffectSetups.IsNullOrEmpty())
                .With(e => e.AddStatusSetups(abilityLevel.StatusSetups), when: !abilityLevel.StatusSetups.IsNullOrEmpty())
                .AddTargetsBuffer(new List<int>(TargetsBufferCapacity))
                .AddCollectTargetsInterval(auraSetup.Interval)
                .AddCollectTargetsTimer(0)
                .AddLayerMask(CollisionLayer.Enemy.AsMask())
                .AddWorldPosition(Vector3.zero)
                .With(e => e.isFollowingProducer =  true);
        }

        public GameEntity CreateExplosion(int producerId, Vector3 position)
        {
            EnchantConfig config = _staticDataService.GetEnchantConfig(EnchantTypeId.ExplosiveArmaments);

            return CreateEntity.Empty()
                .AddID(_identifierService.Next())
                .AddLayerMask(CollisionLayer.Enemy.AsMask())
                .AddProducerId(producerId)
                .AddRadius(config.Radius)
                .AddViewPrefab(config.ViewPrefab)
                .With(e => e.AddEffectSetups(config.EffectSetups), when: !config.EffectSetups.IsNullOrEmpty())
                .With(e => e.AddStatusSetups(config.StatusSetups), when: !config.StatusSetups.IsNullOrEmpty())
                .AddTargetsBuffer(new List<int>(TargetsBufferCapacity))
                .AddWorldPosition(position)
                .With(e => e.isReadyToCollectTargets = true)
                .AddSelfDestructTimer(1);
        }

        private GameEntity CreateProjectileEntity(Vector3 position, AbilityLevel abilityLevel, ProjectileSetup projectileSetup)
        {
            return CreateEntity.Empty()
                .AddID(_identifierService.Next())
                .With(e => e.isArmament = true)
                .AddViewPrefab(abilityLevel.ViewPrefab)
                .AddWorldPosition(position)
                .AddSpeed(projectileSetup.Speed)
                .With(e =>e.AddEffectSetups(abilityLevel.EffectSetups), when: !abilityLevel.EffectSetups.IsNullOrEmpty())
                .With(e =>e.AddStatusSetups(abilityLevel.StatusSetups), when: !abilityLevel.StatusSetups.IsNullOrEmpty())
                .AddRadius(projectileSetup.ContactRadius)
                .AddTargetsBuffer(new List<int>(TargetsBufferCapacity))
                .AddProcessedTargets(new List<int>(TargetsBufferCapacity))
                .With(e => e.AddTargetsLimit(projectileSetup.Pierce), when: projectileSetup.Pierce > 0)
                .AddLayerMask(CollisionLayer.Enemy.AsMask())
                .With(e => e.isMovementAvailable = true)
                .With(e => e.isReadyToCollectTargets = true)
                .With(e => e.isCollectingTargetsContinuously = true)
                .AddSelfDestructTimer(projectileSetup.LifeTime);
        }
    }
}