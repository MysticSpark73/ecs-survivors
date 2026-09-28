using Entitas;
using UnityEngine;

namespace Code.Common.Destroy.Systems
{
    public class CleanupGameDestroyedViewSystem : ICleanupSystem
    {
        private readonly IGroup<GameEntity> _destroyed;

        public CleanupGameDestroyedViewSystem(GameContext gameContext)
        {
            _destroyed = gameContext.GetGroup(
                GameMatcher.AllOf(
                    GameMatcher.Destroyed,
                    GameMatcher.View));
        }
        
        public void Cleanup()
        {
            foreach (GameEntity destroyed in _destroyed)
            {
                destroyed.View.ReleaseEntity();
                Object.Destroy(destroyed.View.GameObject);
            }
        }
    }
}