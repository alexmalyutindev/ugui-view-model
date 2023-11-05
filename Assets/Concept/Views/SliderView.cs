using System;
using UnityEngine;
using UnityEngine.UI;

namespace Concept
{
    public class SliderView : BindableView
    {
        [ViewModelProperty]
        public string PropertyName;

        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _slider.onValueChanged.AddListener(OnSliderChanged);
        }

        protected override ViewDataBridge[] CreateDataBridge()
        {
            return new ViewDataBridge[]
            {
                new FloatViewDataBridge(PropertyName)
            };
        }

        public override void OnChanged<T>(PropertyView<T> propertyView)
        {
            float value = propertyView switch
            {
                PropertyView<float> f => f.Value,
                PropertyView<int> i => i.Value,
                PropertyView<string> s => float.Parse(s.Value),
                _ => throw new Exception(
                    $"Property '{PropertyName}' has type '{propertyView.GetType().GenericTypeArguments[0].Name}', " +
                    $"but resolver '{dataBridge.GetType().Name}' doesn't support it!"
                )
            };

            _slider.SetValueWithoutNotify(value);
        }

        private void OnSliderChanged(float value)
        {
            dataBridge.SetValue(value);
        }
    }
}