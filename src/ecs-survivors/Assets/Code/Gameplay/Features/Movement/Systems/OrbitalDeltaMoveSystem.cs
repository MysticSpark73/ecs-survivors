using Code.Gameplay.Common.Time;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Movement.Systems
{
    public class OrbitalDeltaMoveSystem : IExecuteSystem
    {
        private readonly ITimeService _timeService;
        private readonly IGroup<GameEntity> _movers;

        public OrbitalDeltaMoveSystem(GameContext gameContext, ITimeService timeService)
        {
            _timeService = timeService;
            _movers = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.OrbitPhase,
                    GameMatcher.OrbitCenterPosition,
                    GameMatcher.OrbitRadius,
                    GameMatcher.WorldPosition,
                    GameMatcher.Speed,
                    GameMatcher.MovementAvailable,
                    GameMatcher.Moving));
        }

        public void Execute()
        {
            foreach (GameEntity mover in _movers)
            {
                float phase = mover.OrbitPhase + _timeService.DeltaTime * mover.Speed;
                mover.ReplaceOrbitPhase(phase);

                Vector3 newRelativePosition = new Vector3(
                    Mathf.Cos(phase) * mover.OrbitRadius,
                    Mathf.Sin(phase) * mover.OrbitRadius,
                    0);

                Vector3 newPosition = newRelativePosition + mover.OrbitCenterPosition;
                mover.ReplaceWorldPosition(newPosition);
            }
        }
    }
}