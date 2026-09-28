using Code.Common.Entity;
using Code.Infrastructure.Identifiers;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.View
{
    public class SelfInitializedEntityView : MonoBehaviour
    {
        [SerializeField] private EntityBehaviour _entityBehaviour;
        
        private IIdentifierService _identifierService;

        [Inject]
        public void Construct(IIdentifierService identifierService)
        {
            _identifierService = identifierService;
        }

        private void Awake()
        {
            GameEntity entity = CreateEntity.Empty()
                .AddID(_identifierService.Next());
            
            _entityBehaviour.SetEntity(entity);
        }
    }
}