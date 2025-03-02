using UnityEditor;
using UnityEngine;
using System.IO;

namespace tamagotori.lib.CameraUsherTool
{
    public class CameraUsherToolUtil
    {
        public static string GetToolRootPath(EditorWindow window)
        {
            var windowPath = AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(window));
            var dirInfo = new DirectoryInfo(windowPath);
            while (dirInfo.Name != "CameraUsherTool")
            {
                if (dirInfo.Parent == null) return null;
                dirInfo = dirInfo.Parent;
            }
            var path = dirInfo.FullName;
            path = path.Replace("\\", "/");
            path = path.Split("/Assets/")[1];
            return $"Assets/{path}";
        }
    }
}
