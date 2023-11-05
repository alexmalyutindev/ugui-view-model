using System;
using UnityEngine.UI;

namespace Concept
{
    public class SliderView : BindableView
    {
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _slider.onValueChanged.AddListener(OnSliderChanged);
        }

        protected override ViewDataBridge CreateResolver()
        {
            return new FloatViewDataBridge();
        }

        public override void OnChanged<T>(PropertyView<T> propertyView)
        {
            _slider.value = propertyView switch
            {
                PropertyView<float> f => f.Value,
                _ => throw new Exception(
                    $"Property '{TargetPropertyName}' has type '{propertyView.GetType().GenericTypeArguments[0].Name}', " +
                    $"but resolver '{resolver.GetType().Name}' doesn't support it!"
                )
            };
        }

        private void OnSliderChanged(float value)
        {
            resolver.SetValue(value);
        }
    }
}