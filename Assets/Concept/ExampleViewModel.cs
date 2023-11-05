using System;
using UnityEngine;
using UnityEngine.Serialization;

public partial class ExampleViewModel : ViewModelRoot
{
    public PropertyView<string> exampleLabel;
    public PropertyView<float> exampleFloat;
    public PropertyView<string> exampleInput;
    public PropertyView<string> exampleInputPlaceholder;

    [SerializeField]
    private string _exampleString;
    [SerializeField]
    private float _exampleFloat;
    [FormerlySerializedAs("_exampleInputPlaceholder")]
    [SerializeField]
    private string _exampleInputPlaceHolder;

    private GUIStyle _box;
    private GUIStyle _boldLabel;

    private void Start()
    {
        exampleLabel.SetFromModel(_exampleString);
        exampleFloat.SetFromModel(_exampleFloat);
        exampleInputPlaceholder.SetFromModel(_exampleInputPlaceHolder);
    }

    private void OnValidate()
    {
        exampleLabel?.SetFromModel(_exampleString);
        exampleFloat?.SetFromModel(_exampleFloat);
        exampleInputPlaceholder?.SetFromModel(_exampleInputPlaceHolder);
    }

    private void OnGUI()
    {
        _box ??= new GUIStyle("box")
        {
            normal = new GUIStyleState
            {
                textColor = Color.black
            },
            active = new GUIStyleState()
            {
                textColor = Color.black
            }
        };
        _boldLabel ??= new GUIStyle("Label")
        {
            fontStyle = FontStyle.Bold
        };

        using var verticalScope = new GUILayout.VerticalScope(_box);
        GUILayout.Label("Model:", _boldLabel);
        GUILayout.Label(nameof(exampleLabel) + ": " + exampleLabel.Value);
        GUILayout.Label(nameof(exampleFloat) + ": " + exampleFloat.Value);
        GUILayout.Label(nameof(exampleInput) + ": " + exampleInput.Value);
    }
}

// Generated
public partial class ExampleViewModel
{
    protected override void InitPropertiesCache()
    {
        _propertiesCache[nameof(exampleLabel)] = exampleLabel = new();
        _propertiesCache[nameof(exampleFloat)] = exampleFloat = new();
        _propertiesCache[nameof(exampleInput)] = exampleInput = new();
        _propertiesCache[nameof(exampleInputPlaceholder)] = exampleInputPlaceholder = new();
    }
}