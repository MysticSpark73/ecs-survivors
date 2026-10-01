using System.Collections.Generic;
using System.Linq;
using Code.Common.Extensions;
using Code.Gameplay.Features.Armaments.Factory;
using Code.Gameplay.Features.Cooldowns;
using Code.Gameplay.StaticData;
using Entitas;

namespace Code.Gameplay.Features.Abilities.Systems
{
    public class VegetableBoltAbilitySystem : IExecuteSystem
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IArmamentFactory _armamentFactory;

        private readonly List<GameEntity> _buffer = new List<GameEntity>(8);
        private readonly IGroup<GameEntity> _abilities;
        private readonly IGroup<GameEntity> _heroes;
        private readonly IGroup<GameEntity> _enemies;

        public VegetableBoltAbilitySystem(GameContext gameContext, IStaticDataService staticDataService, IArmamentFactory armamentFactory)
        {
            _staticDataService = staticDataService;
            _armamentFactory = armamentFactory;

            _abilities = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.VegetableBoltAbility,
                    GameMatcher.CooldownUp));

            _heroes = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Hero,
                    GameMatcher.WorldPosition));
            
            _enemies = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Enemy,
                    GameMatcher.WorldPosition));
        }
            
        public void Execute()
        {
            foreach (GameEntity ability in _abilities.GetEntities(_buffer))
            {
                foreach (GameEntity hero in _heroes)
                {
                    if (_enemies.count <= 0) continue;
                    
                    _armamentFactory.CreateVegetableBolt(1, hero.WorldPosition)
                        .ReplaceDirection((FirstAvailableTarget().WorldPosition - hero.WorldPosition).normalized)
                        .With(e => e.isMoving = true);
                        
                    ability.PutOnCooldown(_staticDataService.GetAbilityLevel(AbilityId.VegetableBolt, 1).Cooldown);
                }
            }
        }

        private GameEntity FirstAvailableTarget()
        {
            return _enemies.AsEnumerable().First();
        }
    }
}