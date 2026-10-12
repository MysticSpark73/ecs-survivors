using Entitas;

namespace Code.Gameplay.Features.Loot.Systems
{
    public class PullTowardsHeroSystem : IExecuteSystem
    {
        private const float PullSpeed = 4;
        
        private readonly IGroup<GameEntity> _pullable;
        private readonly IGroup<GameEntity> _heroes;

        public PullTowardsHeroSystem(GameContext gameContext)
        {
            _pullable = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.Pulling));
            
            _heroes = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.Hero));
        }
            
        public void Execute()
        {
            foreach (var hero in _heroes)
            {
                foreach (var pullable in _pullable)
                {
                    pullable.ReplaceDirection((hero.WorldPosition - pullable.WorldPosition).normalized);
                    pullable.ReplaceSpeed(PullSpeed);
                    pullable.isMoving = true;
                    pullable.isMovementAvailable = true;
                }
            }
        }
    }
}