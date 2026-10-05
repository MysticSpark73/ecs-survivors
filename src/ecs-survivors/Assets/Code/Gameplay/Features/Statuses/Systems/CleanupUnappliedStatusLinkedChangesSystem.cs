using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Statuses.Systems
{
    public class CleanupUnappliedStatusLinkedChangesSystem : ICleanupSystem
    {
        private readonly GameContext _gameContext;
        
        private readonly IGroup<GameEntity> _statuses;
        private readonly List<GameEntity> _buffer = new (32);
    
        public CleanupUnappliedStatusLinkedChangesSystem (GameContext gameContext)
        {
            _gameContext = gameContext;
            _statuses = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.ID,
                GameMatcher.Status,
                GameMatcher.Unapplied));
        }
    
        public void Cleanup()
        {
            foreach (GameEntity status in _statuses.GetEntities(_buffer))
            {
                foreach (GameEntity entity in _gameContext.GetEntitiesWithStatusApplierLink(status.ID))
                {
                    entity.isDestroyed = true;
                }
            }
        }
    }
}