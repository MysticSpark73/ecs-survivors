using System;
using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Features.Abilities;
using Code.Gameplay.Features.Abilities.Configs;
using Code.Gameplay.Features.Enchants;
using Code.Gameplay.Features.LevelUp;
using Code.Gameplay.Features.Loot;
using Code.Gameplay.Features.Loot.Configs;
using Code.Gameplay.Windows;
using Code.Gameplay.Windows.Configs;
using UnityEngine;

namespace Code.Gameplay.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string AbilitiesPath = "Configs/Abilities";
        private const string EnchantsPath = "Configs/Enchants";
        private const string LootPath = "Configs/Loot";
        public const string WindowsPath = "Configs/Windows/WindowsConfig";
        public const string LevelUpPath = "Configs/LevelUp/LevelUpConfig";

        private Dictionary<AbilityId, AbilityConfig> _abilityById;
        private Dictionary<EnchantTypeId, EnchantConfig> _enchantById;
        private Dictionary<LootTypeId, LootConfig> _lootById;
        private Dictionary<WindowId, GameObject> _windowPrefabsById;
        private LevelUpConfig _levelUpConfig;


        public void LoadAll()
        {
            LoadAbilities();
            LoadEnchants();
            LoadLoot();
            LoadWindows();
            LoadLevelUp();
        }

        public AbilityConfig GetAbilityConfig(AbilityId id)
        {
            if (_abilityById.TryGetValue(id, out AbilityConfig abilityConfig))
            {
                return abilityConfig;
            }

            throw new Exception($"AbilityConfig for {id} not found");
        }

        public EnchantConfig GetEnchantConfig(EnchantTypeId id)
        {
            if (_enchantById.TryGetValue(id, out EnchantConfig enchantConfig))
            {
                return enchantConfig;
            }

            throw new Exception($"EnchantConfig for {id} not found");
        }

        public LootConfig GetLootConfig(LootTypeId id)
        {
            if (_lootById.TryGetValue(id, out LootConfig lootConfig))
            {
                return lootConfig;
            }

            throw new Exception($"LootConfig for {id} not found");
        }

        public GameObject GetWindowPrefab(WindowId id)
        {
            if (_windowPrefabsById.TryGetValue(id, out GameObject prefab))
                return prefab;
            
            throw new Exception($"Prefab for window with id {id} not found");
        }

        public int GetMaxLevel() => _levelUpConfig.MaxLevel;
        public float GetExperienceForLevel(int level) => _levelUpConfig.ExperienceForLevelUp[level];

        public AbilityLevel GetAbilityLevel(AbilityId id, int level)
        {
            AbilityConfig abilityConfig = GetAbilityConfig(id);

            if (level > abilityConfig.Levels.Count)
            {
                level = abilityConfig.Levels.Count;
            }

            return abilityConfig.Levels[level - 1];
        }

        private void LoadAbilities()
        {
            _abilityById = Resources
                .LoadAll<AbilityConfig>(AbilitiesPath)
                .ToDictionary(i => i.AbilityId, i => i);
        }

        private void LoadEnchants()
        {
            _enchantById = Resources
                .LoadAll<EnchantConfig>(EnchantsPath)
                .ToDictionary(i => i.TypeId, i => i);
        }

        private void LoadLoot()
        {
            _lootById = Resources
                .LoadAll<LootConfig>(LootPath)
                .ToDictionary(i => i.TypeId, i => i);
        }

        private void LoadWindows()
        {
            _windowPrefabsById = Resources
                .Load<WindowsConfig>(WindowsPath)
                .WindowConfigs
                .ToDictionary(i => i.Id, i => i.Prefab);
        }

        private void LoadLevelUp()
        {
            _levelUpConfig = Resources.Load<LevelUpConfig>(LevelUpPath);
        }
    }
}