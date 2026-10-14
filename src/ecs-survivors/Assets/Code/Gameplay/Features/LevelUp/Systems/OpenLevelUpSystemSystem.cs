using System.Collections.Generic;
using Code.Gameplay.Windows;
using Entitas;

namespace Code.Gameplay.Features.LevelUp.Systems
{
    public class OpenLevelUpSystemSystem : ReactiveSystem<GameEntity>
    {
        private readonly IWindowService _windowService;

        public OpenLevelUpSystemSystem(GameContext context, IWindowService windowService) : base(context)
        {
            _windowService = windowService;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.LevelUp.Added());

        protected override bool Filter(GameEntity entity) => entity.isLevelUp;

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
            {
                _windowService.Open(WindowId.LevelUpWindow);
            }
        }
    }
}