using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonView : BindableView
{
    [FormerlySerializedAs("PropertyName")]
    [ViewModelPropertyName]
    public string Action;
    [ViewModelPropertyName]
    public string Text;

    private Button _button;
    private Text _text;

    protected override ViewDataBridge[] CreateDataBridge()
    {
        return new ViewDataBridge[]
        {
            new BoolDataBridge(Action),
            new StringViewDataBridge(Text)
                .SubscribeOnModelChanged<string>(view => _text.text = view.As<string>().Value),
        };
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
        _text = _button.GetComponentInChildren<Text>();
    }

    private void OnClick()
    {
        dataBridge.PushValueFromView(true);
    }
}