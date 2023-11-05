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
        InputProperty = 0,
        PalaceHolderProperty,
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
        bridges[(int) Bindings.InputProperty] = new StringViewDataBridge(InputProperty);
            // .OnChange(
            //     propertyView => { _inputField.text = propertyView.As<string>().Value; }
            // );
        bridges[(int) Bindings.PalaceHolderProperty] = new StringViewDataBridge(PalaceHolderProperty);
        return bridges;
    }

    public override void OnChangedFromModel<T>(PropertyView<T> propertyView)
    {
        _inputField.SetTextWithoutNotify(propertyView.As<string>().Value);

        // TODO: Resolve multiple properties bindings.
        // _placeHolder.text = propertyView.As<string>().Value;
    }

    private void OnInputChanged(string value)
    {
        dataBridges[(uint) Bindings.InputProperty].SetValue(value);
    }
}