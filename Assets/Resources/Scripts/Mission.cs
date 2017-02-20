using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//ミッションを設定するクラス
[System.Serializable]
public class Mission {
    [System.Serializable]
    public class MissionTarget
    {
        //達成しているか？
        [SerializeField]
        private bool Complete = false;
        public bool complete { get { CheckComplate(); return Complete; } }
        //倒すエネミーのID
        [SerializeField]
        private int EnemyID = -1;
        public int id { get { return EnemyID; } }
        //倒す数
        [SerializeField]
        private int Number = 1;
        //倒した数
        [SerializeField]
        private int Count;
        public int count { get { return Count; } set { Count = value; CheckComplate(); } }
        //成功しているかチェック
        public void CheckComplate()
        {
            //指定数以上倒した。
            Complete = (Count >= Number);
        }
    }
    //ミッションターゲットのリスト
    [SerializeField]
    private List<MissionTarget> MissionT;
    public List<MissionTarget> missionT { get { return MissionT; }set { MissionT = value; } }
    //すべてのミッションクリア
    [SerializeField]
    private bool AllMissionComplete = false;
    public bool allcomplete { get { CheckComplate(); return AllMissionComplete; } }

    //すべてクリアしているかチェック
    public void CheckComplate()
    {
        AllMissionComplete = true;
        foreach (MissionTarget MT in MissionT)
        {
            if (!MT.complete)
                AllMissionComplete = false;
        }
    }

    void Start()
    {
        AllMissionComplete = false;
    }

    public void CheckEnemy(Enemy enemy)
    {
        foreach(MissionTarget MT in MissionT)
        {
            if (MT.id == -1 ||        //指定なし
                MT.id == enemy.id)    //ID比較
            {
                //カウント増加
                MT.count++;
                CheckComplate();
            }
        }
    }
}
