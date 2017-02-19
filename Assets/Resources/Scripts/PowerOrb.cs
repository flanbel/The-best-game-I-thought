using UnityEngine;
using System.Collections;
//オーブクラス
[System.Serializable]
public class PowerOrb {
    //最大値
    [SerializeField]
    private int Max = 0;
    public int max { get { return Max; } }
    //現在値
    [SerializeField]
    private int Now = 0;
    public int now { get { return Now; } }
    //オーブの色
    [SerializeField]
    private Color color = new Color(1, 1, 1, 0.5f);
    //経験値加算
    public void AddExp(int add)
    {
        //Maxより大きくならないように
        Now = Mathf.Min(Now + add, Max);
    }
}
