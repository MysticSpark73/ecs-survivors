using Entitas;

namespace Code.Gameplay.Features.Abilities.Systems
{
    public class DestroyAbilityOnUpgradeSystem : IExecuteSystem
    {
        private readonly GameContext _gameContext;
        private readonly IGroup<GameEntity> _upgradeRequest;
        private readonly IGroup<GameEntity> _abilities;

        public DestroyAbilityOnUpgradeSystem(GameContext gameContext)
        {
            _gameContext = gameContext;

            _upgradeRequest = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.AbilityUpgradeRequest,
                    GameMatcher.AbilityId));

            _abilities = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.AbilityId,
                    GameMatcher.RecreatedOnUpgrade));
        }

        public void Execute()
        {
            foreach (var request in _upgradeRequest)
            {
                foreach (var ability in _abilities)
                {
                    if (request.AbilityId == ability.AbilityId)
                    {
                        foreach (var entity in _gameContext.GetEntitiesWithParentAbility(ability.AbilityId))
                        {
                            entity.isDestroyed = true;
                        }

                        ability.isActive = false;
                    }
                }
            }
        }
    }
}