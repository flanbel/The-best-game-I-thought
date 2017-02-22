using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//今装備している武器とこれから装備する武器の比較を表示

public class ShowStatusComp : MonoBehaviour {
    //表示用のテキスト
    [SerializeField]
    private Text Status, Text;
    //変更する装備
    private Fitment Fit;
    public Fitment fit { set { Fit = value; UpdateDisplay(); } }

    void Start()
    {
        CharacterParameters p = SaveData.GetClass(SaveDataName.PlayerParameter.ToString(), new CharacterParameters());
        Equipment Equipment = SaveData.GetClass(SaveDataName.PlayerEquipment.ToString(), new Equipment());
        Fitment w = Equipment.weapon;
        Fitment a = Equipment.armor;

        string HP, ATK, DEF, LUK, SPD;
        HP = "HP:" + (p.mhp + w.hp + a.hp).ToString() + "\n";
        ATK = "ATK:" + (p.atk + w.atk + a.atk).ToString() + "\n";
        DEF = "DEF:" + (p.def + w.def + a.def).ToString() + "\n";
        LUK = "LUK:" + (p.luk + w.luk + a.luk).ToString() + "\n";
        SPD = "SPD:" + (p.spd + w.spd + a.spd).ToString() + "\n";
        Status.text = HP + ATK + DEF + LUK + SPD;
        Text.text = "";
    }
    //テキスト描画更新
    void UpdateDisplay()
    {
        CharacterParameters p = SaveData.GetClass(SaveDataName.PlayerParameter.ToString(), new CharacterParameters());
        Equipment Equipment = SaveData.GetClass(SaveDataName.PlayerEquipment.ToString(), new Equipment());
        Fitment w = Equipment.weapon;
        Fitment a = Equipment.armor;

        Fitment Nw = Equipment.weapon;
        Fitment Na = Equipment.armor;
        switch (Fit.fitType)
        {
            case Fitment.FITTYPE.WEAPON:
                Nw = Fit;
                break;
            case Fitment.FITTYPE.ARMOR:
                Na = Fit;
                break;
            default:
                break;

        }

        string HP, ATK, DEF, LUK, SPD;
        int hp, atk, def, luk, spd;
        hp = (int)p.mhp + Nw.hp + Na.hp;
        atk = (int)p.atk + Nw.atk + Na.atk;
        def = (int)p.def + Nw.def + Na.def;
        luk = (int)p.luk + Nw.luk + Na.luk;
        spd = (int)p.spd + Nw.spd + Na.spd;

        HP = "HP:" + (p.mhp + w.hp + a.hp).ToString() + " → " + hp.ToString() + "\n";
        ATK = "ATK:" + (p.atk + w.atk + a.atk).ToString() + " → " + atk.ToString() + "\n";
        DEF = "DEF:" + (p.def + w.def + a.def).ToString() + " → " + def.ToString() + "\n";
        LUK = "LUK:" + (p.luk + w.luk + a.luk).ToString() + " → " + luk.ToString() + "\n";
        SPD = "SPD:" + (p.spd + w.spd + a.spd).ToString() + " → " + spd.ToString() + "\n";
        Status.text = HP + ATK + DEF + LUK + SPD;
        Text.text = Fit.text;

        CharacterParameters CP = new CharacterParameters();
        CP.hp = CP.mhp = hp;
        CP.atk = atk;
        CP.def = def;
        CP.luk = luk;
        CP.spd = spd;
        SaveData.SetClass(SaveDataName.AfterParameter.ToString(), CP);
        SaveData.Save();
    }
}
