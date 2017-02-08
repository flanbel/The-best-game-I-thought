using UnityEngine;
using System.Collections;
//エネミークラス(たぶん基底クラス)
public class Enemy : Character
{
    //ずっと変わらず同じものを使うのでstatic指定
    //クエストマネージャー
    static private QuestManager QuestM;
    //プレイヤーのコンポーネント
    static Player player;
    //経験値プレハブ
    static GameObject EXPPrfab;


    //消えるまでの時間
    [SerializeField]
    private float FadeTime = 10.0f;
    private float time = 0.0f;
    //落ちる経験値
    [SerializeField]
    private int DropExp = 0;

    // Use this for initialization
    void Start()
    {
        DamageCollisionTag = "Player_Damage_Collision";
        //最大HPに揃える
        Parm.hp = Parm.mhp;
        if (!player)
            player = GameObject.Find("Player").GetComponent<Player>();
        if (!EXPPrfab)
            EXPPrfab = Resources.Load("Prefab/EXP_Particle") as GameObject;
        //クエストマネージャー取得
        if (!QuestM)
        {
            QuestM = GameObject.Find("QuestManager").GetComponent<QuestManager>();
        }

    }
	
	// Update is called once per frame
	void Update () {
        Fade();
	}

    protected override void Move() { }
    protected override void Attack() { }
    protected override void Death()
    {
        //経験値の追加
        player.AddExp(DropExp);
        //経験値の生成(オブジェクト)
        GameObject expO = Instantiate(EXPPrfab);
        expO.transform.SetParent(player.transform,false);
        //アイテムのドロップ
        DropItems di;
        if(di = GetComponent<DropItems>())
        {
            di.ItemDrop();
        }
        //削除フラグを建てる。
        IsDied = true;
        QuestM.Check(this);
        //コリジョン削除
        GetComponent<Collider>().enabled = false;
    }

    void Fade()
    {
        //死んでいるなら
        if (IsDied)
        {
            time += Time.deltaTime;
            float persent = 1.0f - (time / FadeTime);
            Color c = GetComponent<MeshRenderer>().material.color;
            c.a = persent;
            GetComponent<MeshRenderer>().material.color = c;
            //時間経過したなら
            if(time >= FadeTime)
            {
                //完全消滅
                GameObject.Destroy(this.gameObject);
            }
        }
    }

    protected override void CreateAttackCollision(float lifetime = 5.0f)
    {
        //プレハブから攻撃判定をインスタンス化
        GameObject coll = Instantiate(DCPrefab);
        //タグの設定
        coll.tag = "Enemy_Damage_Collision";
        //寿命設定
        coll.GetComponent<DeleteTimer>().lifetime = lifetime;
        //ダメージ設定
        coll.GetComponent<DamageCollision>().damage = AttackCalculation();
    }
}
