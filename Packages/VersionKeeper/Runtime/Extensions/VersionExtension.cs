using System;

namespace VersionKeeper.Extensions
{
    public static class VersionExtension
    {
        public static bool IsVersionString(this string version)
        {
            if (string.IsNullOrWhiteSpace(version)) return false;
            return Version.TryParse(version, out _);
        }
    }
}