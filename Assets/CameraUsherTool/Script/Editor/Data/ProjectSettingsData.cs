using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace tamagotori.lib.CameraUsherTool
{
    public class ProjectSettingsData : ScriptableObject
    {
        [FoldoutGroup("ターゲット設定", order: 20)]
        [LabelText("Unityレイヤー名")]
        [Tooltip("指定されたレイヤーに所属するオブジェクトをターゲットとして扱います。\n未指定の場合は全てのレイヤーが対象となります。")]
        public string targetLayer;
        [FoldoutGroup("ターゲット設定")]
        [LabelText("Unityタグ名")]
        [Tooltip("指定されたタグを持つオブジェクトをターゲットとして扱います。\n未指定の場合は全てのタグが対象となります。")]
        public string targetTag;
        [FoldoutGroup("ターゲット設定")]
        [LabelText("オブジェクト検索リスト")]
        [Tooltip("【設定必須項目】\n正規表現で一致したオブジェクトを対象として扱います。")]
        public List<string> targetNameList = new List<string>();
        [FoldoutGroup("ターゲット設定")]
        [LabelText("パーツ検索リスト")]
        [Tooltip("【設定必須項目】\n正規表現で一致したパーツを対象として扱います。")]
        public List<TargetPartsData> targetPartsList = new List<TargetPartsData>();


        [System.Serializable]
        public class TargetPartsData
        {
            [LabelText("識別ID名")]
            public string name;
            [LabelText("表示パーツ名")]
            public string displayName;
            [LabelText("検索ルール(正規表現)")]
            public List<string> searchConditionList = new List<string>();
        }
    }
}

