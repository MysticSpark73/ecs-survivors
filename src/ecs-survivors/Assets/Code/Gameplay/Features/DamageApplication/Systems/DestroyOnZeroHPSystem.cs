using Entitas;

namespace Code.Gameplay.Features.DamageApplication.Systems
{
    public class DestroyOnZeroHPSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;

        public DestroyOnZeroHPSystem(GameContext gameContext)
        {
            _entities = gameContext.GetGroup(GameMatcher.CurrentHP);
        }
        public void Execute()
        {
            foreach (GameEntity entity in _entities)
            {
                if (entity.CurrentHP <= 0) entity.isDestroyed = true;
            }
        }
    }
}