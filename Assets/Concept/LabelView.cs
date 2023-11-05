using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LabelView : BindableView
{
    private Text _text;

    private LabelView()
    {
        resolver = new StringBindingResolver();
    }
    
    private void Awake()
    {
        _text = GetComponent<Text>();
        _text.text = resolver.GetValue<string>();
    }

    public override void OnChanged<T>(T oldValue, T newValue)
    {
        // TODO:
        _text.text = newValue.ToString();
    }
}