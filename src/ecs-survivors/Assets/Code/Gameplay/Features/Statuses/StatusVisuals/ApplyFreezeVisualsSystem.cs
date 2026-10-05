using System.Collections.Generic;
using Code.Gameplay.Features.Effects;
using Entitas;

namespace Code.Gameplay.Features.Statuses.StatusVisuals
{
    public class ApplyFreezeVisualsSystem : ReactiveSystem<GameEntity>
    {
        public ApplyFreezeVisualsSystem(GameContext gameContext) : base(gameContext)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) => 
            context.CreateCollector(GameMatcher.Freeze.Added());

        protected override bool Filter(GameEntity entity) => entity.isStatus && entity.isFreeze && entity.hasTargetId;

        protected override void Execute(List<GameEntity> statuses)
        {
            foreach (GameEntity status in statuses)
            {
                var target = status.Target();
                if (target is { hasStatusVisuals: true })
                {
                    target.StatusVisuals.ApplyFreeze();
                }
            }
        }
    }
}