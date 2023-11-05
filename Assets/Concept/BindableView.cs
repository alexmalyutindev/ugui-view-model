using System.Collections.Generic;
using UnityEngine;

public abstract class BindableView : MonoBehaviour
{
    public string TargetPropertyName;
    public PropertyView TargetProperty;

    protected ViewDataBridge resolver { get; private set; }

    protected abstract ViewDataBridge CreateResolver();

    public abstract void OnChanged<T>(PropertyView<T> propertyView);

    public void Bind(Dictionary<string, PropertyView> properties)
    {
        resolver = CreateResolver();
        resolver.Link(this, properties[TargetPropertyName]);
    }
}