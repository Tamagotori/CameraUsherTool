using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace tamagotori.lib.CameraUsherTool
{
    public class CameraPresetData : ScriptableObject
    {
        [Multiline(3)]
        [LabelText("メモ欄")]
        public string description;
        [LabelText("グループ名")]
        [ValueDropdown(nameof(GetGroupNameList))]
        public string groupName;
        [LabelText("プリセット名")]
        public string presetName;
        [LabelText("指定想定パーツ名")]
        public string expectedPartName;

        List<string> GetGroupNameList()
        {
            CameraUsherToolUtil.searchPathPivot = this;
            return CameraUsherToolUtil.GetGroupNameList(false);
        }
    }
}
