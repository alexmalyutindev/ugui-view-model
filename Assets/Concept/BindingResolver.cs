using UnityEngine;

public abstract class BindingResolver
{
    public abstract void Resolve(BindableView view, PropertyView property);
    public abstract T GetValue<T>();
    public abstract void SetValue<T>(T value);
}

public class StringBindingResolver : BindingResolver
{
    private PropertyView<string> _property;

    public override void Resolve(BindableView view, PropertyView property)
    {
        view.TargetProperty = property;
        property.As<string>().Changed += view.OnChanged;
    }

    public override T GetValue<T>()
    {
        return _property.As<T>().Value;
    }

    public override void SetValue<T>(T value)
    {
        _property.As<T>().SetFromView(value);
    }
}

public class FloatBindingResolver : BindingResolver
{
    private PropertyView<float> _property;

    public override void Resolve(BindableView view, PropertyView property)
    {
        view.TargetProperty = property;

        _property = property.As<float>();
        _property.Changed += view.OnChanged;
    }

    public override T GetValue<T>()
    {
        return _property.As<T>().Value;
    }

    public override void SetValue<T>(T value)
    {
        _property.As<T>().SetFromView(value);
    }
}