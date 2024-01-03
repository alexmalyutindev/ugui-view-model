using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace AlexMalyutinDev.ViewModelBinding.Editor
{
    [CustomPropertyDrawer(typeof(ViewModelPropertyNameAttribute))]
    public class ViewModelPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var buttonRect = EditorGUI.PrefixLabel(position, label);

            var value = string.IsNullOrEmpty(property.stringValue)
                ? new GUIContent("None")
                : new GUIContent(property.stringValue);

            if (EditorGUI.DropdownButton(buttonRect, value, FocusType.Passive))
            {
                if (property.serializedObject.targetObject is Component component)
                {
                    var viewModelRoot = component.GetComponentInParent<ViewModelRoot>();

                    var menu = new GenericMenu();
                    menu.AddItem(
                        new GUIContent("None"),
                        false,
                        () =>
                        {
                            property.stringValue = String.Empty;
                            property.serializedObject.ApplyModifiedProperties();
                        }
                    );

                    menu.AddSeparator(String.Empty);

                    var viewModelType = viewModelRoot.GetType();
                    var fields = viewModelType.GetFields(BindingFlags.Public | BindingFlags.Instance);
                    foreach (var field in fields)
                    {
                        menu.AddItem(
                            CreateGuiFromFieldInfo(viewModelType, field),
                            false,
                            () =>
                            {
                                property.stringValue = field.Name;
                                property.serializedObject.ApplyModifiedProperties();
                            }
                        );
                    }

                    menu.DropDown(buttonRect);
                }
            }
        }

        private static GUIContent CreateGuiFromFieldInfo(Type viewModelType, FieldInfo field)
        {
            return new GUIContent($"{viewModelType.Name}/{GetGenericTypeName(field.FieldType)} {field.Name}");
        }

        private static string GetGenericTypeName(Type type)
        {
            return GetTypeName(type.GenericTypeArguments[0]);
        }

        private static string GetTypeName(Type type)
        {
            return type.Name switch
            {
                nameof(Int32) => "int",
                nameof(Single) => "float",
                nameof(Double) => "double",
                nameof(String) => "string",
                nameof(Boolean) => "bool",
                _ => type.Name
            };
        }
    }
}