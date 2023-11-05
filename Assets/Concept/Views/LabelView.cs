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
        ViewDataBridge bridge = Type switch
        {
            PropertyType.String => new StringViewDataBridge(PropertyName),
            PropertyType.Float => new FloatViewDataBridge(PropertyName),
            _ => new StringViewDataBridge(PropertyName),
        };

        return new ViewDataBridge[] { bridge };
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
            PropertyView<float> f => f.Value.ToString(Format),
            PropertyView<int> i => i.Value.ToString(Format),
            _ => _text.text
        };
    }
}