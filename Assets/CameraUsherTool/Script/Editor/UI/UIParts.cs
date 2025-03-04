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
            public CameraPresetData currentPresetData;
            [BoxGroup("プリセット検索")]
            [ValueDropdown(nameof(GetGroupNameList))]
            public string searchGroupName;
            [BoxGroup("プリセット検索")]
            public string searchPresetWord;
            [BoxGroup("プリセット検索")]
            [Button("検索")]
            void Search()
            {

            }

            List<string> GetGroupNameList()
            {
                return CameraUsherToolUtil.GetGroupNameList(true);
            }
        }
    }

}
