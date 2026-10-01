using Entitas;

namespace Code.Gameplay.Features.Armaments.Systems
{
    public class MarkProcessedOnTargetLimitExceededSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _armaments;
    
        public MarkProcessedOnTargetLimitExceededSystem(GameContext gameContext)
        {
            _armaments = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Armament,
                    GameMatcher.TargetsLimit,
                    GameMatcher.ProcessedTargets));
        }
            
        public void Execute()
        {
            foreach (var armament in _armaments)
            {
                if (armament.ProcessedTargets.Count >= armament.TargetsLimit) 
                    armament.isArmamentProcessed = true;
            }
        }
    }
}