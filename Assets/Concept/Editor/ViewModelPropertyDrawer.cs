using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Concept.Editor
{
    [CustomPropertyDrawer(typeof(ViewModelPropertyAttribute))]
    public class ViewModelPropertyDrawer : PropertyDrawer
    {
        private static readonly float LabelWidth =
            EditorGUIUtility.labelWidth + EditorGUIUtility.standardVerticalSpacing;

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

                    var fields = viewModelRoot.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
                    foreach (var field in fields)
                    {
                        menu.AddItem(
                            CreateGuiFromFieldInfo(field),
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

        private static GUIContent CreateGuiFromFieldInfo(FieldInfo field)
        {
            return new GUIContent($"{field.Name} ({field.FieldType.GenericTypeArguments[0].Name})");
        }
    }
}