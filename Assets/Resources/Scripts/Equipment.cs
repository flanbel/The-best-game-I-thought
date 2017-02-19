using UnityEngine;
using System.Collections;
//装備しているもの
[System.Serializable]
public class Equipment
{
    //装備している武器
    [SerializeField]
    private Weapon weapon;
    //装備している防具
    [SerializeField]
    private Armor armor;
}