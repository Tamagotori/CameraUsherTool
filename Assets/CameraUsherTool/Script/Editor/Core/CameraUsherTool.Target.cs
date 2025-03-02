using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;
using System.Text.RegularExpressions;

namespace tamagotori.lib.CameraUsherTool
{
    public partial class CameraUsherTool
    {
        public static ValueDropdownList<GameObject> GetTargetList(ProjectSettingsData projectSettingsData)
        {
            var list = new ValueDropdownList<GameObject>();
            List<GameObject> resultList = new List<GameObject>();
            var count = SceneManager.sceneCount;
            for (int i = 0; i < count; i++)
            {
                var rootObjects = SceneManager.GetSceneAt(i).GetRootGameObjects();
                var layerIndex = -1;
                if (!string.IsNullOrEmpty(projectSettingsData.targetLayer))
                {
                    layerIndex = LayerMask.NameToLayer(projectSettingsData.targetLayer);
                }
                foreach (var targetName in projectSettingsData.targetNameList)
                {
                    var targetNameRegex = new Regex(targetName);
                    foreach (var rootObject in rootObjects)
                    {
                        resultList.AddRange(GetTargetChildrenObject(rootObject, projectSettingsData.targetTag, layerIndex, targetNameRegex));
                    }
                }
            }
            foreach (var result in resultList)
            {
                list.Add(result.name, result);
            }
            DistinctList(list);
            return list;
        }

        static List<GameObject> GetTargetChildrenObject(GameObject rootObject, string targetTag, int layerIndex, Regex targetNameRegex)
        {
            var resultList = new List<GameObject>();
            var children = rootObject.GetComponentsInChildren<Transform>(true);
            foreach (var child in children)
            {
                if (targetNameRegex.IsMatch(child.gameObject.name))
                {
                    var isExclude = true;
                    if (layerIndex != -1)
                    {
                        if (child.gameObject.layer != layerIndex) isExclude = false;
                    }
                    if (!string.IsNullOrEmpty(targetTag))
                    {
                        if (child.gameObject.tag != targetTag) isExclude = false;
                    }
                    if (!isExclude) continue;
                    resultList.Add(child.gameObject);
                }
            }
            return resultList;
        }

        public static ValueDropdownList<GameObject> GetTargetPartsList(ProjectSettingsData projectSettingsData, GameObject targetObj)
        {
            var list = new ValueDropdownList<GameObject>();
            if (targetObj == null) return list;
            list.Add("Root", targetObj);
            var children = targetObj.GetComponentsInChildren<Transform>(true);
            foreach (var child in children)
            {
                foreach (var targetParts in projectSettingsData.targetPartsList)
                {
                    GetTargetPartsChildren(list, targetParts, child);
                }
            }
            DistinctList(list);
            return list;
        }

        static ValueDropdownList<GameObject> GetTargetPartsChildren(ValueDropdownList<GameObject> list, ProjectSettingsData.TargetPartsData targetParts, Transform child)
        {
            foreach (var targetName in targetParts.targetNameList)
            {
                var targetNameRegex = new Regex(targetName);
                if (targetNameRegex.IsMatch(child.gameObject.name))
                {
                    list.Add(targetParts.displayName, child.gameObject);
                }
            }
            return list;
        }

        static void DistinctList(ValueDropdownList<GameObject> list)
        {
            var distinctList = new ValueDropdownList<GameObject>();
            var addedObjList = new List<GameObject>();
            foreach (var item in list)
            {
                if (!addedObjList.Contains(item.Value))
                {
                    distinctList.Add(item.Text, item.Value);
                    addedObjList.Add(item.Value);
                }
            }
            list.Clear();
            foreach (var item in distinctList)
            {
                list.Add(item.Text, item.Value);
            }
        }
    }
}

