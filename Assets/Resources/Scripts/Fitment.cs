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
        WEAPON = 1,     //武器
        ARMOR = 2,      //鎧
        ALL = 3,        //全て
    }
    [SerializeField]
    protected FITTYPE FitType = FITTYPE.NONE;
    public FITTYPE fitType { get { return FitType; } }
    //装備のID、データを参照するときに使う
    [SerializeField]
    protected int ID = -1;
    public int id { get { return ID; } }
    //装備の名前
    [SerializeField]
    protected string Name = "NULL";
    public string name { get { return Name; } }
    //装備の説明
    [SerializeField, TextAreaAttribute(2, 5)]
    protected string Text = "NONE";
    public string text { get { return Text; } }
    //体力
    [SerializeField]
    private int HP = 0;
    public int hp { get { return HP; } }
    //攻撃力
    [SerializeField]
    private int ATK = 0;
    public int atk { get { return ATK; } }
    //防御力
    [SerializeField]
    private int DEF = 0;
    public int def { get { return DEF; } }
    //敏捷
    [SerializeField]
    private int SPD = 0;
    public int spd { get { return SPD; } }
    //運
    [SerializeField]
    private int LUK = 0;
    public int luk { get { return LUK; } }
    //装備中
    [SerializeField]
    protected bool Equip = false;
    public bool equip { get { return Equip; }set { Equip = value; } }

    //値コピー
    public virtual void ToCopy(Fitment fit)
    {
        this.FitType = fit.FitType;
        this.ID = fit.ID;
        this.Name = fit.Name;
        this.Text = fit.Text;
        this.Equip = fit.Equip;
        this.HP = fit.HP;
        this.ATK = fit.ATK;
        this.DEF = fit.DEF;
        this.LUK = fit.LUK;
        this.SPD = fit.SPD;
    }
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

    public override void ToCopy(Fitment fit)
    {
        base.ToCopy(fit);
        //仮
        this.WeaponType = WEAPONTYPE.SWORD;
    }
}

//防具クラス
[System.Serializable]
public class Armor : Fitment
{
   
}