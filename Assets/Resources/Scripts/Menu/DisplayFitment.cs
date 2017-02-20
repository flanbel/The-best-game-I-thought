using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
//装備品表示

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
        List<Fitment> FitList = SaveData.GetClass(SaveDataName.PlayerInventory.ToString(), new Inventory()).fitlist;
        foreach (var fit in FitList)
        {
            //装備タイプが同じもの
            if(fit.fitType == FitType)
            {
                //リストに格納
                DisplayList.Add(fit);
                //表示用のオブジェクト作成
                GameObject Display = Instantiate(Resources.Load("Prefab/Menu/EquipButton") as GameObject);
                Display.GetComponent<EquipButton>().fit = fit;
            }
        }
        
    }
}
