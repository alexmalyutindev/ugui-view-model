using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class InputFieldView : BindableView
{
    [ViewModelProperty]
    public string InputProperty;
    [ViewModelProperty]
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
        bridges[(int) Bindings.Input] = new StringViewDataBridge(InputProperty)
            .OnChanged<string>(OnInputField);
        bridges[(int) Bindings.PalaceHolder] = new StringViewDataBridge(PalaceHolderProperty)
            .OnChanged<string>(OnPlaceHolder);
        return bridges;
    }

    private void OnInputField(PropertyView<string> input)
    {
        _inputField.SetTextWithoutNotify(input.As<string>().Value);
    }
    
    private void OnPlaceHolder(PropertyView<string> input)
    {
        _placeHolder.text = input.As<string>().Value;
    }

    private void OnInputChanged(string value)
    {
        dataBridges[(uint) Bindings.Input].SetValue(value);
    }
}