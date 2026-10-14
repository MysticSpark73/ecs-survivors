using Code.Gameplay.Features.Abilities.Upgrade;
using Entitas;

namespace Code.Gameplay.Features.LevelUp.Systems
{
    public class UpgradeAbilityOnRequestSystem : IExecuteSystem
    {
        private readonly IAbilityUpgradeService _abilityUpgradeService;

        private readonly IGroup<GameEntity> _entities;
        private readonly IGroup<GameEntity> _levelUps;

        public UpgradeAbilityOnRequestSystem(GameContext gameContext, IAbilityUpgradeService abilityUpgradeService)
        {
            _abilityUpgradeService = abilityUpgradeService;

            _levelUps = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.LevelUp));

            _entities = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.AbilityUpgradeRequest,
                    GameMatcher.AbilityId));
        }

        public void Execute()
        {
            foreach (var entity in _entities)
            {
                foreach (var levelUp in _levelUps)
                {
                    _abilityUpgradeService.UpgradeAbility(entity.AbilityId);
                    levelUp.isArmamentProcessed = true;
                    entity.isDestroyed = true;
                }
            }
        }
    }
}