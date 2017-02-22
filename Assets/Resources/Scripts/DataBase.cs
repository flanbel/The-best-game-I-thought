using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//武器とかクエストとかのデータ管理
public class DataBase {
    private WeaponsInfo WeaponsInfo;
    public Weapon[] weapons { get { return WeaponsInfo.weapons; } }
    private ArmorsInfo ArmorsInfo;
    public Armor[] armors { get { return ArmorsInfo.armors; } }

    private static DataBase instance;

    public static DataBase Instance()
    {
        if (instance == null)
        {
            instance = new DataBase();
            instance.WeaponsInfo = Resources.Load("Data/WeaponsInfo") as WeaponsInfo; ;
            instance.ArmorsInfo = Resources.Load("Data/ArmorsInfo") as ArmorsInfo; ;
        }
        return instance;
    }
}
