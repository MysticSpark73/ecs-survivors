using Code.Common.Destroy.Systems;
using Code.Infrastructure.Systems;

namespace Code.Common.Destroy
{
    public class ProcessDestroyedFeature : Feature
    {
        public ProcessDestroyedFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<SelfDestructTimerSystem>());
            Add(systemFactory.Create<Systems.CleanupGameDestroyedViewSystem>());
            Add(systemFactory.Create<Systems.CleanupGameDestroyedSystem>());
        }
    }
}