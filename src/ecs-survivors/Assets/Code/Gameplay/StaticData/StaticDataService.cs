using System;
using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Features.Abilities;
using Code.Gameplay.Features.Abilities.Configs;
using UnityEngine;

namespace Code.Gameplay.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private const string AbilitiesPath = "Configs/Abilities";
    private Dictionary<AbilityId, AbilityConfig> _abilityById;

    public void LoadAll()
    {
      LoadAbilities();
    }

    public AbilityConfig GetAbilityConfig(AbilityId id)
    {
      if (_abilityById.TryGetValue(id, out AbilityConfig abilityConfig))
      {
        return abilityConfig;
      }

      throw new Exception($"AbilityConfig for {id} not found");
    }

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
        .ToDictionary(i => i.AbilityId, i=> i);
    }
  }
}