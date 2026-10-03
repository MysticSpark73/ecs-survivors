using Code.Gameplay.Common.Time;
using Entitas;

namespace Code.Gameplay.Features.Statuses.Systems
{
    public class StatusDurationSystem : IExecuteSystem
    {
        private readonly ITimeService _timeService;
        
        private readonly IGroup<GameEntity> _statuses;

        public StatusDurationSystem(GameContext gameContext, ITimeService timeService)
        {
            _timeService = timeService;
            _statuses = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Status,
                    GameMatcher.Duration,
                    GameMatcher.StatusTimeLeft
                    ));
        }

        public void Execute()
        {
            foreach (var status in _statuses)
            {
                if (status.StatusTimeLeft >= 0)
                {
                    status.ReplaceStatusTimeLeft(status.StatusTimeLeft - _timeService.DeltaTime);
                }
                else
                {
                    status.isUnapplied = true;
                }
            }
        }
    }
}