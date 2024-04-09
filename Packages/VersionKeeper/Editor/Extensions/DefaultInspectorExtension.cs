using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace VersionKeeper.Editor.Extensions
{
    public static class DefaultInspectorExtension
    {
        public static void HideScriptField(this VisualElement visualElement)
        {
            var scriptField = visualElement.Q<PropertyField>("PropertyField:m_Script");
            if (scriptField != null)
            {
                scriptField.style.display = DisplayStyle.None;
            }
        }
    }
}