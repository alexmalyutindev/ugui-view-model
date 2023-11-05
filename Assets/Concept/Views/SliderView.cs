using System;
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
            _slider.value = propertyView switch
            {
                PropertyView<float> f => f.Value,
                _ => throw new Exception(
                    $"Property '{PropertyName}' has type '{propertyView.GetType().GenericTypeArguments[0].Name}', " +
                    $"but resolver '{dataBridge.GetType().Name}' doesn't support it!"
                )
            };
        }

        private void OnSliderChanged(float value)
        {
            dataBridge.SetValue(value);
        }
    }
}