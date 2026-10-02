using System.Collections.Generic;
using Code.Gameplay.Features.Effects;
using Entitas;

namespace Code.Gameplay.Features.Armaments
{
    [Game] public class Armament : IComponent { }
    [Game] public class ArmamentProcessed : IComponent { }
    [Game] public class TargetsLimit : IComponent { public int Value; }
    [Game] public class EffectSetups : IComponent { public List<EffectSetup> Value; }
}