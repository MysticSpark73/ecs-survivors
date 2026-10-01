using System.Collections.Generic;
using Code.Gameplay.Common.Time;
using Entitas;

namespace Code.Gameplay.Features.Cooldowns.Systems
{
    public class CooldownSystem : IExecuteSystem
    {
        private readonly ITimeService _timeService;
        
        private readonly IGroup<GameEntity> _cooldowns;
        private readonly List<GameEntity> _buffer = new (32);

        public CooldownSystem(GameContext gameContext, ITimeService timeService)
        {
            _timeService = timeService;
            _cooldowns = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Cooldown,
                    GameMatcher.CooldownLeft));
        }
            
        public void Execute()
        {
            foreach (var cooldown in _cooldowns.GetEntities(_buffer))
            {
                cooldown.ReplaceCooldownLeft(cooldown.CooldownLeft - _timeService.DeltaTime);

                if (cooldown.CooldownLeft <= 0)
                {
                    cooldown.isCooldownUp = true;
                    cooldown.RemoveCooldownLeft();
                }
            }
        }
    }
}