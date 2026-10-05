using Code.Common.Extensions;
using Entitas;

namespace Code.Gameplay.Features.CharacterStats.Systems
{
    public class ApplySpeedFromStatsSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;
    
        public ApplySpeedFromStatsSystem(GameContext gameContext)
        {
            _entities = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.StatModifiers,
                    GameMatcher.BaseStats,
                    GameMatcher.Speed));
        }
            
        public void Execute()
        {
            foreach (var entity in _entities)
            {
                entity.ReplaceSpeed(CalculateSpeedModifier(entity).ZeroIfNegative());
            }
        }

        private static float CalculateSpeedModifier(GameEntity entity)
        {
            return entity.BaseStats[Stats.Speed] + entity.StatModifiers[Stats.Speed];
        }
    }
}