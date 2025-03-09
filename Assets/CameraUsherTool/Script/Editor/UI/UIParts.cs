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
        public class PresetGroupData
        {

            [BoxGroup("プリセット検索")]
            [ValueDropdown(nameof(GetGroupNameList))]
            [LabelText("グループ名")]
            public string searchGroupName;
            [BoxGroup("プリセット検索")]
            [LabelText("カット名")]
            [ValueDropdown(nameof(GetCutNameList))]
            public string searchPresetName;

            List<string> GetGroupNameList()
            {
                return CameraUsherToolUtil.GetGroupNameList(true);
            }

            List<string> GetCutNameList()
            {
                return CameraUsherToolUtil.GetCutNameList(searchGroupName, true);
            }

        }

        [System.Serializable]
        public class SearchPresetData
        {
            [LabelText("")]
            [InlineProperty]
            public PresetGroupData presetGroupData;

            [LabelText("プリセットデータ")]
            //[ValueDropdown(nameof(GetPresetDataList))]
            [InlineProperty]
            public CameraPresetData currentPresetData;

            List<CameraPresetData> GetPresetDataList()
            {
                return CameraUsherToolUtil.GetPresetDataList(presetGroupData.searchGroupName, presetGroupData.searchPresetName);
            }
        }
    }

}
