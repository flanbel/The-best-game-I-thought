using UnityEngine;
using System.Collections;
//回復ポーションのクラス
public class HealPotion : MonoBehaviour {
    //回復レート
    [SerializeField, Range(0, 100)]
    private float HealRate = 0;
	
	void Start () {
        
    }
		
    //プレイヤーの体力回復
    void Heal(GameObject obj)
    {
       Player p = obj.GetComponent<Player>();
        if (p)
        {
            p.Heal(HealRate / 100.0f, Character.HEALTYPE.RATE);
            p.UpdateHPbar();
        }
    }

    public void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            //回復処理
            Heal(coll.gameObject);
            
            //オブジェクト削除
            GameObject.Destroy(gameObject);
        }

    }
}
