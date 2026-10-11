using Code.Infrastructure.View.Registrars;
using UnityEngine;

namespace Code.Gameplay.Common.Visuals.EnchantVisuals
{
    public class EnchantVisualsRegistrar : EntityComponentRegistrar
    {
        [SerializeField] private EnchantVisuals _enchantVisuals;

        public override void RegisterComponents()
        {
            Entity.AddEnchantVisuals(_enchantVisuals);
        }

        public override void UnregisterComponents()
        {
            if (Entity.hasEnchantVisuals)
            {
                Entity.RemoveEnchantVisuals();
            }
        }
    }
}