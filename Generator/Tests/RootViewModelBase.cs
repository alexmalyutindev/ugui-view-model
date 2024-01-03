namespace Tests;

public abstract class RootViewModelBase
{
    protected Dictionary<string, object> _propertiesCache;
    protected abstract void InitPropertiesCache();
}
