using Entitas;

namespace Code.Gameplay.Features.Hero.Systems
{
    public class SetHeroDirectionByInput : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _heroes;
        private readonly IGroup<GameEntity> _input;

        public SetHeroDirectionByInput(GameContext gameContext)
        {
            _heroes = gameContext.GetGroup(GameMatcher.Hero);
            _input = gameContext.GetGroup(GameMatcher.Input);
        }
        
        public void Execute()
        {
            foreach (GameEntity input in _input)
            {
                foreach (GameEntity hero in _heroes)
                {
                    hero.isMoving = input.hasAxisInput;

                    if (input.hasAxisInput)
                    {
                        hero.ReplaceDirection(input.AxisInput.normalized);
                    }
                }
            }
        }
    }
}