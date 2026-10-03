using System;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Infrastructure.Identifiers;

namespace Code.Gameplay.Features.Effects.Factory
{
    public class EffectsFactory : IEffectsFactory
    {
        private readonly IIdentifierService _identifierService;

        public EffectsFactory(IIdentifierService identifierService)
        {
            _identifierService = identifierService;
        }

        public GameEntity CreateEffect(EffectSetup setup, int producerId, int targetId)
        {
            switch (setup.EffectTypeId)
            {
              case  EffectTypeId.Damage:
                  return CreateDamage(producerId, targetId, setup.Value);
              default:
                  throw new Exception($"Effect with type id {setup.EffectTypeId} does not exist");
            }
        }

        private GameEntity CreateDamage(int producerId, int targetId, float value)
        {
            return CreateEntity.Empty()
                .AddID(_identifierService.Next())
                .With(e => e.isEffect = true)
                .With(e => e.isDamageEffect = true)
                .AddEffectValue(value)
                .AddProducerId(producerId)
                .AddTargetId(targetId);
        }
    }
}