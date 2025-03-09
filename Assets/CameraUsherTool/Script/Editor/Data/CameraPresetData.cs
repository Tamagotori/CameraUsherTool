using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace tamagotori.lib.CameraUsherTool
{
    public class CameraPresetData : PresetDataBase
    {
        [Multiline(3)]
        [LabelText("メモ欄")]
        public string description;

        [LabelText("指定想定パーツ名")]
        public string expectedPartName;
    }
}
