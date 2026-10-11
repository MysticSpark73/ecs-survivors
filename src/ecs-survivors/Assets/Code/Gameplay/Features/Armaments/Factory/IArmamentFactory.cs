using Code.Gameplay.Features.Abilities;
using UnityEngine;

namespace Code.Gameplay.Features.Armaments.Factory
{
    public interface IArmamentFactory
    {
        GameEntity CreateVegetableBolt(int level, Vector3 position);
        GameEntity CreateOrbitingMushroom(int level, Vector3 position, float phase);
        GameEntity CreateEffectAura(AbilityId parentAbilityId, int producerId, int level);
        GameEntity CreateExplosion(int producerId, Vector3 position);
    }
}