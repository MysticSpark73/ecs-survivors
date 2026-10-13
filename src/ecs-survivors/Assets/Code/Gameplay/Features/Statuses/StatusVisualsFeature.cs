using Code.Gameplay.Features.Enchants.Systems;
using Code.Gameplay.Features.Statuses.StatusVisuals;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.Statuses
{
    public class StatusVisualsFeature : Feature
    {
        public StatusVisualsFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<ApplyPoisonVisualsSystem>());
            Add(systemFactory.Create<ApplyFreezeVisualsSystem>());
            Add(systemFactory.Create<UnapplyPoisonVisualsSystem>());
            Add(systemFactory.Create<UnapplyFreezeVisualsSystem>());
            Add(systemFactory.Create<RemoveUnappliedEnchantsFromHolderSystem>());
        }
    }
}