using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.StaticData;

namespace Code.Gameplay.Features.LevelUp.Services
{
    public class LevelUpService : ILevelUpService
    {
        public float CurrentExperience { get; private set; }

        public int CurrentLevel { get; private set; }

        private readonly IStaticDataService _staticDataService;

        public LevelUpService(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public void AddExperience(float experience)
        {
            CurrentExperience += experience;
            UpdateLevel();
        }

        public float GetExperienceForNextLevel() => _staticDataService.GetExperienceForLevel(CurrentLevel + 1);

        private void UpdateLevel()
        {
            if (CurrentLevel >= _staticDataService.GetMaxLevel()) return;
            
            float experienceForLevelUp = _staticDataService.GetExperienceForLevel(CurrentLevel + 1);

            if (CurrentExperience < experienceForLevelUp) return;

            CurrentExperience -= experienceForLevelUp;
            CurrentLevel++;

            CreateEntity.Empty()
                .With(e => e.isLevelUp = true);
            
            UpdateLevel();
        }
    }
}