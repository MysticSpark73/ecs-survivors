using Code.Gameplay.Features.Enemies.Behaviours;
using Entitas;

namespace Code.Gameplay.Features.Enemies
{
    [Game] public class Enemy : IComponent { }
    [Game] public class EnemyTypeIdComponent : IComponent { public EnemyTypeId Value; }
    [Game] public class EnemyAnimatorComponent : IComponent { public EnemyAnimator Value; }
}