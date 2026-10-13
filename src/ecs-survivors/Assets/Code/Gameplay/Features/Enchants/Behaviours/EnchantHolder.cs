using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Features.Enchants.UIFactories;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Features.Enchants.Behaviours
{
    public class EnchantHolder : MonoBehaviour
    {
        [SerializeField] private Transform _enchantLayout;

        private IEnchantUIFactory _factory;
        private readonly List<Enchant> _enchants = new();

        [Inject]
        private void Construct(IEnchantUIFactory factory)
        {
            _factory = factory;
        }

        public void AddEnchant(EnchantTypeId typeId)
        {
            if (IsEnchantAlreadyCreated(typeId)) return;

            Enchant enchant = _factory.CreateEnchant(_enchantLayout, typeId);
            _enchants.Add(enchant);
        }

        public void RemoveEnchant(EnchantTypeId typeId)
        {
            Enchant enchant = _enchants.Find(i => i.EnchantTypeId == typeId);
            if (enchant == null) return;

            _enchants.Remove(enchant);
            Destroy(enchant.gameObject);
        }

        private bool IsEnchantAlreadyCreated(EnchantTypeId typeId) => _enchants.Any(i => i.EnchantTypeId == typeId);
    }
}