using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Effects.Systems
{
    public class ProcessHealEffectSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _effects;

        public ProcessHealEffectSystem(GameContext gameContext)
        {
            _effects = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.HealEffect,
                    GameMatcher.EffectValue,
                    GameMatcher.TargetId));
        }

        public void Execute()
        {
            foreach (var effect in _effects)
            {
                GameEntity target = effect.Target();

                effect.isArmamentProcessed = true;

                if (target.isDead) continue;

                if (target.hasMaxHP && target.hasCurrentHP)
                {
                    float newValue = Mathf.Min(target.CurrentHP + effect.EffectValue, target.MaxHP);
                    target.ReplaceCurrentHP(newValue);
                }
            }
        }
    }
}