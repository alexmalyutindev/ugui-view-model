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

    [ViewModelPropertyName]
    public string PropertyName;
    [SerializeField]
    private PropertyType Type;

    public string Format;

    private Text _text;

    protected override ViewDataBridge[] CreateDataBridge()
    {
        var bridge = Type switch
        {
            PropertyType.String => new StringViewDataBridge(PropertyName).SubscribeOnModelChanged<string>(OnChanged),
            PropertyType.Float => new FloatViewDataBridge(PropertyName).SubscribeOnModelChanged<float>(OnChanged),
            _ => throw new Exception($"Can't bind Label to {PropertyName}!"),
        };

        return new[] { bridge };
    }

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    private void OnChanged<T>(IPropertyView<T> propertyView)
    {
        // TODO: Cache `propertyView` type
        _text.text = propertyView switch
        {
            IPropertyView<string> s => s.Value,
            IPropertyView<float> f => f.Value.ToString(Format),
            IPropertyView<int> i => i.Value.ToString(Format),
            _ => throw new BridgeTypeException(PropertyName, propertyView, dataBridge)
        };
    }
}