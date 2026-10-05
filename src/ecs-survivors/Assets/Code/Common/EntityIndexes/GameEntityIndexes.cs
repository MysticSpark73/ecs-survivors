using System.Collections.Generic;
using Code.Gameplay.Features.CharacterStats;
using Code.Gameplay.Features.CharacterStats.Indexing;
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
        public const string StatChanges = "StatChanges";
        
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
            
            _gameContext.AddEntityIndex(new EntityIndex<GameEntity, StatKey>(
                name:  StatChanges,
                group: _gameContext.GetGroup(GameMatcher.AllOf(
                    GameMatcher.StatChange,
                    GameMatcher.TargetId)),
                getKey: GetTargetStatKey,
                comparer: new StatKeyEqualityComparer()));
        }

        private StatusKey GetTargetStatusKey(GameEntity entity, IComponent component)
        {
            return new StatusKey(
                (component as TargetId)?.Value ?? entity.TargetId,
                (component as StatusTypeIdComponent)?.Value ?? entity.StatusTypeId);
        }

        private StatKey GetTargetStatKey(GameEntity entity, IComponent component)
        {
            return new StatKey(
                (component as TargetId)?.Value ?? entity.TargetId,
                (component as StatChange)?.Value ?? entity.StatChange);
        }
    }

    public static class ContextIndexesExtensions
    {
        public static HashSet<GameEntity> TargetStatusesOfType(this GameContext context, StatusTypeId statusTypeId, int targetId)
        {
            return ((EntityIndex<GameEntity, StatusKey>) context.GetEntityIndex(GameEntityIndexes.StatusesOfType))
                ?.GetEntities(new StatusKey(targetId, statusTypeId));
        }

        public static HashSet<GameEntity> TargetStatChanges(this GameContext context, Stats stat, int targetId)
        {
            return ((EntityIndex<GameEntity, StatKey>) context.GetEntityIndex(GameEntityIndexes.StatChanges))
                ?.GetEntities(new StatKey(targetId, stat));
        }
    } 
}