using UnityEngine;
using UnityEngine.UI;

namespace AlexMalyutinDev.ViewModelBinding
{
    [RequireComponent(typeof(InputField))]
    public class InputFieldView : BindableView
    {
        [ViewModelPropertyName]
        public string InputProperty;
        [ViewModelPropertyName]
        public string PalaceHolderProperty;

        private enum Bindings
        {
            Input = 0,
            PalaceHolder,
            Count
        }

        private InputField _inputField;
        private Text _placeHolder;

        private void Awake()
        {
            _inputField = GetComponent<InputField>();
            _placeHolder = _inputField.placeholder.GetComponent<Text>();
            _inputField.onValueChanged.AddListener(OnInputChanged);
        }

        protected override ViewDataBridge[] CreateDataBridge()
        {
            var bridges = new ViewDataBridge[(int) Bindings.Count];
            // TODO: Make generic inferable!
            bridges[(int) Bindings.Input] = ViewDataBridge.Create<string>(InputProperty, OnInputField);
            bridges[(int) Bindings.PalaceHolder] = ViewDataBridge.Create<string>(PalaceHolderProperty, OnPlaceHolder);
            return bridges;
        }

        private void OnInputField(IPropertyView input)
        {
            _inputField.SetTextWithoutNotify(input.As<string>().Value);
        }

        private void OnPlaceHolder(IPropertyView input)
        {
            _placeHolder.text = input.As<string>().Value;
        }

        private void OnInputChanged(string value)
        {
            dataBridges[(uint) Bindings.Input].PushValueFromView(value);
        }
    }
}
