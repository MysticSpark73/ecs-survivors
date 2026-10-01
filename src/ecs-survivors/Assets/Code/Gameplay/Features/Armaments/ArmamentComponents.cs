using Entitas;

namespace Code.Gameplay.Features.Armaments
{
    [Game] public class Armament : IComponent { }
    [Game] public class TargetsLimit : IComponent { public int Value; }
}