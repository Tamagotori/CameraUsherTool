using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

namespace tamagotori.lib.CameraUsherTool
{
    public class CameraUsherToolUtil
    {
        public static string GetToolRootPath(ToolWindow window)
        {
            var windowPath = AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(window));
            var dirInfo = new DirectoryInfo(windowPath);
            while (dirInfo.Name != "CameraUsherTool")
            {
                if (dirInfo.Parent == null) return null;
                dirInfo = dirInfo.Parent;
            }
            var path = dirInfo.FullName;
            path = path.Replace("\\", "/");
            path = path.Split("/Assets/")[1];
            return $"Assets/{path}";
        }

        public static List<string> GetPresetPathList(ToolWindow window)
        {
            var pathList = new List<string>();
            var dirPath = $"{GetToolRootPath(window)}/CameraPreset";
            var dirInfo = new DirectoryInfo(dirPath);
            var files = dirInfo.GetFiles("*.asset", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var path = file.FullName;
                path = path.Replace("\\", "/");
                path = path.Split("/Assets/")[1];
                pathList.Add($"Assets/{path}");
            }
            return pathList;
        }

        public static List<CameraPresetData> GetPresetDataList(ToolWindow window)
        {
            var pathList = GetPresetPathList(window);
            var dataList = new List<CameraPresetData>();
            foreach (var path in pathList)
            {
                var data = AssetDatabase.LoadAssetAtPath<CameraPresetData>(path);
                dataList.Add(data);
            }
            return dataList;
        }
    }
}
