using Code.Gameplay.Features.Effects;
using Code.Gameplay.Features.Effects.Factory;
using Code.Gameplay.Features.Statuses;
using Code.Gameplay.Features.Statuses.Applier;
using Entitas;

namespace Code.Gameplay.Features.Loot
{
    public class CollectStatusItemSystem : IExecuteSystem
    {
        private readonly IStatusApplier _statusApplier;

        private readonly IGroup<GameEntity> _collected;
        private readonly IGroup<GameEntity> _heroes;

        public CollectStatusItemSystem(GameContext gameContext, IStatusApplier statusApplier)
        {
            _statusApplier = statusApplier;
            _collected = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Collected,
                    GameMatcher.StatusSetups));

            _heroes = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Hero,
                    GameMatcher.ID,
                    GameMatcher.WorldPosition));
        }

        public void Execute()
        {
            foreach (var hero in _heroes)
            {
                foreach (var collected in _collected)
                {
                    foreach (StatusSetup statusSetup in collected.StatusSetups)
                    {
                        _statusApplier.ApplyStatus(statusSetup, hero.ID, hero.ID);
                    }
                }
            }
        }
    }
}