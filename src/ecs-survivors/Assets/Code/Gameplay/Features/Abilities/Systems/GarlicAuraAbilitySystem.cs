using System.Collections.Generic;
using Code.Gameplay.Features.Armaments.Factory;
using Entitas;

namespace Code.Gameplay.Features.Abilities.Systems
{
    public class GarlicAuraAbilitySystem : IExecuteSystem
    {
        private readonly IArmamentFactory _armamentFactory;
        private readonly IGroup<GameEntity> _abilities;
        private readonly IGroup<GameEntity> _heroes;
        
        private readonly List<GameEntity> _buffer = new (8);

        public GarlicAuraAbilitySystem(GameContext gameContext, IArmamentFactory armamentFactory)
        {
            _armamentFactory = armamentFactory;
            
            _abilities = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.GarlicAuraAbility)
                .NoneOf(GameMatcher.Active));

            _heroes = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Hero,
                    GameMatcher.ID));
        }

        public void Execute()
        {
            foreach (GameEntity ability in _abilities.GetEntities(_buffer))
            {
                foreach (GameEntity hero in _heroes)
                {
                    _armamentFactory.CreateEffectAura(AbilityId.GarlicAura, hero.ID, 1);

                    ability.isActive = true;
                }
            }
        }
    }
}