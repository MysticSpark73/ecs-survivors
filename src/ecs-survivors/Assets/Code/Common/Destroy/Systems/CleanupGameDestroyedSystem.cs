using System.Collections.Generic;
using Entitas;

namespace Code.Common.Destroy.Systems
{
    public class CleanupGameDestroyedSystem : ICleanupSystem
    {
        private readonly IGroup<GameEntity> _destroyed;
        private readonly List<GameEntity> _buffer = new (64);

        public CleanupGameDestroyedSystem(GameContext gameContext)
        {
            _destroyed = gameContext.GetGroup(GameMatcher.Destroyed);
        }
        
        public void Cleanup()
        {
            foreach (GameEntity destroyed in _destroyed.GetEntities(_buffer))
            {
                destroyed.Destroy();
            }
        }
    }
}