using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

namespace tamagotori.lib.CameraUsherTool
{
    public class EditPresetWindow : ToolWindow
    {
        [MenuItem("Tools/CameraUsherTool/カメラプリセット作成_編集")]
        private static void OpenWindow()
        {
            var window = GetWindow<EditPresetWindow>();
            window.titleContent = new GUIContent("カメラプリセット作成/編集");
            window.Show();
        }

        [TitleGroup("プリセット作成", order: 10)]
        [Button("プリセットデータ作成")]
        void CreatePresetData()
        {
            var presetData = CameraUsherTool.CreatePresetData();
            searchPresetData.currentPresetData = presetData;
        }

        [TitleGroup("プリセット編集", order: 20)]
        [LabelText("")]
        [InlineProperty]
        public UIParts.SearchPresetData searchPresetData;

        void Awake()
        {
            CameraUsherToolUtil.searchPathPivot = this;
        }
        void Update()
        {
            CameraUsherToolUtil.searchPathPivot = this;
        }
    }
}

