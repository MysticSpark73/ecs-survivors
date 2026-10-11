using Code.Gameplay.Features.Enchants.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.Enchants
{
    public sealed class EnchantFeature : Feature
    {
        public EnchantFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<PoisonEnchantSystem>());
            Add(systemFactory.Create<ExplosiveEnchantSystem>());
            Add(systemFactory.Create<ApplyPoisonEnchantVisualsSystem>());
        }
    }
}