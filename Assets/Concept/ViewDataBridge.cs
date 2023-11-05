using System;

public abstract class ViewDataBridge
{
    public readonly string PropertyName;

    protected ViewDataBridge(string propertyName)
    {
        PropertyName = propertyName;
    }
    
    public abstract void Link(BindableView view, PropertyView property);
    public abstract T GetValue<T>();
    public abstract void SetValue<T>(T value);
}

public class StringViewDataBridge : ViewDataBridge
{
    private PropertyView<string> _property;

    public StringViewDataBridge(string propertyName) : base(propertyName) { }

    public override void Link(BindableView view, PropertyView property)
    {
        _property = property.As<string>();
        _property.Changed += view.OnChangedFromModel;
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

public class FloatViewDataBridge : ViewDataBridge
{
    private PropertyView<float> _property;

    public FloatViewDataBridge(string targetPropertyName) : base(targetPropertyName) { }

    public override void Link(BindableView view, PropertyView property)
    {
        _property = property.As<float>();
        _property.Changed += view.OnChangedFromModel;
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