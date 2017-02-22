using UnityEngine;
using System.Collections.Generic;
using System.Collections;
//インベントリクラス
[System.Serializable]
public class Inventory {
    //持っている装備品リスト
    [SerializeField]
    List<Fitment> FitList = new List<Fitment>();
    public List<Fitment> fitlist { get { return FitList; } }
    //最大保有数
    [SerializeField]
    int MaxPossessionNum = 100;
    //装備品追加
    public void AddFit<T>(T fit) where T : Fitment
    {
        //所持限界に達していない
        if (FitList.Count < MaxPossessionNum)
        {
            //リストに追加
            FitList.Add(fit);
        }
        //所持限界
        else
        {
            //持てないよー
        }
    }
}
