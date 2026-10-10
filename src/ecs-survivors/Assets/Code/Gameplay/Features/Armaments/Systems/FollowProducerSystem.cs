using Entitas;

namespace Code.Gameplay.Features.Armaments.Systems
{
    public class FollowProducerSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _followers;
        private readonly IGroup<GameEntity> _producers;
    
        public FollowProducerSystem(GameContext gameContext)
        {
            _followers = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.FollowingProducer,
                    GameMatcher.ProducerId,
                    GameMatcher.WorldPosition));
                
            _producers = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.ID,
                    GameMatcher.WorldPosition));
        }
            
        public void Execute()
        {
            foreach (var follower in _followers)
            {
                foreach (var producer in _producers)
                {
                    if (follower.ProducerId == producer.ID)
                    {
                        follower.ReplaceWorldPosition(producer.WorldPosition);
                    }
                }
            }
        }
    }
}