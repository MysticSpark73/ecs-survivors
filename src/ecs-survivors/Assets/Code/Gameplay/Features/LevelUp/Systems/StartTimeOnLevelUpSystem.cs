using System.Collections.Generic;
using Code.Gameplay.Common.Time;
using Entitas;

namespace Code.Gameplay.Features.LevelUp.Systems
{
    public class StartTimeOnLevelUpSystem : ReactiveSystem<GameEntity>
    {
        private readonly ITimeService _timeService;

        public StartTimeOnLevelUpSystem(GameContext context, ITimeService timeService) : base(context)
        {
            _timeService = timeService;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.ArmamentProcessed.Added());

        protected override bool Filter(GameEntity entity) => entity.isLevelUp && entity.isArmamentProcessed;

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
            {
                _timeService.StartTime();
            }
        }
    }
}