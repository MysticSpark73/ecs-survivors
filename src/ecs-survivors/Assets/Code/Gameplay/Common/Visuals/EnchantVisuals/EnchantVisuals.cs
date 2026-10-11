using UnityEngine;

namespace Code.Gameplay.Common.Visuals.EnchantVisuals
{
    public class EnchantVisuals : MonoBehaviour, IEnchantVisuals
    {
        private static readonly int ColorProperty = Shader.PropertyToID("_Color");
        private static readonly int ColorIntensityProperty = Shader.PropertyToID("_Intensity");
        private static readonly int OutlineSizeProperty = Shader.PropertyToID("_OutlineSize");
        private static readonly int OutlineColorProperty = Shader.PropertyToID("_OutlineColor");
        private static readonly int OutlineSmoothnessProperty = Shader.PropertyToID("_OutlineSmoothness");

        [SerializeField] private Renderer _renderer;

        [Header("Freeze")]
        [SerializeField] private Color _freezeColor = new Color(56, 163, 190, 255);
        [SerializeField] private float _freezeOutlineSize = 3;
        [SerializeField] private float _freezeOutlineSmoothness = 8;
        
        [Header("Poison")]
        [SerializeField] private Color _poisonColor = new Color(56, 163, 190, 255);
        [SerializeField] private float _poisonColorIntensity = .6f;

        public void ApplyFreeze()
        {
            _renderer.material.SetColor(OutlineColorProperty, _freezeColor);
            _renderer.material.SetFloat(OutlineSizeProperty, _freezeOutlineSize);
            _renderer.material.SetFloat(OutlineSmoothnessProperty, _freezeOutlineSmoothness);
        }

        public void UnapplyFreeze()
        {
            _renderer.material.SetColor(OutlineColorProperty, Color.white);
            _renderer.material.SetFloat(OutlineSizeProperty, 0f);
            _renderer.material.SetFloat(OutlineSmoothnessProperty, 0f);
        }

        public void ApplyPoison()
        {
            _renderer.material.SetColor(ColorProperty, _poisonColor);
            _renderer.material.SetFloat(ColorIntensityProperty, _poisonColorIntensity);
        }
        public void UnapplyPoison()
        {
            _renderer.material.SetColor(ColorProperty, Color.white);
            _renderer.material.SetFloat(ColorIntensityProperty, 0f);
        }
    }
}