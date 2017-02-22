using UnityEngine;
using System.Collections;

public class EquipButton : MonoBehaviour {
    private Fitment Fit;
    public Fitment fit { set { Fit = value; } }

    //装備する
    public void Equip()
    {
        Equipment Equip = SaveData.GetClass(SaveDataName.PlayerEquipment.ToString(), new Equipment());
        //装備させる
        Equip.Equip(Fit, Fit.fitType);
        //セーブ
        SaveData.SetClass(SaveDataName.PlayerEquipment.ToString(), Equip);
        SaveData.Save();
    }
}
