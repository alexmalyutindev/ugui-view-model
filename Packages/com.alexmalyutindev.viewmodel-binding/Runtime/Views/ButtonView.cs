using UnityEngine;
using UnityEngine.UI;

namespace AlexMalyutinDev.ViewModelBinding
{
    [RequireComponent(typeof(Button))]
    public class ButtonView : BindableView
    {
        [ViewModelPropertyName]
        public string Action;
        [ViewModelPropertyName]
        public string Intercatable;
        [ViewModelPropertyName]
        public string Text;

        private Button _button;
        private Text _text;

        protected override ViewDataBridge[] CreateDataBridge()
        {
            return new ViewDataBridge[]
            {
                new BoolDataBridge(Action),
                ViewDataBridge.Create<bool>(Intercatable, SetInteractable),
                ViewDataBridge.Create<string>(Text, SetButtonText),
            };
        }

        private void SetInteractable(IPropertyView<bool> view)
        {
            _button.interactable = view.Value;
        }

        private void SetButtonText(IPropertyView<string> view)
        {
            _text.text = view.As<string>().Value;
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
            _text = _button.GetComponentInChildren<Text>();
        }

        private void OnClick()
        {
            dataBridge.PushValueFromView(true);
        }
    }
}
