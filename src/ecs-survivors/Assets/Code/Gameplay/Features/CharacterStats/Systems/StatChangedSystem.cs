using Code.Common.EntityIndexes;
using Entitas;

namespace Code.Gameplay.Features.CharacterStats.Systems
{
    public class StatChangedSystem : IExecuteSystem
    {
        private readonly GameContext _gameContext;
        
        private readonly IGroup<GameEntity> _statsOwners;

        public StatChangedSystem(GameContext gameContext)
        {
            _gameContext = gameContext;

            _statsOwners = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.ID,
                    GameMatcher.BaseStats,
                    GameMatcher.StatModifiers));
        }

        public void Execute()
        {
            foreach (GameEntity owner in _statsOwners)
            {
                foreach (Stats stat in owner.BaseStats.Keys)
                {
                    owner.StatModifiers[stat] = 0;
                    foreach (GameEntity statChange in _gameContext.TargetStatChanges(stat, owner.ID))
                    {
                        owner.StatModifiers[stat] += statChange.EffectValue;
                    }
                }
            }
        }
    }
}