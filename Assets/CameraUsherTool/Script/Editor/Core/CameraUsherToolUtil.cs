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
        const string allGroupName = "All";

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

        public static List<string> GetPresetPathList(string searchGroupName, string searchCutName)
        {
            var pathList = new List<string>();
            if (string.IsNullOrEmpty(searchGroupName) || string.IsNullOrEmpty(searchCutName)) return pathList;
            var groupNameList = GetGroupNameList(false);
            foreach (var groupName in groupNameList)
            {
                if (groupName != searchGroupName && searchGroupName != allGroupName) continue;
                var cutNameList = GetCutNameList(groupName, false);
                foreach (var cutName in cutNameList)
                {
                    if (cutName != searchCutName && searchCutName != allGroupName) continue;
                    var dirPath = $"{GetToolRootPath()}/CameraPreset/{groupName}/{cutName}";
                    var dirInfo = new DirectoryInfo(dirPath);
                    var files = dirInfo.GetFiles("*.asset", SearchOption.AllDirectories);
                    foreach (var file in files)
                    {
                        var path = file.FullName;
                        path = path.Replace("\\", "/");
                        path = path.Split("/Assets/")[1];
                        pathList.Add($"Assets/{path}");
                    }
                }
            }

            return pathList;
        }

        public static List<CameraPresetData> GetPresetDataList(string searchGroupName, string searchCutName)
        {
            var pathList = GetPresetPathList(searchGroupName, searchCutName);
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

        static List<string> GetFolderChildNameList(string path)
        {
            var list = new List<string>();
            var dirInfo = new DirectoryInfo(path);
            foreach (var dir in dirInfo.GetDirectories())
            {
                list.Add(dir.Name);
            }
            return list;
        }

        public static List<string> GetGroupNameList(bool withAll)
        {
            var list = new List<string>() { };
            if (withAll) list.Add(allGroupName);
            var groupNameList = GetFolderChildNameList($"{GetToolRootPath()}/CameraPreset");
            foreach (var name in groupNameList)
            {
                list.Add(name);
            }
            return list;
        }

        public static List<string> GetCutNameList(string groupName, bool withAll)
        {
            var list = new List<string>() { };
            if (withAll) list.Add(allGroupName);
            var groupNameList = GetFolderChildNameList($"{GetToolRootPath()}/CameraPreset");
            foreach (var name in groupNameList)
            {
                if (groupName != name && groupName != allGroupName) continue;
                var cutNameList = GetFolderChildNameList($"{GetToolRootPath()}/CameraPreset/{name}");
                foreach (var cutName in cutNameList)
                {
                    list.Add(cutName);
                }
            }

            return list;
        }
    }
}
