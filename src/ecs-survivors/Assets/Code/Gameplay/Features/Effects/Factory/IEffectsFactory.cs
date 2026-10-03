namespace Code.Gameplay.Features.Effects.Factory
{
    public interface IEffectsFactory
    {
        GameEntity CreateEffect(EffectSetup setup, int producerId, int targetId);
    }
}