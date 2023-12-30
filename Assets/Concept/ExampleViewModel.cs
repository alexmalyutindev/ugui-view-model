using UnityEngine;
using UnityEngine.Serialization;

public partial class ExampleViewModel : ViewModelRoot
{
    public PropertyView<string> exampleLabel;
    public PropertyView<float> exampleFloat;
    public PropertyView<string> exampleInput;
    public PropertyView<string> exampleInputPlaceholder;

    // TODO: Add attribute to identify model props
    // [ViewModelProperty("UserName")]
    public PropertyView<string> userName;
    public PropertyView<string> userNameText;
    public PropertyView<string> password;
    public PropertyView<string> passwordText;
    public PropertyView<bool> loginButton;
    public PropertyView<string> loginButtonText;
    
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
        exampleLabel.Set(_exampleString);
        exampleFloat.Set(_exampleFloat);
        exampleInputPlaceholder.Set(_exampleInputPlaceHolder);

        userNameText.Set("User Name");
        passwordText.Set("Password");
        loginButtonText.Set("Login");
        loginButton.Changed += view =>
        {
            Debug.Log($"{nameof(loginButton)}: {view.OldValue} -> {view.Value}\n" +
                $"Login with: {userName.Value}: {password.Value}");
        };
    }

    private void OnValidate()
    {
        exampleLabel?.Set(_exampleString);
        exampleFloat?.Set(_exampleFloat);
        exampleInputPlaceholder?.Set(_exampleInputPlaceHolder);
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
        
        _propertiesCache[nameof(userName)] = userName = new();
        _propertiesCache[nameof(password)] = password = new();
        _propertiesCache[nameof(loginButton)] = loginButton = new();

        _propertiesCache[nameof(userNameText)] = userNameText = new();
        _propertiesCache[nameof(passwordText)] = passwordText = new();
        _propertiesCache[nameof(loginButtonText)] = loginButtonText = new();
    }
}