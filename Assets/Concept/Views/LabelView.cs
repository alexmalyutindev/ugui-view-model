using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LabelView : BindableView
{
    private Text _text;

    protected override BindingResolver CreateResolver()
    {
        // TODO: Switch on Property type.
        return new StringBindingResolver();
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
            PropertyView<int> i => i.Value.ToString(),
            _ => _text.text
        };
    }
}