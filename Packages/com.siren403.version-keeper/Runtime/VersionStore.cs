using System;
using UnityEngine;

namespace AppVersion
{
    public class VersionStore : ScriptableObject
    {
        public static readonly Version InitialVersion = new(0, 1, 0);
        [SerializeField] [VersionString] private string versionString = InitialVersion.ToString();

        public Version Version
        {
            get => Version.Parse(versionString);
            set => versionString = value.ToString();
        }
    }
}