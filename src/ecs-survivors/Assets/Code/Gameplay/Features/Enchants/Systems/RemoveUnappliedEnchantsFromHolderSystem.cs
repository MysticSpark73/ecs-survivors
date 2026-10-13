using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Enchants.Systems
{
    public class RemoveUnappliedEnchantsFromHolderSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<GameEntity> _enchantHolders;

        public RemoveUnappliedEnchantsFromHolderSystem(GameContext gameContext) : base(gameContext)
        {
            _enchantHolders = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.EnchantHolder));
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) => 
            context.CreateCollector(GameMatcher.AllOf(GameMatcher.EnchantTypeId, GameMatcher.Unapplied).Added());

        protected override bool Filter(GameEntity entity) => entity.hasEnchantTypeId && entity.isUnapplied;

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
            {
                foreach (GameEntity holder in _enchantHolders)
                {
                    holder.EnchantHolder.RemoveEnchant(entity.EnchantTypeId);
                }
            }
        }
    }
}