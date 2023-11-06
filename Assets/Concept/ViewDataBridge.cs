using System;

public abstract class ViewDataBridge
{
    public readonly string PropertyName;

    protected ViewDataBridge(string propertyName)
    {
        PropertyName = propertyName;
    }

    public abstract ViewDataBridge OnChanged<T>(Action<PropertyView<T>> onChange);
    public abstract void Link(BindableView view, PropertyView property);
    public abstract T GetValue<T>();
    public abstract void SetValue<T>(T value);
}

public abstract class ViewDataBridge<TValue> : ViewDataBridge
{
    protected PropertyView<TValue> _property;
    protected Action<PropertyView<TValue>> _onChange;

    protected ViewDataBridge(string propertyName) : base(propertyName) { }
}

public class StringViewDataBridge : ViewDataBridge<string>, IDisposable
{
    public StringViewDataBridge(string propertyName) : base(propertyName) { }

    public override void Link(BindableView view, PropertyView property)
    {
        _property = property.As<string>();
        _property.Changed += _onChange;
    }

    public override T GetValue<T>()
    {
        return _property.As<T>().Value;
    }

    public override void SetValue<T>(T value)
    {
        if (value is not string stringValue)
        {
            throw new Exception($"Bound view has type {typeof(T)}, but property of type {typeof(float)}!");
        }

        _property.SetFromView(stringValue);
    }

    public override ViewDataBridge OnChanged<T>(Action<PropertyView<T>> onChange)
    {
        if (onChange is not Action<PropertyView<string>> onChangeString)
        {
            throw new Exception();
        }

        _onChange = onChangeString;
        return this;
    }

    public void Dispose()
    {
        _property.Changed -= _onChange;
    }
}

public class FloatViewDataBridge : ViewDataBridge<float>, IDisposable
{
    public FloatViewDataBridge(string targetPropertyName) : base(targetPropertyName) { }

    public override ViewDataBridge OnChanged<T>(Action<PropertyView<T>> onChange)
    {
        if (onChange is not Action<PropertyView<float>> onChangeString)
        {
            throw new TypeMismatchException(typeof(float), onChange.GetType());
        }

        _onChange = onChangeString;
        return this;
    }

    public override void Link(BindableView view, PropertyView property)
    {
        _property = property.As<float>();
        _property.Changed += _onChange;
    }

    public override T GetValue<T>()
    {
        return _property.As<T>().Value;
    }

    public override void SetValue<T>(T value)
    {
        if (value is not float floatValue)
        {
            throw new TypeMismatchException(typeof(float), typeof(T));
        }

        _property.SetFromView(floatValue);
    }

    public void Dispose()
    {
        _property.Changed -= _onChange;
    }
}

public class BoolDataBridge : ViewDataBridge<bool>
{
    public BoolDataBridge(string propertyName) : base(propertyName) { }
    public override ViewDataBridge OnChanged<T>(Action<PropertyView<T>> onChange)
    {
        return this;
    }

    public override void Link(BindableView view, PropertyView property)
    {
        _property = property.As<bool>();
    }

    public override T GetValue<T>()
    {
        return _property.As<T>().Value;
    }

    public override void SetValue<T>(T value)
    {
        if (value is not bool boolValue)
        {
            throw new TypeMismatchException(typeof(bool), typeof(T));
        }

        _property.SetFromView(boolValue);
    }
}

public class TypeMismatchException : Exception
{
    public TypeMismatchException(Type expected, Type actual) : base(
        $"Get type {actual.Name}, but expected {expected.Name}"
    ) { }
}