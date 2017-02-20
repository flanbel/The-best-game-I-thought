using UnityEngine;
using System.Collections;
//武器クラス(武器の情報)
public class WeaponsInfo : ScriptableObject
{
    //JSON用中間クラス
    public class Json
    {
        public Weapon[] weapons;
    }
    public Weapon[] weapons;
}