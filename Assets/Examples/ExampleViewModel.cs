using UnityEngine;
using UnityEngine.Serialization;

namespace Example
{
    [ViewModelRoot]
    public partial class ExampleViewModel : ViewModelRoot
    {
        public PropertyView<string> headerName;
        public PropertyView<float> floatSlider;

        // TODO: Add attribute to identify model props
        [ViewModelProperty("UserName")]
        public PropertyView<string> userName;
        public PropertyView<string> userNameText;
        public PropertyView<string> password;
        public PropertyView<string> passwordText;

        public PropertyView<bool> loginButton;
        public PropertyView<bool> loginButtonActive;
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
            headerName.Set(_exampleString);
            floatSlider.Set(_exampleFloat);

            // TODO: Maybe notify view on subscribe?
            userName.Changed += ActivateLoginIfNotEmpty;
            password.Changed += ActivateLoginIfNotEmpty;

            userNameText.Set("User Name");
            passwordText.Set("Password");

            loginButtonText.Set("Login");
            loginButtonActive.Set(false);
            loginButton.Changed += view =>
            {
                Debug.Log(
                    $"{nameof(loginButton)}: {view.OldValue} -> {view.Value}\n" +
                    $"Login with: {userName.Value}: {password.Value}"
                );
            };
        }

        private void ActivateLoginIfNotEmpty(PropertyView<string> _)
        {
            var invalidInput = string.IsNullOrWhiteSpace(userName.Value) || string.IsNullOrWhiteSpace(password.Value);
            var active = !invalidInput;
            if (loginButtonActive.Value != active)
            {
                // TODO: Split stack to prevent deep recursion.
                loginButtonActive.Set(active);
            }
        }

        private void OnValidate()
        {
            headerName?.Set(_exampleString);
            floatSlider?.Set(_exampleFloat);
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
            GUILayout.Label(nameof(headerName) + ": " + headerName.Value);
            GUILayout.Label(nameof(floatSlider) + ": " + floatSlider.Value);

            GUILayout.Label(nameof(userName) + ": " + userName.Value);
            GUILayout.Label(nameof(password) + ": " + password.Value);
        }
    }
}
