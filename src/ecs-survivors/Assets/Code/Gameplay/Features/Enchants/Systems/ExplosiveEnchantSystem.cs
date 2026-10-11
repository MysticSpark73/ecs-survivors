using System.Collections.Generic;
using Code.Gameplay.Features.Armaments.Factory;
using Entitas;

namespace Code.Gameplay.Features.Enchants.Systems
{
    public class ExplosiveEnchantSystem : ReactiveSystem<GameEntity>
    {
        private readonly IArmamentFactory _armamentFactory;
        private readonly IGroup<GameEntity> _enchants;

        public ExplosiveEnchantSystem(GameContext gameContext, IArmamentFactory armamentFactory) : base(gameContext)
        {
            _armamentFactory = armamentFactory;
            
            _enchants = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.EnchantTypeId,
                    GameMatcher.ExplosiveEnchant,
                    GameMatcher.ProducerId));
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) => context.CreateCollector(GameMatcher.AllOf(GameMatcher.Armament, GameMatcher.ReachedTarget).Added());

        protected override bool Filter(GameEntity entity) => entity.isArmament && entity.hasWorldPosition;

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity enchant in _enchants)
            {
                foreach (GameEntity entity in entities)
                {
                    _armamentFactory.CreateExplosion(enchant.ProducerId, entity.WorldPosition);
                }
            }
        }
    }
}