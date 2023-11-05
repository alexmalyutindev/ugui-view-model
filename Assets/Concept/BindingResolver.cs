using UnityEngine;

public abstract class BindingResolver
{
    public abstract void Resolve(BindableView view, PropertyView property);
    public abstract T GetValue<T>();
}

public class StringBindingResolver : BindingResolver
{
    private PropertyView<string> _viewTargetProperty;

    public override void Resolve(BindableView view, PropertyView property)
    {
        _viewTargetProperty = property.As<string>();
        view.TargetProperty = _viewTargetProperty;
        _viewTargetProperty.Changed += view.OnChanged;
    }

    public override T GetValue<T>()
    {
        return _viewTargetProperty.As<T>().Value;
    }
}