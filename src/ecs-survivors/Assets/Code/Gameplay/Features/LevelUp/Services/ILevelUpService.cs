namespace Code.Gameplay.Features.LevelUp.Services
{
    public interface ILevelUpService
    {
        float CurrentExperience { get; }
        int CurrentLevel { get; }
        void AddExperience(float experience);
        float GetExperienceForNextLevel();
    }
}