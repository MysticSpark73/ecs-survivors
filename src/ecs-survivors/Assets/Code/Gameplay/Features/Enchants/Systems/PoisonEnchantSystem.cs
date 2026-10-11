using System.Collections.Generic;
using Code.Gameplay.Features.Statuses;
using Code.Gameplay.StaticData;
using Entitas;

namespace Code.Gameplay.Features.Enchants.Systems
{
    public class PoisonEnchantSystem : IExecuteSystem
    {
        private readonly IStaticDataService _staticDataService;

        private readonly IGroup<GameEntity> _enchants;
        private readonly IGroup<GameEntity> _armaments;

        private readonly List<GameEntity> _buffer = new(32);

        public PoisonEnchantSystem(GameContext gameContext, IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            _enchants = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.EnchantTypeId,
                    GameMatcher.PoisonEnchant,
                    GameMatcher.ProducerId));

            _armaments = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Armament,
                    GameMatcher.ProducerId)
                .NoneOf(GameMatcher.PoisonEnchant));
        }

        public void Execute()
        {
            foreach (var enchant in _enchants)
            {
                foreach (GameEntity armament in _armaments.GetEntities(_buffer))
                {
                    if (enchant.ProducerId == armament.ProducerId)
                    {
                        GetOrAddStatusSetups(armament)
                            .AddRange(_staticDataService.GetEnchantConfig(EnchantTypeId.PoisonArmaments).StatusSetups);
                        armament.isPoisonEnchant = true;
                    }
                }
            }
        }

        private List<StatusSetup> GetOrAddStatusSetups(GameEntity armament)
        {
            if (!armament.hasStatusSetups)
            {
                armament.AddStatusSetups(new List<StatusSetup>());
            }

            return armament.StatusSetups;
        }
    }
}