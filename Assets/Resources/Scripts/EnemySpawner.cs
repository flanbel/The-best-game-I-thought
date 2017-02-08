using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {

    //エネミー出現
    [System.Serializable]
    public class EnemySpawn
    {
        //出す敵
        [SerializeField]
        private Enemy Enemy;
        //数
        [SerializeField]
        private int SpawnNum;
        //出現時間
        [SerializeField]
        private float SpawnTime;
        public Enemy enemy { get { return Enemy; } set { Enemy = value; } }
        public int num { get { return SpawnNum; } set { SpawnNum = value; } }
        public float spawntime { get { return SpawnTime; } set { SpawnTime = value; } }
    }
    [SerializeField]
    public List<EnemySpawn> Spawn = new List<EnemySpawn>();
    //一定かどうか
    [SerializeField]
    public bool Constant;
    //一定なら間隔
    [SerializeField]
    public float Interval = 0.0f;
    //ループ
    [SerializeField]
    public bool Loop = false;
    [SerializeField, TooltipAttribute("0以下だと無限ループ")]
    public int LoopNum = 2;

    private int LoopCount = 0;
    private bool End = false;
    private float time = 0.0f;
    private int idx = 0;
    // Use this for initialization
    void Start () {
        int a = 0;
	}
	
	// Update is called once per frame
	void Update () {
        //終わってない間ずっと
        if (!End)
        {
            time += Time.deltaTime;
            //配列があるか？
            if (Spawn.Count > 0)
            {
                //一定かどうか
                float t = Constant ? Interval : Spawn[idx].spawntime;

                //時間経過
                if (time >= t)
                {
                    //スポーン数ループ
                    for (int i = 0; i < Spawn[idx].num; i++)
                    {
                        //エネミー生成
                        GameObject enemy = Instantiate(Spawn[idx].enemy.gameObject);
                        Vector3 sca = transform.lossyScale;
                        sca *= 0.5f;
                        //ポジション算出
                        Vector3 pos = new Vector3(Random.Range(-sca.x, sca.x), Random.Range(-sca.y, sca.y), Random.Range(-sca.z, sca.z));
                        //スポナーからみたポジションに
                        enemy.transform.localPosition = transform.localPosition + pos;
                        //親子関係
                        enemy.transform.SetParent(transform);
                    }
                    //スポナーグループの添え字を増やす
                    idx++;
                    
                    if (idx == Spawn.Count &&   //一周して終わった && 
                        LoopNum > 0)            //無限ループじゃない
                    {
                        //一巡したらリセット
                        time = 0.0f;
                        LoopCount++;
                        
                        if (!Loop ||                //ループしない
                            LoopNum <= LoopCount)   //指定数ループした
                        {
                            //宗力
                            End = true;
                        }
                    }

                    //毎回リセット
                    if (Constant)
                    {
                        time = 0.0f;
                    }
                   
                    //配列の範囲を超えないようにする
                    idx %= Spawn.Count;
                }
            }
        }
	}
}
