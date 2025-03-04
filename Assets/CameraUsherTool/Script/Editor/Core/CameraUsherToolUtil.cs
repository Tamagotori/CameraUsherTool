using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

namespace tamagotori.lib.CameraUsherTool
{
    public class CameraUsherToolUtil
    {
        public static ScriptableObject searchPathPivot;
        public const string toolName = "CameraUsherTool";

        public static string GetToolRootPath()
        {
            var pivotPath = AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(searchPathPivot));
            var dirInfo = new DirectoryInfo(pivotPath);
            while (dirInfo.Name != toolName)
            {
                if (dirInfo.Parent == null) return null;
                dirInfo = dirInfo.Parent;
            }
            var path = dirInfo.FullName;
            path = path.Replace("\\", "/");
            path = path.Split("/Assets/")[1];
            return $"Assets/{path}";
        }

        public static List<string> GetPresetPathList()
        {
            var pathList = new List<string>();
            var dirPath = $"{GetToolRootPath()}/CameraPreset";
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

        public static List<CameraPresetData> GetPresetDataList()
        {
            var pathList = GetPresetPathList();
            var dataList = new List<CameraPresetData>();
            foreach (var path in pathList)
            {
                var data = AssetDatabase.LoadAssetAtPath<CameraPresetData>(path);
                dataList.Add(data);
            }
            return dataList;
        }

        public static ProjectSettingsData GetProjectSettingsData()
        {
            var path = $"{GetToolRootPath()}/Setting/{CameraUsherToolSetup.ProjectSettingsName}.asset";
            return AssetDatabase.LoadAssetAtPath<ProjectSettingsData>(path);
        }

        public static List<string> GetGroupNameList(bool withAll)
        {
            var list = new List<string>() { };
            if (withAll) list.Add("All");
            var projectSettings = GetProjectSettingsData();
            foreach (var name in projectSettings.groupNameList)
            {
                list.Add(name);
            }
            return list;
        }
    }
}
