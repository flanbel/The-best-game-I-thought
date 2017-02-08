using UnityEngine;
using System.Collections;
//ダメージ判定
public class DamageCollision : MonoBehaviour {
    [SerializeField]
    private float Damage = 1.0f;
    private bool IsCritical = false;
    public float damage { get { return Damage; } set { Damage = value; } }
    public bool crit { get { return IsCritical; } set { IsCritical = value; } }
}
