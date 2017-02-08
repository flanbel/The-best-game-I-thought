using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
//プレイヤークラス
public class Player : Character
{
    //データを持ったオブジェクト
    private GameObject Data;
    //カメラ
    private GameObject GameCamera;
    //持っているもの
    private GameObject have;
    //アニメーター
    Animator anim;
    //各アニメーションのハッシュ値を取得
    private int[] Hash;
    //体力バーの画像
    [SerializeField]
    private Image HPbarImage;
    //おーぶの画像
    [SerializeField]
    private Image OrbImage;
    //パワーオーブ
    [System.Serializable]
    public class PowerOrb
    {
        //最大値
        [SerializeField]
        private int Max = 0;
        public int max { get { return Max; } }
        //現在
        [SerializeField]
        private int Now = 0;
        public int now { get { return Now; } }
        //オーブの色
        [SerializeField]
        private Color color = new Color(1, 1, 1, 0.5f);
        //追加
        public void AddExp(int add)
        {
            //Maxより大きくならないように
            Now = Mathf.Min(Now + add, Max);
        }
    }
    [SerializeField]
    private PowerOrb Orb;
    public PowerOrb orb { get { return Orb; } } 


    // Use this for initialization
    void Start() {
        //データから必要な情報取得
        if (Data = GameObject.Find("Data"))        
        {
            //パラメータ取得
            Parm = Data.GetComponent<DataManager>().info.PParams;
            //オーブ情報取得
            Orb = Data.GetComponent<DataManager>().info.POrb;
        }
        //タグ設定
        DamageCollisionTag = "Enemy_Damage_Collision";
        //プレハブ取得
        DCPrefab = Resources.Load("Prefab/DamageCollision") as GameObject;
        DTextPrefab = Resources.Load("Prefab/3DDamageText") as GameObject;

        //最大HPに揃える
        Parm.hp = Parm.mhp;
        UpdateHPbar();
        //
        OrbImage = GameObject.Find("OrbContainer/PowerOrb").GetComponent<Image>();
        //経験値おーぶを変動させる。
        AddExp(0);
        //カメラ取得
        GameCamera = GameObject.Find("GameCamera");
        //手から持っているものを取得
        have = transform.FindChild("Hand").GetChild(0).gameObject;
        //アニメーション取得
        anim = GetComponent<Animator>();
        
        //アニメーションコントローラー取得
        RuntimeAnimatorController RAC = anim.runtimeAnimatorController;
        //アニメーションクリップ数配列を確保
        Hash = new int[RAC.animationClips.Length];
        //各アニメーションクリップのhashを取得
        int i = 0;
        foreach (AnimationClip ac in RAC.animationClips)
        {
            Hash[i] = Animator.StringToHash(ac.name);
            i++;
        }
    }

    // Update is called once per frame
    void Update() {
        Move();
        Attack();
    }
    //移動関数
    protected override void Move()
    {
        Vector3 dir = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            dir.z += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            dir.z -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            dir.x += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            dir.x -= 1;
        }
        
           
        //移動量にスピードをかける
        dir *= SpeedCalculation() * Time.deltaTime;
        

        //カメラの回転と同じに
        transform.localRotation = GameCamera.transform.localRotation;

        Vector3 save;
        Transform cameraT = GameCamera.transform;
        //値保持
        save = cameraT.localEulerAngles;
        cameraT.localEulerAngles = new Vector3(0, cameraT.localEulerAngles.y, cameraT.localEulerAngles.z);
        //カメラの方向に進む
        transform.localPosition += cameraT.TransformDirection(dir);
        
        //保持していた内容を戻す
        cameraT.localEulerAngles = save;
    }
    //攻撃
    protected override void Attack()
    {
        //現在のステート取得
        AnimatorStateInfo animState = this.anim.GetCurrentAnimatorStateInfo(0);
        
        //瞬間的に左クリックされたか。
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("Slash", true);
            //どのステートか？
            if (animState.shortNameHash == Hash[1])
            {
                //半分以上時間が経過している
                if (animState.normalizedTime >= 0.5f)
                {
                    anim.SetInteger("SlashNum", 1);
                }
            }
            else if (animState.shortNameHash == Hash[2])
            {
                if (animState.normalizedTime >= 0.5f)
                {
                    anim.SetInteger("SlashNum", 2);
                }
            }
            else
            {
                anim.SetInteger("SlashNum", 0);
            }
        }
        else
        {
            anim.SetBool("Slash", false);
        }
    }
    //死ぬ
    protected override void Death()
    { }
    //コリジョンに当たったときの処理
    protected override void OnHit()
    {
        UpdateHPbar();
    }
    //関数化する意味はあるのか？
    void UpdateHPbar()
    {
        //体力再計算
        HPbarImage.fillAmount = Parm.hp / Parm.mhp;
    }
    //おーぶ更新
    public void AddExp(int add)
    {
        orb.AddExp(add);
        OrbImage.fillAmount = (float)orb.now / (float)orb.max;
    }
    public void SendData()
    {
        if(Data)
        Data.GetComponent<DataManager>().UpdateOrb(Orb);
    }
    //攻撃判定を出す(アニメーションイベントから呼び出す)
    protected override void CreateAttackCollision(float lifetime = 5.0f)
    {
        //プレハブから攻撃判定をインスタンス化
        GameObject coll = Instantiate(DCPrefab);
        //タグの設定
        coll.tag = "Player_Damage_Collision";
        Weapon w = have.GetComponent<Weapon>();
        //親子関係登録
        coll.transform.SetParent(w.collpos.transform,false);
        //いろいろ初期化
        coll.transform.localScale = w.size;
        //寿命設定
        coll.GetComponent<DeleteTimer>().lifetime = lifetime;
        DamageCollision DC = coll.GetComponent<DamageCollision>();
        //攻撃力設定
        DC.damage = AttackCalculation();
        //クリティカル
        DC.crit = (Random.Range(0.0f, 100.0f) <= Parm.luk);
    }
}
