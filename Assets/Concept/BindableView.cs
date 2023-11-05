using System.Collections.Generic;
using UnityEngine;

public abstract class BindableView : MonoBehaviour
{
    protected ViewDataBridge dataBridge => dataBridges[0];
    protected ViewDataBridge[] dataBridges { get; private set; }

    protected abstract ViewDataBridge[] CreateDataBridge();

    public abstract void OnChanged<T>(PropertyView<T> propertyView);

    public void Bind(Dictionary<string, PropertyView> properties)
    {
        dataBridges = CreateDataBridge();
        foreach (var bridge in dataBridges)
        {
            bridge.Link(this, properties[bridge.PropertyName]);
        }
    }
}