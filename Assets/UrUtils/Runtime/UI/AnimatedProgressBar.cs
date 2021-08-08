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
        [SerializeField, Range(0, 1)] float _value = 0f;

        [SerializeField] Image _slider = null;
        [SerializeField] float _smoothness = 1f;
        [SerializeField] bool _instantDecrease = true;

        void Update()
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

        void OnValidate()
        {
            if (_slider != null)
            {
                SetFill(_value);
            }
        }

        void SetFill(float value)
        {
            _slider.fillAmount = value;
        }
    }
}
