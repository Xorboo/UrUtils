using UnityEngine;
using UnityEngine.UI;

namespace UrUtils.UI
{
    public class AnimatedProgressBar : MonoBehaviour
    {
        public float Value
        {
            get => _value;
            set
            {
                _value = Mathf.Clamp01(value);
                if (_value < _slider.fillAmount && _instantDecrease)
                {
                    _slider.fillAmount = _value;
                }
            }
        }
        [SerializeField, Range(0, 1)] private float _value = 0f;

        [SerializeField] private Image _slider = null;
        [SerializeField] private float _smoothness = 1f;
        [SerializeField] private bool _instantDecrease = true;

        private void Update()
        {
            if (_slider != null)
            {
                float current = _slider.fillAmount;
                float newValue = Mathf.Approximately(current - _value, 0.001f) ? _value : Mathf.Lerp(current, _value, _smoothness * Time.deltaTime);
                if (current != newValue)
                {
                    SetFill(newValue);
                }
            }
        }

        private void OnValidate()
        {
            if (_slider != null)
            {
                SetFill(_value);
            }
        }

        private void SetFill(float value)
        {
            _slider.fillAmount = value;
        }
    }
}
