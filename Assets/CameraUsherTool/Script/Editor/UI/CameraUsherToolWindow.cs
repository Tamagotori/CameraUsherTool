using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace tamagotori.lib.CameraUsherTool
{
    public class CameraUsherToolWindow : ToolWindow
    {

        [MenuItem("Tools/CameraUsherTool/カメラプリセット")]
        private static void OpenWindow()
        {
            var window = GetWindow<CameraUsherToolWindow>();
            window.titleContent = new GUIContent("カメラプリセット(CUTツール)");
            window.Show();
        }

        [SerializeField]
        [TitleGroup("設定", order: 10)]
        [LabelText("プロジェクト設定")]
        public ProjectSettingsData projectSettingsData;

        [TitleGroup("プリセット", order: 20)]
        [LabelText("")]
        [InlineProperty]
        public UIParts.SearchPresetData searchPresetData;

        [TitleGroup("ターゲット", order: 30)]
        [LabelText("ターゲット名")]
        [DisableIf(nameof(IsForceTargetObj))]
        [ValueDropdown(nameof(GetTargetObjList))]
        public GameObject targetObj;

        ValueDropdownList<GameObject> GetTargetObjList()
        {
            return CameraUsherTool.GetTargetList(projectSettingsData);
        }

        [TitleGroup("ターゲット")]
        [LabelText("パーツ名")]
        [DisableIf(nameof(IsForceTargetObj))]
        [ValueDropdown(nameof(GetTargetPartsObjList))]
        public GameObject targetPartsObj;
        ValueDropdownList<GameObject> GetTargetPartsObjList()
        {
            return CameraUsherTool.GetTargetPartsList(projectSettingsData, targetObj);
        }

        [TitleGroup("ターゲット")]
        [LabelText("直接指定ターゲット")]
        public GameObject forceTargetObj;

        bool IsForceTargetObj()
        {
            return forceTargetObj != null;
        }

        private void Update()
        {
            CameraUsherTool.SetupProjectSettings(this);
            searchPresetData.currentWindow = this;
        }
    }

}
