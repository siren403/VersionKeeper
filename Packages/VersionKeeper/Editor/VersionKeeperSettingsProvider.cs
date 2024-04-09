using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using VersionKeeper.Editor.Extensions;

namespace VersionKeeper.Editor
{
    public class VersionKeeperSettingsProvider : SettingsProvider
    {
        private const string PackageName = "com.siren403.version-keeper";
        private const string PackageResources = "Packages/" + PackageName + "/PackageResources";
        private const string ProjectSettingsPath = PackageResources + "/ProjectSettings.uxml";
        private const string DefaultThemePath = PackageResources + "/VersionKeeper.tss";

        [SettingsProvider]
        public static SettingsProvider Register()
        {
            return new VersionKeeperSettingsProvider($"Project/{nameof(VersionKeeper)}", SettingsScope.Project);
        }

        private VersionKeeperSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) :
            base(path, scopes, keywords)
        {
            label = "Version Keeper";
        }


        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            base.OnActivate(searchContext, rootElement);

            var (titleLabel, contentContainer) = LoadView(rootElement);

            titleLabel.text = label;

            VersionStore version = GetOrCreateVersionStore();
            contentContainer.Add(CreateVersionStoreInspector(version));
        }

        private ProjectSettingElements LoadView(VisualElement rootElement)
        {
            var projectSettings = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(ProjectSettingsPath)
                .Instantiate();
            rootElement.Add(projectSettings);

            var defaultTheme = AssetDatabase.LoadAssetAtPath<StyleSheet>(DefaultThemePath);
            projectSettings.styleSheets.Add(defaultTheme);

            return ProjectSettingElements.Q(rootElement);
        }


        private VersionStore GetOrCreateVersionStore()
        {
            VersionStore version;

            var guids = AssetDatabase.FindAssets($"t:{typeof(VersionStore)}");
            if (!guids.Any())
            {
                if (!Version.TryParse(PlayerSettings.bundleVersion, out var initialVersion))
                {
                    // TODO: invalid bundle version
                    initialVersion = VersionManager.InitialVersion;
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

            return version;
        }

        private VisualElement CreateVersionStoreInspector(VersionStore version)
        {
            var serializedObject = new SerializedObject(version);
            var serializedContainer = new VisualElement();
            serializedContainer.Bind(serializedObject);
            InspectorElement.FillDefaultInspector(serializedContainer, serializedObject, null);
            serializedContainer.HideScriptField();
            return serializedContainer;
        }
    }
}