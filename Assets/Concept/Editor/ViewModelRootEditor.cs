using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Concept.Editor
{
    [CustomEditor(typeof(ViewModelRoot), editorForChildClasses: true)]
    public class ViewModelRootEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (target is not ViewModelRoot viewModel)
            {
                return;
            }

            var fields = viewModel.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (var field in fields)
            {
                GUILayout.Label(field.Name + " : " + field.FieldType.GenericTypeArguments[0].Name);
            }
        }
    }
}