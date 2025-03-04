using UnityEditor;
using UnityEngine;

namespace tamagotori.lib.CameraUsherTool
{
    public partial class CameraUsherTool
    {
        public static CameraPresetData CreatePresetData()
        {
            var rootPath = CameraUsherToolUtil.GetToolRootPath();
            var assetPath = $"{rootPath}/CameraPreset/CameraPresetData.asset";
            assetPath = AssetDatabase.GenerateUniqueAssetPath(assetPath);

            var presetData = ScriptableObject.CreateInstance<CameraPresetData>();
            AssetDatabase.CreateAsset(presetData, assetPath);
            AssetDatabase.Refresh();

            return AssetDatabase.LoadAssetAtPath<CameraPresetData>(assetPath);
        }
    }
}

