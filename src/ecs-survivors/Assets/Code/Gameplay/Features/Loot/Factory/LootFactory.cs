using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Features.Loot.Configs;
using Code.Gameplay.StaticData;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Gameplay.Features.Loot.Factory
{
    public class LootFactory : ILootFactory
    {
        private readonly IIdentifierService _identifierService;
        private readonly IStaticDataService _staticDataService;

        public LootFactory(IIdentifierService identifierService, IStaticDataService staticDataService)
        {
            _identifierService = identifierService;
            _staticDataService = staticDataService;
        }

        public GameEntity CreateLootItem(LootTypeId typeId, Vector3 position)
        {
            LootConfig lootConfig = _staticDataService.GetLootConfig(typeId);

            return CreateEntity.Empty()
                .AddID(_identifierService.Next())
                .AddWorldPosition(position)
                .AddLootTypeId(typeId)
                .AddViewPrefab(lootConfig.ViewPrefab)
                .With(e => e.AddExperience(lootConfig.Experience), when: lootConfig.Experience > 0)
                .With(e => e.AddEffectSetups(lootConfig.EffectSetups), when: !lootConfig.EffectSetups.IsNullOrEmpty())
                .With(e => e.AddStatusSetups(lootConfig.StatusSetups), when: !lootConfig.StatusSetups.IsNullOrEmpty())
                .With(e => e.isPullable = true);
        }
    }
}