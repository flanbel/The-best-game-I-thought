using UnityEngine;
using System.Collections;
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Searching))]
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
    private float FadeTime = 3.0f;
    private float Ftime = 0.0f;
    //[SerializeField]
    //private SkinnedMeshRenderer[] MeshRenders;
    //落ちる経験値
    [SerializeField]
    private int DropExp = 0;
    //アニメーター
    private Animator Anim;
    //ナビゲーションエージェント
    private NavMeshAgent Agent;
    //索敵
    private Searching Search;
    //発見している
    private bool Found;
    
    //見てから経過した時間(見失うまでの時間)
    [SerializeField]
    float Stime = 0.0f;
    

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
        Audio = GetComponent<AudioSource>();
        Anim = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        Search = GetComponent<Searching>();
        //スピード計算
        Agent.speed = SpeedCalculation();
    }
	
	// Update is called once per frame
	void Update () {
        if (!IsDied)
        {
            SearchPlayer();
            Move();
            Attack();
        }
        //死んでいるなら
        else
        {
            Fade();
        }
	}

    void SearchPlayer()
    {
        if (Found == false &&
               Search.isFound == true)
        {
            //見つけた瞬間の処理
            Audio.PlayOneShot(Resources.Load("Sounds/Found") as AudioClip);
            GameObject mark = Instantiate(Resources.Load("Prefab/ActionMark") as GameObject);
            mark.GetComponent<MeshRenderer>().material = Resources.Load("Materials/bikkuri") as Material;
            mark.transform.position = this.transform.TransformPoint(0, 2, 0);
        }
        

        //発見した
        if (Search.isFound)
        {
            //とりあえず定数設定
            Stime = 3.0f;
            Found = true;
        }
        else
        {
            //時間減少(見失う)
            Stime -= Time.deltaTime;
        }

        //見つけているかどうか
        if (Stime > 0.0f)
        {
            //移動先を設定し続ける
            Agent.destination = player.transform.position;
        }
        //完全に見失った瞬間
        else if(Found)
        {
            Found = false;
            Audio.PlayOneShot(Resources.Load("Sounds/question") as AudioClip);
            //マークを出す
            GameObject mark = Instantiate(Resources.Load("Prefab/ActionMark") as GameObject);
            mark.GetComponent<MeshRenderer>().material = Resources.Load("Materials/question") as Material;
            mark.transform.position = this.transform.TransformPoint(0, 2, 0);
            
            Agent.destination = transform.position;
        }
    }

    protected override void Move()
    {
        //移動しているなら
        if (Agent.hasPath)
        {
            Anim.SetBool(Anim.parameters[0].name, true);
        }
        else
        {
            Anim.SetBool(Anim.parameters[0].name, false);
        }
    }
    protected override void Attack()
    {
        //攻撃範囲内
        if (Found &&    //見つけている
            Agent.remainingDistance <= Agent.stoppingDistance &&    //距離が止まる距離以下
            //現在が攻撃ではない
            Anim.GetCurrentAnimatorStateInfo(0).shortNameHash != Animator.StringToHash(Anim.runtimeAnimatorController.animationClips[2].name))
        {
            Anim.SetTrigger(Anim.parameters[1].name);
        }
    }
    protected override void Death()
    {
        //経験値の追加
        player.AddExp(DropExp);
        //経験値の生成(オブジェクト)
        GameObject expO = Instantiate(EXPPrfab);
        expO.transform.SetParent(player.transform,false);
        
        //削除フラグを建てる。
        IsDied = true;
        //死亡アニメーション以外なら
        if (Anim.GetCurrentAnimatorStateInfo(0).shortNameHash != Animator.StringToHash(Anim.runtimeAnimatorController.animationClips[3].name))
        {
            Anim.SetTrigger(Anim.parameters[2].name);
        }
        //ナビゲーションオフ
        Agent.enabled = false;
        QuestM.Check(this);
        //全てのコリジョン削除
        foreach(Collider coll in GetComponents<Collider>())
        {
            coll.enabled = false;
        }
    }

    void Fade()
    {
        

        Ftime += Time.deltaTime;
        float persent = 1.0f - (Ftime / FadeTime);
        //消えてくよー
        {
            //Color c = MeshRenders[0].sharedMesh.colors[0];
            //c.a = persent;
            //MeshRenders[0].material.color = c;
        }

        //時間経過したなら
        if (Ftime >= FadeTime)
        {
            //アイテムのドロップ
            DropItems di;
            if (di = GetComponent<DropItems>())
            {
                di.ItemDrop();
            }
            //完全消滅
            GameObject.Destroy(this.gameObject);
        }
    }

    protected override void OnHit()
    {
        //攻撃を受けた時、探しにいく(ポジション取得)
        Stime = 3.0f;
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
