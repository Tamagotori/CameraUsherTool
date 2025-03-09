using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;

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

        static List<string> GetDataPathList(string searchGroupName, string searchCutName)
        {
            var pathList = new List<string>();
            if (string.IsNullOrEmpty(searchGroupName) || string.IsNullOrEmpty(searchCutName)) return pathList;
            var groupNameItemList = GetGroupNameItemList(false);
            foreach (var groupNameItem in groupNameItemList)
            {
                if (groupNameItem.Value != searchGroupName && searchGroupName != allGroupName) continue;
                var cutNameItemList = GetCutNameItemList(groupNameItem.Value, false);
                foreach (var cutNameItem in cutNameItemList)
                {
                    if (cutNameItem.Value != searchCutName && searchCutName != allGroupName) continue;
                    var dirPath = $"{GetToolRootPath()}/CameraPreset/{groupNameItem.Value}/{cutNameItem.Value}";
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

        public static ValueDropdownList<T> LoadDataItemList<T>(string searchGroupName, string searchCutName, NameType nameType) where T : PresetDataBase
        {
            var pathList = GetDataPathList(searchGroupName, searchCutName);
            var dataList = new ValueDropdownList<T>();
            foreach (var path in pathList)
            {
                var data = AssetDatabase.LoadAssetAtPath<T>(path);
                if (data == null) continue;
                dataList.Add(GetPresetDataItem(path, data, nameType));
            }
            return dataList;
        }

        public static List<T> LoadDataList<T>(string searchPath) where T : PresetDataBase
        {
            var dataList = new List<T>();
            var dirInfo = new DirectoryInfo(searchPath);
            var files = dirInfo.GetFiles("*.asset", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var path = file.FullName;
                path = path.Replace("\\", "/");
                path = path.Split("/Assets/")[1];
                path = $"Assets/{path}";
                var data = AssetDatabase.LoadAssetAtPath<T>(path);
                if (data == null) continue;
                dataList.Add(data);
            }
            return dataList;
        }

        static ValueDropdownItem<T> GetPresetDataItem<T>(string path, T data, NameType nameType) where T : PresetDataBase
        {
            var item = new ValueDropdownItem<T>("!!Error!!", data);
            if (nameType == NameType.FileName)
            {
                var fileInfo = new FileInfo(path);
                item.Text = fileInfo.Name;
            }
            else if (nameType == NameType.DisplayName)
            {
                item.Text = data.displayName;
            }
            return item;
        }

        public static ProjectSettingsData GetProjectSettingsData()
        {
            var path = $"{GetToolRootPath()}/Setting/{CameraUsherToolSetup.ProjectSettingsName}.asset";
            return AssetDatabase.LoadAssetAtPath<ProjectSettingsData>(path);
        }

        static ValueDropdownList<string> GetChildrenDirNameItemList(string path)
        {
            var pathList = new ValueDropdownList<string>();
            var dirInfo = new DirectoryInfo(path);
            var childrenDirInfoList = dirInfo.GetDirectories();
            foreach (var childDirInfo in childrenDirInfoList)
            {
                var hierarchyData = LoadDataList<PresetHirarchyData>(childDirInfo.FullName);
                pathList.Add(hierarchyData[0].displayName, childDirInfo.Name);
            }
            return pathList;
        }

        public static ValueDropdownList<string> GetGroupNameItemList(bool withAll)
        {
            var list = new ValueDropdownList<string>();
            if (withAll) list.Add(allGroupName, allGroupName);
            list.AddRange(GetChildrenDirNameItemList($"{GetToolRootPath()}/CameraPreset"));
            return list;
        }

        public static ValueDropdownList<string> GetCutNameItemList(string groupName, bool withAll)
        {
            var list = new ValueDropdownList<string>();
            if (withAll) list.Add(allGroupName, allGroupName);
            var groupNameItemList = GetChildrenDirNameItemList($"{GetToolRootPath()}/CameraPreset");
            foreach (var groupNameItem in groupNameItemList)
            {
                if (groupName != groupNameItem.Value && groupName != allGroupName) continue;
                list.AddRange(GetChildrenDirNameItemList($"{GetToolRootPath()}/CameraPreset/{groupNameItem.Value}"));
            }
            return list;
        }

        public enum NameType
        {
            FileName,
            DisplayName
        }
    }
}
