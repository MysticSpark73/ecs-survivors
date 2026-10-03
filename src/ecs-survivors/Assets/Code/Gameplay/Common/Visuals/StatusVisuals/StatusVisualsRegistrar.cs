using Code.Infrastructure.View.Registrars;
using UnityEngine;

namespace Code.Gameplay.Common.Visuals.StatusVisuals
{
    public class StatusVisualsRegistrar : EntityComponentRegistrar
    {
        [SerializeField] private StatusVisuals _statusVisuals;
        public override void RegisterComponents()
        {
            Entity.AddStatusVisuals(_statusVisuals);
        }

        public override void UnregisterComponents()
        {
            if (Entity.hasStatusVisuals)
            {
                Entity.RemoveStatusVisuals();
            }
        }
    }
}