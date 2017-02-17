using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
//キャラクターの抽象クラス
public abstract class Character : MonoBehaviour {
    //ダメージコリジョンのプレハブ(取得はプレイヤーでしているがなんか違うよね。)
    protected static GameObject DCPrefab;
    //ダメージテキストのプレハブ
    protected static GameObject DTextPrefab;
    //死んだかどうか
    protected bool IsDied = false;
    //キャラクターのパラメータ
    [System.Serializable]//classはこいつがないとInspectorに表示されないので注意！
    public class CharacterParameters
    {
        //最大体力
        [SerializeField]
        protected float MAXHP = 100.0f;
        //体力
        [SerializeField]
        protected float HP = 100.0f;
        //攻撃力
        [SerializeField]
        protected float ATK = 0.0f;
        //防御力
        [SerializeField]
        protected float DEF = 0.0f;
        //運(乱数に関するものがよくなる。)
        [SerializeField]
        protected float LUK = 0.0f;
        //速度(移動速度と回避力に影響)
        [SerializeField]
        protected float SPD = 0.0f;
        public float mhp { get { return MAXHP; } set { MAXHP = value; } }
        //プレイヤーで継承するのでvirtual
        public float hp { get { return HP; } set { HP = value; } }
        public float atk { get { return ATK; } }
        public float def { get { return DEF; } }
        public float luk { get { return LUK; } }
        public float spd { get { return SPD; } }
    }

    // キャラクターのパラメータ
    [SerializeField]
    protected CharacterParameters Parm;
    public CharacterParameters parm { get { return Parm; } }
    //オーディオ
    protected AudioSource Audio;
    //回復タイプ
    public enum HEALTYPE
    {
        VALUE = 0,  //値そのまま
        RATE,       //割合回復
        OVER        //オーバーヒール
    }
    /// <summary>
    /// 体力を回復させる関数
    /// </summary>
    /// <param name="heal"></param>
    /// <param name="type"></param>　回復タイプ指定
    public void Heal(float heal, HEALTYPE type = HEALTYPE.VALUE)
    {
        //オーディオがないなら
        if(!Audio)
        {
            Audio = GetComponent<AudioSource>();
        }
        Audio.PlayOneShot(Resources.Load("Sounds/Heal") as AudioClip);
        switch (type)
        {
            case HEALTYPE.VALUE:
                Parm.hp = Mathf.Min(Parm.mhp, Parm.hp + heal);
                break;
            case HEALTYPE.RATE:
                Parm.hp = Mathf.Min(Parm.mhp, Parm.hp + (heal * Parm.mhp));
                break;
            case HEALTYPE.OVER:
                //未実装
                //Parm.hp = Mathf.Min(Parm.mhp, Parm.hp + heal);
                break;
        }
    }
    /// <summary>
    /// 自分がダメージを受けるコリジョンのタグ。
    /// </summary>
    protected string DamageCollisionTag;

    /// <summary>
    /// 移動関数
    /// </summary>
    protected abstract void Move();

    /// <summary>
    /// 攻撃関数
    /// </summary>
    protected abstract void Attack();
    /// <summary>
    /// 死ぬ関数
    /// </summary>
    protected abstract void Death();
    //スピード計算
    protected float SpeedCalculation()
    {
        //基礎値は3.0
        float speed = 3.0f;
        //スピード上限設定
        float max = 20.0f;
        float now = 0.0f;
        //スピードが上がるしきい値
        float threshold = 10.0f;
        //上昇値
        float rising = 1.0f;
        now = speed + (Parm.spd / threshold) * rising;
        //最終的なスピードの値を計算
        speed = Mathf.Min(max, now);
        return speed;
    }
    //攻撃力計算
    protected float AttackCalculation()
    {
        //攻撃力
        float attack = 0.0f;
        //補正上限
        float max = 0.35f;
        float now = 0.0f;
        //補正値
        float correction = 0.0f;
        //運によって補正が0.1上がるしきい値
        float threshold = 5.0f;
        float rising = 0.1f;
        now = Parm.luk / threshold * rising;
        //補正値を計算
        correction = Mathf.Min(max, now);
        //振れ幅
        attack = Parm.atk * Random.Range(0.75f + correction, 1.25f);
        return attack;
    }
    //ダメージ計算
    float DamageCalculation(DamageCollision dc)
    {
        float damage = dc.damage;
        //クリティカルか否か
        if (dc.crit)
        {
            //クリティカル倍率
            damage = damage * 1.2f;
            //ダメージ軽減
            damage -= Parm.def * 0.25f;
        }
        else
        {
            //防御の半分ダメージ減少
            damage -= Parm.def * 0.5f;
        }
        //ダメージにも幅を
        damage *= Random.Range(0.75f, 1.25f);
        //ダメージが0以上かどうか
        damage = Mathf.Max(0.0f, damage);

        return damage;
    }

    //回避チェック
    bool AvoidanceCheck()
    {
        bool avoid = false;
        //スピードでの回避率補正値(x)
        //運での回避率補正値(y)
        Vector2 correction = Vector2.zero;
        //上限値
        Vector2 Max = new Vector2(0.25f, 0.25f);
        Vector2 Now = Vector2.zero;
        //しきい値
        Vector2 threshold = new Vector2(25.0f, 25.0f);
        //上昇値
        Vector2 rising = new Vector2(0.1f, 0.1f);
        Now = new Vector2((Parm.spd / threshold.x) * rising.x, (Parm.spd / threshold.y) * rising.y);
        //回避率計算
        correction.x = Mathf.Min(Max.x, Now.x);
        correction.y = Mathf.Min(Max.y, Now.y);
        //回避したかどうか
        avoid = (Random.Range(0.0f, 1.0f) <= (correction.x + correction.y));
        return avoid;
    }
    //攻撃が当たったときの特殊な処理
    protected virtual void OnHit() { }

    /// <summary>
    /// 攻撃のあたり判定を生成する関数
    /// </summary>
    /// <param name="lifetime"></param>
    protected abstract void CreateAttackCollision(float lifetime = 5.0f);

    /// <summary>
    /// あたり判定   //CollisionのisTriggerをつけたときはこっちを使う。
    /// </summary>
    /// <param name="coll"></param>
    public void OnTriggerEnter(Collider coll)
    {
        //ダメージ判定
        if (coll.gameObject.tag == DamageCollisionTag)
        {
            DamageCollision DC = coll.gameObject.GetComponent<DamageCollision>();
            string display = "Miss!";
            //回避
            if (!AvoidanceCheck())
            {
                //ダメージ処理(四捨五入)
                int d = Mathf.RoundToInt(DamageCalculation(DC));
                display = d.ToString();
                //HPにダメージ
                Parm.hp -= d;
                //ダメージを受けた時の特殊な処理
                OnHit();
                //HPが0以下
                if (Parm.hp <= 0)
                    Death();
            }
            //ダメージ表記
            DisplayDamage(display,DC.crit);
        }
    }

    //ダメージ表示
    public void DisplayDamage(string damage,bool critical)
    {
        //プレハブからテキストをインスタンス化
        GameObject Dtext = Instantiate(DTextPrefab);
        TextMesh T = Dtext.GetComponent<TextMesh>();
        //クリティカルなら
        if (critical)
            T.color = Color.yellow;
        //テキスト変更
        T.text = damage;
        //キャラクターの上に表示(仮)
        Dtext.transform.localPosition = this.gameObject.transform.TransformPoint(Vector3.up) + new Vector3(Random.Range(-2,2), Random.Range(-2, 2), 0.0f);

    }
}
