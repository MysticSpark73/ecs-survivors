using System.Collections.Generic;
using Code.Common.Entity;
using Code.Gameplay.Features.CharacterStats;
using Entitas;

namespace Code.Gameplay.Features.Statuses.Systems
{
    public class ApplyFreezeStatusSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _statuses;
        private List<GameEntity> _buffer = new (32);

        public ApplyFreezeStatusSystem(GameContext gameContext)
        {
            _statuses = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.ID,
                    GameMatcher.Status,
                    GameMatcher.Freeze,
                    GameMatcher.ProducerId,
                    GameMatcher.TargetId,
                    GameMatcher.EffectValue
                )
                .NoneOf(GameMatcher.Affected));
        }
            
        public void Execute()
        {
            foreach (var status in _statuses.GetEntities(_buffer))
            {
                CreateEntity.Empty()
                    .AddStatChange(Stats.Speed)
                    .AddTargetId(status.TargetId)
                    .AddProducerId(status.ProducerId)
                    .AddEffectValue(status.EffectValue)
                    .AddStatusApplierLink(status.ID);
                
                status.isAffected = true;
            }
        }
    }
}