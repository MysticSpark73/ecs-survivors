using UnityEngine;
using UnityEngine.UI;

namespace Code.Gameplay.Features.LevelUp.Behaviours
{
    public class ExperienceMeter : MonoBehaviour
    {
        [SerializeField] private Slider _progressBar;
        [SerializeField] private Image _fill;

        public void SetExperience(float currentValue, float maxValue)
        {
            _fill.type = Image.Type.Tiled;
            _progressBar.value = currentValue / maxValue;
        }
    }
}