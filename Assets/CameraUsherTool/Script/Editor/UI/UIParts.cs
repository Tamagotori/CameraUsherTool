using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;

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

            ValueDropdownList<string> GetGroupNameList()
            {
                return CameraUsherToolUtil.GetGroupNameItemList(true);
            }

            ValueDropdownList<string> GetCutNameList()
            {
                return CameraUsherToolUtil.GetCutNameItemList(searchGroupName, true);
            }
        }

        [System.Serializable]
        public class SearchPresetData
        {
            [LabelText("")]
            [InlineProperty]
            public PresetGroupData presetGroupData;

            [LabelText("プリセットデータ")]
            [ValueDropdown(nameof(GetPresetDataList))]
            [OnValueChanged(nameof(OnValidateSelectPresetData))]
            public CameraPresetData selectPresetData;
            ValueDropdownList<CameraPresetData> GetPresetDataList()
            {
                return CameraUsherToolUtil.LoadDataItemList<CameraPresetData>(
                    presetGroupData.searchGroupName,
                    presetGroupData.searchPresetName,
                    CameraUsherToolUtil.NameType.FileName
                );
            }

            void OnValidateSelectPresetData()
            {
                if (selectPresetData == null)
                {
                    return;
                }
                currentPresetData = selectPresetData;
            }

            [ShowInInspector]
            [InlineEditor(InlineEditorObjectFieldModes.CompletelyHidden)]
            CameraPresetData currentPresetData;

        }
    }

}
