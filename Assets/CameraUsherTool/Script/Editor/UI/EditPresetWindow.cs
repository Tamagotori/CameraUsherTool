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
            Selection.activeObject = presetData;
        }

        [TitleGroup("プリセット作成", order: 10)]
        [Button("階層データ作成")]
        void CreatePresetHierarchyData()
        {
            var hierarchyData = CameraUsherTool.CreatePresetHierarchyData();
            Selection.activeObject = hierarchyData;
        }

        [TitleGroup("プリセット編集", order: 20)]
        [LabelText("")]
        [InlineProperty]
        public UIParts.UIElementSearchPreset searchPresetData;

        void Awake()
        {
            CameraUsherToolUtil.searchPathPivot = this;
        }
        void Update()
        {
            CameraUsherToolUtil.searchPathPivot = this;
        }

        [TitleGroup("カット編集", order: 30)]
        [LabelText("")]
        [InlineProperty]
        public UIParts.UIElementCutEditor cutEditor;
    }
}

