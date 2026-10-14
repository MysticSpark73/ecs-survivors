using System.Collections.Generic;
using Code.Gameplay.Features.Abilities.Upgrade;
using Code.Gameplay.Features.Armaments.Factory;
using Entitas;

namespace Code.Gameplay.Features.Abilities.Systems
{
    public class GarlicAuraAbilitySystem : IExecuteSystem
    {
        private readonly IArmamentFactory _armamentFactory;
        private readonly IAbilityUpgradeService _abilityUpgradeService;
        
        private readonly IGroup<GameEntity> _abilities;
        private readonly IGroup<GameEntity> _heroes;
        
        private readonly List<GameEntity> _buffer = new (8);

        public GarlicAuraAbilitySystem(GameContext gameContext, IArmamentFactory armamentFactory, IAbilityUpgradeService abilityUpgradeService)
        {
            _armamentFactory = armamentFactory;
            _abilityUpgradeService = abilityUpgradeService;

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
                    int level = _abilityUpgradeService.GetAbilityLevel(AbilityId.GarlicAura);
                    
                    _armamentFactory.CreateEffectAura(AbilityId.GarlicAura, hero.ID, level);

                    ability.isActive = true;
                }
            }
        }
    }
}