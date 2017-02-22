using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
//装備品を表示するボタンを作り出す
public class DisplayFitment : MonoBehaviour {
    //装備タイプ
    public Fitment.FITTYPE FitType;
    //表示する装備のリスト
    public List<Fitment> DisplayList;


    void Awake()
    {
        DisplayList = new List<Fitment>();
    }
    //表示するボタン作成
    public void CreateDisplayFitment()
    {
        //装備品リスト取得
        Inventory I = SaveData.GetClass(SaveDataName.PlayerInventory.ToString(), new Inventory());
        I.fitlist.Clear();
        
        foreach (Weapon w in DataBase.Instance().weapons)
        {
            //アイテム追加
            I.AddFit(w);
        }
        foreach (Armor a in DataBase.Instance().armors)
        {
            //アイテム追加
            I.AddFit(a);
        }
        SaveData.SetClass(SaveDataName.PlayerInventory.ToString(), I);
        SaveData.Save();

        List<Fitment> FitList = SaveData.GetClass(SaveDataName.PlayerInventory.ToString(), new Inventory()).fitlist;
        foreach (var fit in FitList)
        {
            //装備タイプが指定したモノ
            if ((fit.fitType & FitType) > 0x00)
            {
                //リストに格納
                DisplayList.Add(fit);
                //表示用のオブジェクト作成
                GameObject Display = Instantiate(Resources.Load("Prefab/Menu/EquipList") as GameObject);
                //装備を指定
                Display.GetComponent<EquipList>().fit = fit;
            }
        }
        
    }
}
