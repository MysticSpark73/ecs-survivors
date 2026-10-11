using Code.Gameplay.Features.Abilities;
using Code.Gameplay.Features.Abilities.Configs;
using Code.Gameplay.Features.Enchants;

namespace Code.Gameplay.StaticData
{
    public interface IStaticDataService
    {
        void LoadAll();
        AbilityConfig GetAbilityConfig(AbilityId id);
        AbilityLevel GetAbilityLevel(AbilityId id, int level);
        EnchantConfig GetEnchantConfig(EnchantTypeId id);
    }
}