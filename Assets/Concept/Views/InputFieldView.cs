using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class InputFieldView : BindableView
{
    private InputField _inputField;

    private void Awake()
    {
        _inputField = GetComponent<InputField>();
        _inputField.onValueChanged.AddListener(OnInputChanged);
    }

    protected override ViewDataBridge CreateResolver()
    {
        // TODO: Switch on Property type.
        return new StringViewDataBridge();
    }

    public override void OnChanged<T>(PropertyView<T> propertyView)
    {
        _inputField.text = propertyView.As<string>().Value;
    }

    private void OnInputChanged(string value)
    {
        resolver.SetValue(value);
    }
}