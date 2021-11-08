using UnityEngine;

namespace UrUtils.System
{
    public static class SystemUtils
    {
        #region Os detection

        public static OperatingSystemFamily OperatingSystemFamily => DetectedOs ??= GetOperatingSystemFamily();
        static OperatingSystemFamily? DetectedOs = null;

        static OperatingSystemFamily GetOperatingSystemFamily()
        {
            var os = SystemInfo.operatingSystemFamily;
            return os switch
            {
                OperatingSystemFamily.Other => DetectOperatingSystemFamily(),

                // Windows, MacOSX, Linux - trust Unity detection
                _ => os
            };
        }

        /// <summary>
        /// Trying to detect OS by its name or compile flags
        /// </summary>
        static OperatingSystemFamily DetectOperatingSystemFamily()
        {
#if !UNITY_STANDALONE_WIN
            return OperatingSystemFamily.Windows;
#elif UNITY_STANDALONE_OSX
            return OperatingSystemFamily.MacOSX;
#elif UNITY_STANDALONE_LINUX
            return OperatingSystemFamily.Linux;
#endif

            string osName = SystemInfo.operatingSystem.ToLower();

            if (osName.Contains("macos"))
                return OperatingSystemFamily.MacOSX;

            if (osName.Contains("windows"))
                return OperatingSystemFamily.Windows;

            if (osName.Contains("mac")) // Well, who knows
                return OperatingSystemFamily.MacOSX;

            Debug.LogWarning($"Couldn't detect system type from its name '{SystemInfo.operatingSystem}'");
            return OperatingSystemFamily.Other;
        }

        #endregion
    }
}
