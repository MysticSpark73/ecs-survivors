using System;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Infrastructure.Identifiers;

namespace Code.Gameplay.Features.Statuses.Factory
{
    public class StatusFactory : IStatusFactory
    {
        private readonly IIdentifierService _identifierService;

        public StatusFactory(IIdentifierService identifierService)
        {
            _identifierService = identifierService;
        }

        public GameEntity CreateStatus(StatusSetup setup, int producerId, int targetId)
        {
            GameEntity status = null;
            switch (setup.StatusTypeId)
            {
                case StatusTypeId.Poison:
                    status = CreatePoisonStatus(setup, producerId, targetId);
                    break;
                default:
                    throw new Exception($"Effect with type id {setup.StatusTypeId} does not exist");
            }

            status = AddDurationComponents(setup, status);
            status = AddPeriodComponents(setup, status);
            
            return status;
        }

        private GameEntity AddDurationComponents(StatusSetup setup, GameEntity status)
        {
            return status
                .With(e => e.AddDuration(setup.Duration), when: setup.Duration > 0)
                .With(e => e.AddStatusTimeLeft(setup.Duration), when: setup.Duration > 0);
        }

        private GameEntity AddPeriodComponents(StatusSetup setup, GameEntity status)
        {
            return status
                .With(e => e.AddPeriod(setup.Period), when: setup.Period > 0)
                .With(e => e.AddTimeSinceLastPeriod(0), when: setup.Period > 0);
        }

        private GameEntity CreatePoisonStatus(StatusSetup setup, int producerId, int targetId)
        {
            return CreateEntity.Empty()
                .AddID(_identifierService.Next())
                .AddStatusTypeId(StatusTypeId.Poison)
                .AddEffectValue(setup.Value)
                .AddProducerId(producerId)
                .AddTargetId(targetId)
                .With(e => e.isStatus = true)
                .With(e => e.isPoison = true);
        }
    }
}