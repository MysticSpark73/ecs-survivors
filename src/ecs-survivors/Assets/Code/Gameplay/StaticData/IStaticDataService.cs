using Code.Gameplay.Features.Abilities;
using Code.Gameplay.Features.Abilities.Configs;

namespace Code.Gameplay.StaticData
{
    public interface IStaticDataService
    {
        void LoadAll();
        AbilityConfig GetAbilityConfig(AbilityId id);
        AbilityLevel GetAbilityLevel(AbilityId id, int level);
    }
}