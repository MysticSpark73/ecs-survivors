using Entitas;

namespace Code.Gameplay.Features.Movement.Systems
{
    public class OrbitCenterFollowSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;
        private readonly IGroup<GameEntity> _targets;

        public OrbitCenterFollowSystem(GameContext gameContext)
        {
            _entities = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.OrbitCenterPosition,
                    GameMatcher.OrbitFollowTarget));

            _targets = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.ID,
                    GameMatcher.WorldPosition));
        }

        public void Execute()
        {
            foreach (var entity in _entities)
            {
                foreach (var target in _targets)
                {
                    if (entity.OrbitFollowTarget == target.ID)
                    {
                        entity.ReplaceOrbitCenterPosition(target.WorldPosition);
                    }
                }
            }
        }
    }
}