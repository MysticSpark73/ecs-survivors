using Entitas;

namespace Code.Gameplay.Features.Enchants.Systems
{
    public class AddEnchantsToHolderSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _enchantHolders;
        private readonly IGroup<GameEntity> _enchants;

        public AddEnchantsToHolderSystem(GameContext gameContext)
        {
            _enchantHolders = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.EnchantHolder));

            _enchants = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.EnchantTypeId,
                    GameMatcher.StatusTimeLeft));
        }

        public void Execute()
        {
            foreach (var holder in _enchantHolders)
            {
                foreach (var enchant in _enchants)
                {
                    holder.EnchantHolder.AddEnchant(enchant.EnchantTypeId);
                }
            }
        }
    }
}