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
}