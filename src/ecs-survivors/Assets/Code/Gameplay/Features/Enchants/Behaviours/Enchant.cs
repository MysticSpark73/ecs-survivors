using UnityEngine;
using UnityEngine.UI;

namespace Code.Gameplay.Features.Enchants.Behaviours
{
    public class Enchant : MonoBehaviour
    {
        public EnchantTypeId  EnchantTypeId => _enchantTypeId;
        
        [SerializeField] private Image _icon;
        [SerializeField] private EnchantTypeId _enchantTypeId;

        public void Set(EnchantConfig config)
        {
            _enchantTypeId = config.TypeId;
            _icon.sprite = config.Icon;
        }
    }
}