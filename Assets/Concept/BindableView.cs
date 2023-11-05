using System.Collections.Generic;
using UnityEngine;

public abstract class BindableView : MonoBehaviour
{
    public string TargetPropertyName;
    public PropertyView TargetProperty;

    protected BindingResolver resolver;

    public void Bind(Dictionary<string, PropertyView> properties)
    {
        resolver.Resolve(this, properties[TargetPropertyName]);
    }

    public abstract void OnChanged<T>(T oldValue, T newValue);
}