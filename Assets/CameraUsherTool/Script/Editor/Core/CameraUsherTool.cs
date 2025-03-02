using UnityEngine;

namespace tamagotori.lib.CameraUsherTool
{
    public partial class CameraUsherTool
    {
        public static void SetupProjectSettings(CameraUsherToolWindow window)
        {
            if (window.projectSettingsData != null) return;
            window.projectSettingsData = CameraUsherToolSetup.GetOrCreateProjectSettings(window);
        }
    }

}
