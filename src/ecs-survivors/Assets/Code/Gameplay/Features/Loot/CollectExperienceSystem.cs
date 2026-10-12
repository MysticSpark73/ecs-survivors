using Entitas;

namespace Code.Gameplay.Features.Loot
{
    public class CollectExperienceSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _collected;
        private readonly IGroup<GameEntity> _heroes;

        public CollectExperienceSystem(GameContext gameContext)
        {
            _collected = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Collected,
                    GameMatcher.Experience));

            _heroes = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.Hero,
                    GameMatcher.Experience));
        }

        public void Execute()
        {
            foreach (var hero in _heroes)
            {
                foreach (var collected in _collected)
                {
                    hero.ReplaceExperience(hero.Experience + collected.Experience);
                }
            }
        }
    }
}