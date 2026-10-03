using System.Linq;
using Code.Common.EntityIndexes;
using Code.Common.Extensions;
using Code.Gameplay.Features.Statuses.Factory;

namespace Code.Gameplay.Features.Statuses.Applier
{
    public class StatusApplier : IStatusApplier
    {
        private readonly GameContext _gameContext;
        private readonly IStatusFactory _statusFactory;

        public StatusApplier(GameContext gameContext, IStatusFactory statusFactory)
        {
            _gameContext = gameContext;
            _statusFactory = statusFactory;
        }

        public GameEntity ApplyStatus(StatusSetup setup, int producerId, int targetId)
        {
            GameEntity status = _gameContext.TargetStatusesOfType(setup.StatusTypeId, targetId).FirstOrDefault();
            
            if (status != null) return status.ReplaceStatusTimeLeft(setup.Duration);

            return _statusFactory.CreateStatus(setup, producerId, targetId)
                .With(e => e.isApplied = true);
        }
    }
}