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
        [ValueDropdown(nameof(GetTargetPartsNameList))]
        public string expectedPartName;

        List<string> GetTargetPartsNameList()
        {
            var dataList = CameraUsherToolUtil.GetTargetPartsDataList();
            var list = new List<string>();
            foreach (var data in dataList)
            {
                list.Add(data.displayName);
            }
            return list;
        }
    }
}
