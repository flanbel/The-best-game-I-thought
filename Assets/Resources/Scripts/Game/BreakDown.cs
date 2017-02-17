using UnityEngine;
using System.Collections;
//壊れるオブジェクトにつけてね。
public class BreakDown : MonoBehaviour {
    //壊れた後に差し替えるオブジェクト
    [SerializeField]
    private GameObject BrokenObjet;
    //壊れたときに出るパーティクル
    [SerializeField]
    private ParticleSystem Particle;

    //あたり判定と当たった。
    public void OnTriggerEnter(Collider coll)
    {
        //プレイヤーの攻撃なら
        if (coll.gameObject.tag == "Player_Damage_Collision")
        {
            GameObject after;
            //差し替え
            if (BrokenObjet)
            {
                //作成
                after = Instantiate(BrokenObjet);
                //同じ位置に
                after.transform.position = transform.position;
                after.transform.localScale = transform.localScale;
                DeleteTimer dt = after.AddComponent<DeleteTimer>();
                dt.lifetime = 2.0f;
                //パーティクル                
                if (Particle)
                {
                    GameObject p = Instantiate(Particle.gameObject);
                    p.transform.position = after.transform.position;
                    dt = p.AddComponent<DeleteTimer>();
                    dt.lifetime = 2.0f;
                }

                //もしドロップアイテムが設定されていたなら
                DropItems di;
                if (di =GetComponent<DropItems>())
                {
                    di.ItemDrop();
                }
            }
            //削除
            GameObject.Destroy(this.gameObject);
        }
    }
}
