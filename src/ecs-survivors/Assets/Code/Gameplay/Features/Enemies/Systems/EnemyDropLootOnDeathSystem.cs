using Code.Gameplay.Features.Loot;
using Code.Gameplay.Features.Loot.Factory;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Enemies.Systems
{
    public class EnemyDropLootOnDeathSystem : IExecuteSystem
    {
        private readonly ILootFactory _lootFactory;

        private readonly IGroup<GameEntity> _enemies;

        public EnemyDropLootOnDeathSystem(GameContext gameContext, ILootFactory lootFactory)
        {
            _lootFactory = lootFactory;

            _enemies = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Enemy,
                    GameMatcher.WorldPosition,
                    GameMatcher.Dead,
                    GameMatcher.ProcessingDeath));
        }

        public void Execute()
        {
            foreach (var enemy in _enemies)
            {
                if (Random.Range(0, 1f) <= .15f)
                {
                    _lootFactory.CreateLootItem(LootTypeId.HealingItem, enemy.WorldPosition);
                }
                else if (Random.Range(0, 1f) <= .15f)
                {
                    _lootFactory.CreateLootItem(LootTypeId.PoisonEnchantItem, enemy.WorldPosition);
                }
                else if (Random.Range(0, 1f) <= .15f)
                {
                    _lootFactory.CreateLootItem(LootTypeId.ExplosionEnchantItem, enemy.WorldPosition);
                }
                else
                {
                    _lootFactory.CreateLootItem(LootTypeId.XPGem, enemy.WorldPosition);
                }
            }
        }
    }
}