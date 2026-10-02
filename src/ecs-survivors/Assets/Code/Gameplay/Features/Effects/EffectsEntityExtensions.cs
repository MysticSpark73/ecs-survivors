namespace Code.Gameplay.Features.Effects
{
    public static class EffectsEntityExtensions
    {
        private static GameContext GameContext => Contexts.sharedInstance.game;

        public static GameEntity Producer(this GameEntity effect)
        {
            if (!effect.hasProducerId) return null;
            return GameContext.GetEntityWithID(effect.ProducerId);
        }
        
        public static GameEntity Target(this GameEntity effect)
        {
            if (!effect.hasTargetId) return null;
            return GameContext.GetEntityWithID(effect.TargetId);
        }
    }
}