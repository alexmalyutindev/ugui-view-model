using System;

public abstract class PropertyView
{
    public PropertyView<T> As<T>()
    {
        return (PropertyView<T>) this;
    }
}

public class PropertyView<T> : PropertyView
{
    public event Action<PropertyView<T>> Changed;

    public T Value => _value;
    public T OldValue => _oldValue;

    public void SetFromView(T value)
    {
        _oldValue = _value;
        _value = value;
        Changed?.Invoke(this);
    }
    
    public void SetFromModel(T value)
    {
        _oldValue = _value;
        _value = value;
        Changed?.Invoke(this);
    }

    private T _value;
    private T _oldValue;
}