using Sirenix.OdinInspector;
using UnityEngine;

namespace tamagotori.lib.CameraUsherTool
{
    public class CameraPresetData : ScriptableObject
    {
        [Multiline(3)]
        [LabelText("メモ欄")]
        public string description;
        [LabelText("グループ名")]
        public string groupName;
        [LabelText("プリセット名")]
        public string presetName;
        [LabelText("指定想定パーツ名")]
        public string expectedPartName;
    }
}
