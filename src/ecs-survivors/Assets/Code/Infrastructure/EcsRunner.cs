using Code.Gameplay;
using Code.Infrastructure.Systems;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure
{
    public class EcsRunner : MonoBehaviour
    {
        private BattleFeature _battleFeature;
        private ISystemFactory _systemsFactory;

        [Inject]
        public void Construct(ISystemFactory systemFactory)
        {
            _systemsFactory = systemFactory;
            
        }
        
        private void Start()
        {
            _battleFeature = _systemsFactory.Create<BattleFeature>();
            _battleFeature.Initialize();
        }

        private void Update()
        {
            _battleFeature.Execute();
            _battleFeature.Cleanup();
        }

        private void OnDestroy()
        {
            _battleFeature.TearDown();
        }
    }
}