using UnityEngine;
using System.Collections;

//防具の情報
public class ArmorsInfo : ScriptableObject
{
    //JSON用中間クラス
    public class Json
    {
        public Armor[] armors;
    }
    public Armor[] armors;
}
