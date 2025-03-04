using UnityEngine;
using UnityEditor;
using System.IO;

namespace tamagotori.lib.CameraUsherTool
{
    public class CameraUsherToolSetup
    {
        public const string ProjectSettingsName = "ProjectSettings";
        public static ProjectSettingsData GetOrCreateProjectSettings()
        {
            var projectSettingPath = $"{CameraUsherToolUtil.GetToolRootPath()}/Setting/{ProjectSettingsName}.asset";

            var settingsData = AssetDatabase.LoadAssetAtPath<ProjectSettingsData>(projectSettingPath);
            if (settingsData != null) return settingsData;

            settingsData = ScriptableObject.CreateInstance<ProjectSettingsData>();
            settingsData.name = ProjectSettingsName;
            AssetDatabase.CreateAsset(settingsData, projectSettingPath);
            AssetDatabase.Refresh();

            return AssetDatabase.LoadAssetAtPath<ProjectSettingsData>(projectSettingPath);
        }
    }
}

