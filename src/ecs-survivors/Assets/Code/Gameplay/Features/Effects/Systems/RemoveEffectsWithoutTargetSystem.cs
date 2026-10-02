using Entitas;

namespace Code.Gameplay.Features.Effects.Systems
{
    public class RemoveEffectsWithoutTargetSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _effects;
    
        public RemoveEffectsWithoutTargetSystem(GameContext gameContext)
        {
            _effects = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Effect,
                    GameMatcher.TargetId));
        }
            
        public void Execute()
        {
            foreach (var effect in _effects)
            {
                GameEntity target = effect.Target();

                if (target == null) effect.Destroy();
            }
        }
    }
}