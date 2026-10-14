using Code.Gameplay.Features.Effects;
using Code.Gameplay.Features.Effects.Factory;
using Entitas;

namespace Code.Gameplay.Features.Loot.Systems
{
    public class CollectEffectItemSystem : IExecuteSystem
    {
        private readonly IEffectsFactory _effectsFactory;
        
        private readonly IGroup<GameEntity> _collected;
        private readonly IGroup<GameEntity> _heroes;

        public CollectEffectItemSystem(GameContext gameContext, IEffectsFactory effectsFactory)
        {
            _effectsFactory = effectsFactory;
            _collected = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Collected,
                    GameMatcher.EffectSetups));

            _heroes = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Hero,
                    GameMatcher.ID,
                    GameMatcher.WorldPosition));
        }

        public void Execute()
        {
            foreach (var hero in _heroes)
            {
                foreach (var collected in _collected)
                {
                    foreach (EffectSetup effectSetup in collected.EffectSetups)
                    {
                        _effectsFactory.CreateEffect(effectSetup, hero.ID, hero.ID);
                    }
                }
            }
        }
    }
}