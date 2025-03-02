using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace tamagotori.lib.CameraUsherTool
{
    public class ProjectSettingsData : ScriptableObject
    {
        [FoldoutGroup("ターゲット設定", order: 10)]
        [LabelText("レイヤー名")]
        public string targetLayer;
        [FoldoutGroup("ターゲット設定")]
        [LabelText("タグ名")]
        public string targetTag;
        [FoldoutGroup("ターゲット設定")]
        [LabelText("名前ルール(正規表現)")]
        public List<string> targetNameList = new List<string>();
        [FoldoutGroup("ターゲット設定")]
        [LabelText("パーツ名ルール")]
        public List<TargetPartsData> targetPartsList = new List<TargetPartsData>();

        [System.Serializable]
        public class TargetPartsData
        {
            [LabelText("表示パーツ名")]
            public string displayName;
            [LabelText("名前ルール(正規表現)")]
            public List<string> targetNameList = new List<string>();
        }
    }
}

