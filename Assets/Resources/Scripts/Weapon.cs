using UnityEngine;
using System.Collections;
//武器クラス
public class Weapon : MonoBehaviour {
    //武器タイプ
    public enum WEAPONTYPE
    {
        NONE = -1,
        SWORD = 0,
    }
    [SerializeField]
    private WEAPONTYPE WeaponType = WEAPONTYPE.NONE;
    //武器ID、-1は未設定
    private int WeaponID = -1;
    //あたり判定をだす場所
    [SerializeField]
    private GameObject CollisionPos = null;
    //あたり判定のサイズ
    [SerializeField]
    private Vector3 Size = Vector3.one;
    //げったー
    public GameObject collpos { get { return CollisionPos; } }
    public Vector3 size { get { return Size; } }

}
