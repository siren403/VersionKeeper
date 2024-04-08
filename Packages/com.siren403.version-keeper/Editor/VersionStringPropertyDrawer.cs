using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace AppVersion.Editor
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
                label = property.displayName
            };
            field.BindProperty(property);
            field.RegisterValueChangedCallback(e => { Debug.Log($"Value changed! {e.newValue}"); });
            container.Add(field);
            return container;
        }
    }
}