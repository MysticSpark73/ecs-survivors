using Code.Gameplay.Features.Effects.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.Effects
{
    public sealed class EffectFeature : Feature
    {
        public EffectFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<RemoveEffectsWithoutTargetSystem>());
            Add(systemFactory.Create<ProcessDamageEffectSystem>());
            Add(systemFactory.Create<CleanupProcessedEffectsSystem>());
        }
    }
}