using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Gameplay.Features.Hero.Factory
{
    public class HeroFactory : IHeroFactory
    {
        private const float HeroHP = 100;
        private const string HeroViewPath = "Gameplay/Hero/hero";
        
        private readonly IIdentifierService _identifierService;

        public HeroFactory(IIdentifierService identifierService)
        {
            _identifierService = identifierService;
        }

        public GameEntity CreateHero(Vector3 position)
        {
            return CreateEntity.Empty()
                .AddID(_identifierService.Next())
                .AddWorldPosition(position)
                .AddDirection(Vector2.zero)
                .AddSpeed(2)
                .AddCurrentHP(HeroHP)
                .AddMaxHP(HeroHP)
                .AddViewPath(HeroViewPath)
                .With(e => e.isHero = true)
                .With(e => e.isTurnedAlongDirection = true)
                .With(e => e.isMovementAvailable = true);
        }
    }
}