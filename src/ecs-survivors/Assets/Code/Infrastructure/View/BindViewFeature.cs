using Code.Infrastructure.Systems;
using Code.Infrastructure.View.Systems;

namespace Code.Infrastructure.View
{
    public class BindViewFeature : Feature
    {
        public BindViewFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<BindEntityViewFromPathSystem>());
            Add(systemFactory.Create<BindEntityViewFromPrefabSystem>());
        }
    }
}