using UnityEngine.UI;

namespace Concept
{
    public class SliderView : BindableView
    {
        [ViewModelPropertyName]
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
                // TODO: Infer type from property type.
                ViewDataBridge.Create<float>(PropertyName, OnChanged),
            };
        }

        private void OnChanged<T>(IPropertyView<T> propertyView)
        {
            float value = propertyView switch
            {
                PropertyView<float> f => f.Value,
                PropertyView<int> i => i.Value,
                PropertyView<string> s => float.Parse(s.Value),
                _ => throw new BridgeTypeException(PropertyName, propertyView, dataBridge),
            };

            _slider.SetValueWithoutNotify(value);
        }

        private void OnSliderChanged(float value)
        {
            dataBridge.PushValueFromView(value);
        }
    }
}