using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//ステータスを表示する
public class ShowStatus : MonoBehaviour {
    void Start()
    {
        //装備後のパラメータ取得
        CharacterParameters p = SaveData.GetClass(SaveDataName.AfterParameter.ToString(),new CharacterParameters());
        PowerOrb Orb = SaveData.GetClass(SaveDataName.PlayerOrb.ToString(), new PowerOrb());
        Image OrbImage = GameObject.Find("PowerOrb").GetComponent<Image>();
        Text Te = transform.GetChild(0).GetComponent<Text>();
        string HP, ATK, DEF, LUK, SPD,ORB;
        HP = "HP:" + p.mhp.ToString() + "\n";
        ATK = "ATK:" + p.atk.ToString() + "\n";
        DEF = "DEF:" + p.def.ToString() + "\n";
        LUK = "LUK:" + p.luk.ToString()  + "\n";
        SPD = "SPD:" + p.spd.ToString()  + "\n";
        ORB = "ORB:" + Orb.now.ToString() + "/" + Orb.max.ToString() + "\n";
        OrbImage.fillAmount = (float)Orb.now / (float)Orb.max;
        Te.text = HP + ATK + DEF + LUK + SPD + ORB;
    }
}
