using Entitas;

namespace Code.Gameplay.Features.Movement.Systems
{
    public class UpdateTransformDirectionSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _movers;

        public UpdateTransformDirectionSystem(GameContext gameContext)
        {
            _movers = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.Transform
                ));
        }
        
        public void Execute()
        {
            foreach (GameEntity mover in _movers)
            {
                mover.Transform.position = mover.WorldPosition;
            }
        }
    }
}