using Entitas;

namespace Code.Gameplay.Features.Lifetime.Systems
{
    public class UnapplyStatusesOfDeadTargetSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _statuses;
        private readonly IGroup<GameEntity> _dead;

        public UnapplyStatusesOfDeadTargetSystem(GameContext gameContext)
        {
            _statuses = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Status,
                    GameMatcher.TargetId));
            
            _dead = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.ID,
                    GameMatcher.Dead));
        }
            
        public void Execute()
        {
            foreach (GameEntity entity in _dead)
            {
                foreach (var status in _statuses)
                {
                    if (status.TargetId == entity.ID) status.isUnapplied = true;
                }
            }
        }
    }
}