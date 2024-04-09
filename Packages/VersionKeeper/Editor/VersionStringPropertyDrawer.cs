using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace VersionKeeper.Editor
{
    [CustomPropertyDrawer(typeof(VersionStringAttribute))]
    public class VersionStringPropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            if (property.propertyType != SerializedPropertyType.String) return base.CreatePropertyGUI(property);
            var container = new VisualElement();
            var field = new TextField()
            {
                label = property.displayName,
                isDelayed = true
            };
            field.BindProperty(property);
            field.RegisterValueChangedCallback(e =>
            {
                if (!Version.TryParse(e.newValue, out var version))
                {
                    Debug.Log($"{property.stringValue}, {e.previousValue}, {e.newValue}");
                    property.stringValue = e.previousValue;
                    property.serializedObject.ApplyModifiedProperties();
                    Debug.LogError($"Invalid version string! {property.stringValue}, {field.value}");
                }
                AssetDatabase.SaveAssetIfDirty(property.serializedObject.targetObject);
            });
            container.Add(field);
            return container;
        }
    }
}