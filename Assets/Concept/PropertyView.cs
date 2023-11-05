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
    public event Action<T, T> Changed;

    public T Value
    {
        get => _value;
        set
        {
            Changed?.Invoke(_value, value);
            _value = value;
        }
    }

    private T _value;
}