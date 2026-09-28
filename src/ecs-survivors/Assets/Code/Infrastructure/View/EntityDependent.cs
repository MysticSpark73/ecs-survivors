using UnityEngine;

namespace Code.Infrastructure.View
{
    public abstract class EntityDependent : MonoBehaviour
    {
        public EntityBehaviour EntityView;
        
        public GameEntity Entity => EntityView != null ? EntityView.Entity : null;

        private void Awake()
        {
            if (EntityView == null)
            {
                EntityView = GetComponent<EntityBehaviour>();
            }
        }
    }
}