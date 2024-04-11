using System;

namespace VersionKeeper
{
    public static class VersionManager
    {
        internal static readonly Version InitialVersion = new(0, 1, 0);

        private static VersionStore _versionStore;

        public static void Register(VersionStore version)
        {
            _versionStore = version;
        }

        public static string GetVersionString()
        {
            return _versionStore == null
                ? InitialVersion.ToString()
                : _versionStore.Version.ToString();
        }

        public static Version GetVersion()
        {
            return _versionStore == null
                ? InitialVersion
                : _versionStore.Version;
        }
    }
}