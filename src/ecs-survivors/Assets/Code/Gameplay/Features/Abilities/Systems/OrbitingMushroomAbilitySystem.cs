using System.Collections.Generic;
using Code.Common.Extensions;
using Code.Gameplay.Features.Abilities.Configs;
using Code.Gameplay.Features.Armaments.Factory;
using Code.Gameplay.Features.Cooldowns;
using Code.Gameplay.StaticData;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Abilities.Systems
{
    public class OrbitingMushroomAbilitySystem : IExecuteSystem
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IArmamentFactory _armamentFactory;

        private readonly List<GameEntity> _buffer = new List<GameEntity>(8);
        private readonly IGroup<GameEntity> _abilities;
        private readonly IGroup<GameEntity> _heroes;

        public OrbitingMushroomAbilitySystem(GameContext gameContext, IStaticDataService staticDataService, IArmamentFactory armamentFactory)
        {
            _staticDataService = staticDataService;
            _armamentFactory = armamentFactory;

            _abilities = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.OrbitingMushroomAbility,
                    GameMatcher.CooldownUp));

            _heroes = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Hero,
                    GameMatcher.WorldPosition));
        }
            
        public void Execute()
        {
            foreach (GameEntity ability in _abilities.GetEntities(_buffer))
            {
                foreach (GameEntity hero in _heroes)
                {
                    AbilityLevel abilityLevel = _staticDataService.GetAbilityLevel(AbilityId.OrbitingMushroom, 1);
                    int projectileCount = abilityLevel.ProjectileSetup.ProjectileCount;

                    for (int i = 0; i < projectileCount; i++)
                    {
                        var phase = 2 * Mathf.PI * i / projectileCount;
                        
                        CreateOrbitingMushroomProjectile(hero, phase, 1);
                    }
                    
                    ability.PutOnCooldown(abilityLevel.Cooldown);
                }
            }
        }

        private void CreateOrbitingMushroomProjectile(GameEntity hero, float phase, int level)
        {
            _armamentFactory.CreateOrbitingMushroom(level, hero.WorldPosition + Vector3.up, phase)
                .AddProducerId(hero.ID)
                .AddOrbitCenterPosition(hero.WorldPosition)
                .AddOrbitFollowTarget(hero.ID)
                .With(e => e.isMoving = true);
        }
    }
}