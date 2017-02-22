using UnityEngine;
using System.Collections;
//装備しているもの
[System.Serializable]
public class Equipment
{
    //装備している武器
    [SerializeField]
    private Weapon EquipWeapon;
    public Weapon weapon { get { return EquipWeapon; }set { EquipWeapon = value; } }
    //装備している防具
    [SerializeField]
    private Armor EquipArmor;
    public Armor armor { get { return EquipArmor; } set { EquipArmor = value; } }

    //装備する関数
    public void Equip<T>(T fit,Fitment.FITTYPE type)where T: Fitment
    {
        switch (type)
        {
            case Fitment.FITTYPE.WEAPON:
                //前の装備を外す
                EquipWeapon.equip = false;
                //値をコピー
                EquipWeapon.ToCopy(fit);
                //新しいのを装備
                EquipWeapon.equip = true;
                break;
            case Fitment.FITTYPE.ARMOR:
                EquipArmor.equip = false;
                EquipArmor.ToCopy(fit);
                EquipArmor.equip = true;
                break;
            default:
                break;
        }
    }
}