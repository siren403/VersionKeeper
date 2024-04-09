using System;
using UnityEngine;

namespace VersionKeeper
{
    public class VersionStore : ScriptableObject
    {
        [SerializeField] [VersionString] private string versionString = VersionManager.InitialVersion.ToString();

        public Version Version
        {
            get => Version.Parse(versionString);
            set => versionString = value.ToString();
        }

        private void OnEnable()
        {
            VersionManager.Register(this);
        }

        public override string ToString()
        {
            return versionString;
        }
    }
}