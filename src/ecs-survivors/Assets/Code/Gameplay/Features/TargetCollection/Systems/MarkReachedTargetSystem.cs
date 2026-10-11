using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.TargetCollection.Systems
{
    public class MarkReachedTargetSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;
        private readonly List<GameEntity> _buffer = new(128);

        public MarkReachedTargetSystem(GameContext gameContext)
        {
            _entities = gameContext.GetGroup(GameMatcher
                .AllOf(GameMatcher.TargetsBuffer)
                .NoneOf(GameMatcher.ReachedTarget));
        }
            
        public void Execute()
        {
            foreach (var entity in _entities.GetEntities(_buffer))
            {
                if (entity.TargetsBuffer.Count > 0) entity.isReachedTarget = true;
            }
        }
    }
}