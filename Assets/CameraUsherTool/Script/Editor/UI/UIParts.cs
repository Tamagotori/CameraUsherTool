using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

namespace tamagotori.lib.CameraUsherTool
{
    public class ToolWindow : OdinEditorWindow
    {

    }

    public class UIParts
    {
        [System.Serializable]
        public class SearchPresetData
        {
            [LabelText("プリセットデータ")]
            [HideInInspector]
            public ToolWindow currentWindow;
            public CameraPresetData currentPresetData;
            [ValueDropdown(nameof(GetGroupNameList))]
            public string searchGroupName;
            public string searchPresetWord;
            [Button("検索")]
            void Search()
            {

            }

            List<string> GetGroupNameList()
            {
                var list = new List<string>() { "All" };
                if (currentWindow == null) return list;
                var presetDataList = CameraUsherToolUtil.GetPresetDataList(currentWindow);
                foreach (var data in presetDataList)
                {
                    if (!list.Contains(data.groupName)) list.Add(data.groupName);
                }
                return list;
            }
        }
    }

}
