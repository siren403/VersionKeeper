using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace AppVersion.Editor
{
    public class VersionKeeperSettingsProvider : SettingsProvider
    {
        [SettingsProvider]
        public static SettingsProvider Register()
        {
            return new VersionKeeperSettingsProvider("Project/AppVersion", SettingsScope.Project);
        }

        private VersionKeeperSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) :
            base(path, scopes, keywords)
        {
            label = "App Version";
        }


        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            base.OnActivate(searchContext, rootElement);

            rootElement.Add(new Button(() => Debug.Log("Hello World!"))
            {
                text = "Hello World!"
            });

            VersionStore version;
            var guids = AssetDatabase.FindAssets($"t:{typeof(VersionStore)}");
            if (!guids.Any())
            {
                if (!Version.TryParse(PlayerSettings.bundleVersion, out var initialVersion))
                {
                    // TODO: invalid bundle version
                    initialVersion = VersionStore.InitialVersion;
                }

                var versionStore = ScriptableObject.CreateInstance<VersionStore>();
                versionStore.Version = initialVersion;
                versionStore.name = "AppVersion";

                AssetDatabase.CreateAsset(versionStore, "Assets/VersionStore.asset");
                AssetDatabase.SaveAssets();

                var preloads = PlayerSettings.GetPreloadedAssets().ToList();
                preloads.Add(versionStore);
                PlayerSettings.SetPreloadedAssets(preloads.ToArray());

                version = versionStore;
            }
            else
            {
                version = AssetDatabase.LoadAssetAtPath<VersionStore>(AssetDatabase.GUIDToAssetPath(guids.First()));
            }

            var serializedObject = new SerializedObject(version);
            var serializedContainer = new VisualElement();
            serializedContainer.Bind(serializedObject);
            InspectorElement.FillDefaultInspector(serializedContainer, serializedObject, null);
            rootElement.Add(serializedContainer);
        }
    }
}