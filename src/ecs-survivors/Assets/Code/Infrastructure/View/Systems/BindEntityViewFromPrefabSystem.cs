using System.Collections.Generic;
using Code.Infrastructure.View.Factory;
using Entitas;

namespace Code.Infrastructure.View.Systems
{
    public class BindEntityViewFromPrefabSystem : IExecuteSystem
    {
        private readonly IEntityViewFactory _entityViewFactory;
        
        private readonly IGroup<GameEntity> _entities;
        private readonly List<GameEntity> _buffer = new (32);

        public BindEntityViewFromPrefabSystem(GameContext gameContext, IEntityViewFactory entityViewFactory)
        {
            _entityViewFactory = entityViewFactory;
            _entities = gameContext.GetGroup(GameMatcher
                .AllOf(GameMatcher.ViewPrefab)
                .NoneOf(GameMatcher.View));
        }
            
        public void Execute()
        {
            foreach (var entity in _entities.GetEntities(_buffer))
            {
                _entityViewFactory.CreateViewForEntityFromPrefab(entity);
            }
        }
    }
}