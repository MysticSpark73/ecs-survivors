using Code.Gameplay.Common.Time;
using Code.Gameplay.Features.Effects;
using Code.Gameplay.Features.Effects.Factory;
using Entitas;

namespace Code.Gameplay.Features.Statuses.Systems
{
    public class DamageOverTimeStatusSystem : IExecuteSystem
    {
        private readonly ITimeService _timeService;
        private readonly IEffectsFactory _effectsFactory;

        private readonly IGroup<GameEntity> _statuses;
    
        public DamageOverTimeStatusSystem(GameContext gameContext, ITimeService timeService, IEffectsFactory effectsFactory)
        {
            _timeService = timeService;
            _effectsFactory = effectsFactory;

            _statuses = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Status,
                    GameMatcher.Period,
                    GameMatcher.EffectValue,
                    GameMatcher.TimeSinceLastPeriod,
                    GameMatcher.ProducerId,
                    GameMatcher.TargetId));
        }
            
        public void Execute()
        {
            foreach (var status in _statuses)
            {
                if (status.TimeSinceLastPeriod > 0)
                {
                    status.ReplaceTimeSinceLastPeriod(status.TimeSinceLastPeriod - _timeService.DeltaTime);
                }
                else
                {
                    status.ReplaceTimeSinceLastPeriod(status.Period);
                    
                    _effectsFactory.CreateEffect(new EffectSetup()
                    {
                        EffectTypeId = EffectTypeId.Damage,
                        Value = status.EffectValue
                    },
                        status.ProducerId,
                        status.TargetId);
                }
            }
        }
    }
}