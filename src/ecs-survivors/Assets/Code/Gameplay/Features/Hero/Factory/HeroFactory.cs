using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Features.CharacterStats;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Gameplay.Features.Hero.Factory
{
    public class HeroFactory : IHeroFactory
    {
        private const string HeroViewPath = "Gameplay/Hero/hero";
        
        private readonly IIdentifierService _identifierService;

        public HeroFactory(IIdentifierService identifierService)
        {
            _identifierService = identifierService;
        }

        public GameEntity CreateHero(Vector3 position)
        {
            var baseStats = InitStats.EmptyStatsDictionary()
                .With(i => i[Stats.Speed] = 2)
                .With(i => i[Stats.MaxHp] = 100);
            
            return CreateEntity.Empty()
                .AddID(_identifierService.Next())
                .AddWorldPosition(position)
                .AddBaseStats(baseStats)
                .AddStatModifiers(InitStats.EmptyStatsDictionary())
                .AddDirection(Vector2.zero)
                .AddSpeed(baseStats[Stats.Speed])
                .AddCurrentHP(baseStats[Stats.MaxHp])
                .AddMaxHP(baseStats[Stats.MaxHp])
                .AddViewPath(HeroViewPath)
                .With(e => e.isHero = true)
                .With(e => e.isTurnedAlongDirection = true)
                .With(e => e.isMovementAvailable = true);
        }
    }
}