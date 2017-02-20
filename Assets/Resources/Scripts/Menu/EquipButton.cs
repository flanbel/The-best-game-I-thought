using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
//装備ボタン
public class EquipButton : MonoBehaviour {
    //装備の情報
    [SerializeField]
    private Fitment Fit;
    public Fitment fit { set { Fit = value; } }

    void Awake()
    {
        GameObject View = GameObject.Find("ScrollView");
        transform.SetParent(View.transform);
    }

    void Start()
    {
        transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = Fit.name;
    }

    //装備する
    public void Equip()
    {
        Equipment Equip = SaveData.GetClass(SaveDataName.PlayerInventory.ToString(), new Equipment());
        switch (Fit.fitType)
        {
            case Fitment.FITTYPE.WEAPON:
                Equip.weapon = (Weapon)Fit;
                break;
            case Fitment.FITTYPE.ARMOR:
                Equip.armor = (Armor)Fit;
                break;
            default:
                break;
        }
        //セーブ
        SaveData.SetClass("PlayerEquipment", Equip);
        SaveData.Save();
    }
}
