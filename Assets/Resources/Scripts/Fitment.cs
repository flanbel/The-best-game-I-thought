using UnityEngine;
using System.Collections;
//装備の基底クラス
[System.Serializable]
public class Fitment
{
    //装備タイプ
    public enum FITTYPE
    {
        NONE = -1,      //装備していない
        WEAPON = 0,     //武器
        ARMOR,          //鎧
    }
    [SerializeField]
    private FITTYPE FitType = FITTYPE.NONE;
    [SerializeField]
    private int ID = -1;
    [SerializeField]
    private string Name = "none";
    //装備中(仮)
    bool Equip = false;
}

//武器クラス
[System.Serializable]
public class Weapon : Fitment
{
    //武器タイプ
    public enum WEAPONTYPE
    {
        NONE = -1,  //未設定
        SWORD = 0,  //剣
    }
    //武器タイプ
    [SerializeField]
    private WEAPONTYPE WeaponType = WEAPONTYPE.NONE;
    //攻撃力
    [SerializeField]
    private int ATK = 0;
}

//防具クラス
[System.Serializable]
public class Armor : Fitment
{
    //防御力
    [SerializeField]
    private int DEF = 0;
}