using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Linq;
using UnityEditor;

namespace tamagotori.lib.CameraUsherTool
{
    public class ToolWindow : OdinEditorWindow
    {

    }

    public class UIParts
    {

        [System.Serializable]
        public class UIElementPresetGroup
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
        public class UIElementSearchPreset
        {
            [LabelText("")]
            [InlineProperty]
            public UIElementPresetGroup presetGroupData;

            [LabelText("プリセットデータ")]
            [ValueDropdown(nameof(GetPresetDataList))]
            [OnValueChanged(nameof(OnValidateSelectPresetData))]
            public CameraPresetData selectPresetData;
            ValueDropdownList<CameraPresetData> GetPresetDataList()
            {
                return CameraUsherToolUtil.LoadDataItemList<CameraPresetData>(
                    presetGroupData.searchGroupName,
                    presetGroupData.searchPresetName,
                    CameraUsherToolUtil.NameType.FileAndDisplayName
                );
            }

            void OnValidateSelectPresetData()
            {
                if (selectPresetData == null)
                {
                    return;
                }
                currentPresetData = selectPresetData;
                CurrentPresetData = selectPresetData;
            }

            [ShowInInspector]
            [InlineEditor(InlineEditorObjectFieldModes.CompletelyHidden)]
            CameraPresetData currentPresetData;
            public static CameraPresetData CurrentPresetData;
        }

        [System.Serializable]
        public class UIElementCutEditor
        {
            [OnValueChanged(nameof(OnValueChangeTargetList), includeChildren: true)]
            [ValueDropdown(nameof(GetTargetList))]
            public List<GameObject> targetList = new List<GameObject>();

            List<GameObject> GetTargetList()
            {
                return CameraUsherToolUtil.GetTargetList();
            }
            public void OnValueChangeTargetList()
            {
                var isRemove = false;
                var max = 1;
                while (targetList.Count > max)
                {
                    targetList.Remove(targetList.Last());
                    isRemove = true;
                }
                if (isRemove)
                {
                    EditorUtility.DisplayDialog("確認", $"ターゲット指定できる数は{max}までです。", "OK");
                }
            }
        }
    }

}
