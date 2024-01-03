using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace AlexMalyutinDev.ViewModelBinding
{
    [RequireComponent(typeof(Text))]
    public class LabelView : BindableView
    {
        private enum PropertyType
        {
            String,
            Int,
            Float
        }

        [ViewModelPropertyName]
        public string PropertyName;

        // TODO: Infer type from target property type
        [FormerlySerializedAs("Type")]
        [SerializeField]
        private PropertyType _type;

        [FormerlySerializedAs("Format")]
        [SerializeField]
        private string _format;

        private Text _text;

        protected override ViewDataBridge[] CreateDataBridge()
        {
            ViewDataBridge bridge = _type switch
            {
                PropertyType.String => ViewDataBridge.Create<string>(PropertyName, OnChanged),
                PropertyType.Float => ViewDataBridge.Create<float>(PropertyName, OnChanged),
                PropertyType.Int => ViewDataBridge.Create<int>(PropertyName, OnChanged),
                _ => throw new Exception($"Can't bind Label to {PropertyName}!"),
            };

            return new[] { bridge };
        }

        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        private void OnChanged<T>(IPropertyView<T> propertyView)
        {
            // TODO: Cache `propertyView` type
            _text.text = propertyView switch
            {
                IPropertyView<string> s => s.Value,
                IPropertyView<float> f => f.Value.ToString(_format),
                IPropertyView<int> i => i.Value.ToString(_format),
                _ => throw new BridgeTypeException(PropertyName, propertyView, dataBridge)
            };
        }
    }
}
