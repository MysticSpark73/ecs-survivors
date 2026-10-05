using Code.Gameplay.Features.CharacterStats.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.CharacterStats
{
    public sealed class StatsFeature : Feature
    {
        public StatsFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<StatChangedSystem>());
            Add(systemFactory.Create<ApplySpeedFromStatsSystem>());
        }
    }
}