using Code.Gameplay.Features.Statuses;
using Code.Gameplay.Features.Statuses.Applier;
using Entitas;

namespace Code.Gameplay.Features.EffectApplication.Systems
{
    public class ApplyStatusesOnTargetsSystem : IExecuteSystem
    {
        private readonly IStatusApplier _statusApplier;
        private readonly IGroup<GameEntity> _entities;

        public ApplyStatusesOnTargetsSystem(GameContext gameContext, IStatusApplier statusApplier)
        {
            _statusApplier = statusApplier;
            
            _entities = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.TargetsBuffer,
                    GameMatcher.StatusSetups));
        }
        
        public void Execute()
        {
            foreach (GameEntity entity in _entities)
            {
                foreach (int targetId in entity.TargetsBuffer)
                {
                    foreach (StatusSetup setup in entity.StatusSetups)
                    {
                        _statusApplier.ApplyStatus(setup, GetProducerId(entity), targetId);
                    }
                }
            }
        }

        private int GetProducerId(GameEntity entity)
        {
            return entity.hasProducerId ? entity.ProducerId : entity.ID;
        }
    }
}