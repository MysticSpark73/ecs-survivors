using System;
using System.Collections;
using Code.Gameplay.Features.Abilities;
using Code.Gameplay.Features.Abilities.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Gameplay.Features.LevelUp.Behaviours
{
    public class AbilityCard : MonoBehaviour
    {
        private const float StampAnimationTime = 1;
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _stamp;
        
        private AbilityId _id;
        private Action<AbilityId> _onSelected;

        public void SetUp(AbilityId id, AbilityLevel abilityLevel, Action<AbilityId> onSelected)
        {
            _id = id;
            _icon.sprite = abilityLevel.Icon;
            _description.text = abilityLevel.Description;

            _onSelected = onSelected;
            
            _button.onClick.AddListener(SelectCard);
        }

        private void SelectCard()
        {
            StartCoroutine(StampAndReport());
        }

        public IEnumerator StampAndReport()
        {
            _stamp.SetActive(true);
            yield return new WaitForSeconds(StampAnimationTime);
            _stamp.SetActive(false);
            _onSelected?.Invoke(_id);
        }
    }
}