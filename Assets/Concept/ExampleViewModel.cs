using UnityEngine;

public partial class ExampleViewModel : ViewModelRoot
{
    public PropertyView<string> exampleString;
    public PropertyView<int> exampleInt;

    [SerializeField]
    private string _exampleString;

    private void OnValidate()
    {
        if (exampleString != null)
        {
            exampleString.Value = _exampleString;
        }
    }
}

// Generated
public partial class ExampleViewModel
{
    protected override void InitPropertiesCache()
    {
        _propertiesCache[nameof(exampleString)] = exampleString = new PropertyView<string>();
        _propertiesCache[nameof(exampleInt)] = exampleInt = new PropertyView<int>();
    }
}