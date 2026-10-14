using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.LevelUp.Systems
{
    public class FinalizeProcessedLevelUpsSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;
        private readonly List<Entity> _buffer = new();

        public FinalizeProcessedLevelUpsSystem(GameContext gameContext)
        {
            _entities = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.LevelUp,
                GameMatcher.ArmamentProcessed));
        }

        public void Execute()
        {
            foreach (GameEntity entity in _entities)
            {
                entity.isDestroyed = true;
            }
        }
    }
}