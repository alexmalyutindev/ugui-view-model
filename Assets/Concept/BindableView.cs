using System.Collections.Generic;
using UnityEngine;

public abstract class BindableView : MonoBehaviour
{
    public string TargetPropertyName;
    public PropertyView TargetProperty;

    protected BindingResolver resolver { get; private set; }

    protected abstract BindingResolver CreateResolver();

    public abstract void OnChanged<T>(PropertyView<T> propertyView);

    public void Bind(Dictionary<string, PropertyView> properties)
    {
        resolver = CreateResolver();
        resolver.Resolve(this, properties[TargetPropertyName]);
    }
}