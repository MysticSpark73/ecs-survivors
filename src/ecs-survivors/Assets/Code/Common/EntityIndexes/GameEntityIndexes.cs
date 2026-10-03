using System.Collections.Generic;
using Code.Gameplay.Features.Effects;
using Code.Gameplay.Features.Statuses;
using Code.Gameplay.Features.Statuses.Indexes;
using Entitas;
using Zenject;

namespace Code.Common.EntityIndexes
{
    public class GameEntityIndexes : IInitializable
    {
        public const string StatusesOfType = "StatusesOfType";
        private readonly GameContext _gameContext;

        public GameEntityIndexes(GameContext gameContext)
        {
            _gameContext = gameContext;
        }
        
        public void Initialize()
        {
            _gameContext.AddEntityIndex(new EntityIndex<GameEntity, StatusKey>(
               name:  StatusesOfType,
               group: _gameContext.GetGroup(GameMatcher.AllOf(
                   GameMatcher.Status,
                   GameMatcher.StatusTypeId,
                   GameMatcher.TargetId,
                   GameMatcher.Duration,
                   GameMatcher.StatusTimeLeft)),
               getKey: GetTargetStatusKey,
               comparer: new StatusKeyEqualityComparer()));
        }

        private StatusKey GetTargetStatusKey(GameEntity entity, IComponent component)
        {
            return new StatusKey(
                (component as TargetId)?.Value ?? entity.TargetId,
                (component as StatusTypeIdComponent)?.Value ?? entity.StatusTypeId);
        }
    }

    public static class ContextIndexesExtensions
    {
        public static HashSet<GameEntity> TargetStatusesOfType(this GameContext context, StatusTypeId statusTypeId, int targetId)
        {
            return ((EntityIndex<GameEntity, StatusKey>)context.GetEntityIndex(GameEntityIndexes.StatusesOfType))
                ?.GetEntities(new StatusKey(targetId, statusTypeId));
        }
    } 
}