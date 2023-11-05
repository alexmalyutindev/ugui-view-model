using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ViewModelRoot : MonoBehaviour
{
    protected Dictionary<string, PropertyView> _propertiesCache = new();
    private Dictionary<string, BindableView> _viewsCache = new();

    protected abstract void InitPropertiesCache();
    protected virtual void OnAwake() { }

    private void Awake()
    {
        InitPropertiesCache();
        InitBindings();
        OnAwake();
    }

    private void InitBindings()
    {
        var bindableViews = GetComponentsInChildren<BindableView>();
        foreach (var bindableView in bindableViews)
        {
            _viewsCache[bindableView.TargetPropertyName] = bindableView;
            bindableView.Bind(_propertiesCache);
        }
    }
}