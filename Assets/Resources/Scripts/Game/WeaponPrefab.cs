using UnityEngine;
using System.Collections;

public class WeaponPrefab : MonoBehaviour {

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
