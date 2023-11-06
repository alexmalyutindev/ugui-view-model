using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LabelView : BindableView
{
    private enum PropertyType
    {
        String,
        Int,
        Float
    }

    [ViewModelProperty]
    public string PropertyName;
    [SerializeField]
    private PropertyType Type;

    public string Format;

    private Text _text;

    protected override ViewDataBridge[] CreateDataBridge()
    {
        var bridge = Type switch
        {
            PropertyType.String => new StringViewDataBridge(PropertyName).OnChanged<string>(OnChanged),
            PropertyType.Float => new FloatViewDataBridge(PropertyName).OnChanged<float>(OnChanged),
            _ => throw new Exception($"Can't bind Label to {PropertyName}!"),
        };

        return new[] { bridge };
    }

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    private void OnChanged<T>(PropertyView<T> propertyView)
    {
        // TODO: Cache `propertyView` type
        _text.text = propertyView switch
        {
            PropertyView<string> s => s.Value,
            PropertyView<float> f => f.Value.ToString(Format),
            PropertyView<int> i => i.Value.ToString(Format),
            _ => throw new BridgeTypeException(PropertyName, propertyView, dataBridge)
        };
    }
}