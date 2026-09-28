using Code.Infrastructure.View;
using Entitas;

namespace Code.Common
{
    [Game] public class View : IComponent { public IEntityView Value; }
    [Game] public class Destroyed : IComponent { }
    [Game] public class SelfDestructTimer : IComponent { public float Value; }
}