using System.Collections.Generic;
using UnityEngine;

public abstract class BindableView : MonoBehaviour
{
    protected ViewDataBridge dataBridge => dataBridges[0];
    protected ViewDataBridge[] dataBridges { get; private set; }

    protected abstract ViewDataBridge[] CreateDataBridge();

    public void Bind(ViewModelRoot viewModel, Dictionary<string, PropertyView> properties)
    {
        dataBridges = CreateDataBridge();
        foreach (var bridge in dataBridges)
        {
            if (properties.TryGetValue(bridge.PropertyName, out var propertyView))
            {
                bridge.Link(this, propertyView);
            }
            else
            {
                Debug.LogError($"Can't find property '{bridge.PropertyName}' in ViewModel '{viewModel.GetType().Name}'!");
            }
        }
    }
}