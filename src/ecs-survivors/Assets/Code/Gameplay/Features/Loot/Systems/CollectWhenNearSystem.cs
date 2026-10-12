using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Loot.Systems
{
    public class CollectWhenNearSystem : IExecuteSystem
    {
        private const float CollectDistance = .2f;
        
        private readonly IGroup<GameEntity> _pullable;
        private readonly IGroup<GameEntity> _heroes;

        public CollectWhenNearSystem(GameContext gameContext)
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
                    if (Vector3.Distance(hero.WorldPosition, pullable.WorldPosition) <= CollectDistance)
                    {
                        pullable.isCollected = true;
                    }
                }
            }
        }
    }
}