namespace Code.Gameplay.Features.Effects.Facts
{
    public interface IEffectsFactory
    {
        GameEntity CreateEffect(EffectSetup setup, int producerId, int targetId);
    }
}