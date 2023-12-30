using System;

public abstract class ViewDataBridge
{
    public readonly string PropertyName;

    protected ViewDataBridge(string propertyName)
    {
        PropertyName = propertyName;
    }

    public abstract void Link(BindableView view, PropertyView property);

    public abstract void PushValueFromView<T>(T value);
    public abstract void PushValueFromModel<T>(T value);
    
    public static ViewDataBridge<T> Create<T>(string propertyName, Action<IPropertyView<T>> onChange)
    {
        var bridge = new ViewDataBridge<T>(propertyName);
        bridge.SubscribeOnModelChanged(onChange);
        return bridge;
    }
}

public class ViewDataBridge<TValue> : ViewDataBridge, IDisposable
{
    private INotifiableViewSide<TValue> _viewSide;
    private INotifiableModelSide<TValue> _modelSide;

    private Action<IPropertyView<TValue>> _onChangedFromModel;
    private Action<IPropertyView<TValue>> _onChangedFromView;

    public ViewDataBridge(string propertyName) : base(propertyName) { }

    public override void Link(BindableView view, PropertyView property)
    {
        var modelProperty = property.As<TValue>();
        _viewSide = modelProperty;
        _modelSide = modelProperty;
        // NOTE: _viewSide listen changes from model!
        _viewSide.Changed += _onChangedFromModel;
        // NOTE: _modelSide listen changes from view!
        _modelSide.Changed += _onChangedFromView;
    }

    public override void PushValueFromView<T>(T value)
    {
        if (value is not TValue exactTypeValue)
        {
            throw new Exception($"Bound view has type {typeof(T)}, but property of type {typeof(float)}!");
        }

        _viewSide.Set(exactTypeValue);
    }

    public override void PushValueFromModel<T>(T value)
    {
        if (value is not TValue valueExact)
        {
            throw new Exception($"Bound view has type {typeof(T)}, but property of type {typeof(float)}!");
        }

        _modelSide.Set(valueExact);
    }

    public ViewDataBridge<TValue> SubscribeOnModelChanged<T>(Action<IPropertyView<T>> onChange)
    {
        if (onChange is not Action<IPropertyView<TValue>> onChangeExact)
        {
            throw new TypeMismatchException(typeof(float), onChange.GetType());
        }

        _onChangedFromModel = onChangeExact;
        return this;
    }

    public void Dispose()
    {
        _viewSide.Changed -= _onChangedFromModel;
        _modelSide.Changed -= _onChangedFromView;
    }
}

public class BoolDataBridge : ViewDataBridge<bool>
{
    public BoolDataBridge(string propertyName) : base(propertyName) { }
}

public class TypeMismatchException : Exception
{
    public TypeMismatchException(Type expected, Type actual) : base(
        $"Get type {actual.Name}, but expected {expected.Name}"
    ) { }
}
