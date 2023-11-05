using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LabelView : BindableView
{
    [ViewModelProperty]
    public string PropertyName;

    public string Format;

    private Text _text;

    protected override ViewDataBridge[] CreateDataBridge()
    {
        return new ViewDataBridge[]
        {
            new StringViewDataBridge(PropertyName)
        };
    }

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    public override void OnChanged<T>(PropertyView<T> propertyView)
    {
        // TODO: Cache `propertyView` type
        _text.text = propertyView switch
        {
            PropertyView<string> s => s.Value,
            PropertyView<int> i => i.Value.ToString(Format),
            PropertyView<float> f => f.Value.ToString(Format),
            _ => _text.text
        };
    }
}