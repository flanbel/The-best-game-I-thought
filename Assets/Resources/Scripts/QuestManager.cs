using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[RequireComponent(typeof(SceneLoad))]
//クエストのマネージャー
public class QuestManager : MonoBehaviour {
    [SerializeField]
    private Player player;
    //ミッション
    [SerializeField]
    private Mission Mission;
    public Mission mission { get { return Mission; } }
    //シーン遷移用
    private SceneLoad Load;
    //帰還までの時間
    [SerializeField]
    private float ReturnTime = 10.0f;
    private float timer = 0.0f;
    //一回きり実行するためのフラグ
    private bool IsClear = false;
    //時間表示
    private Text timerText;


    // Use this for initialization
    void Awake () {
        //ミッションを取得
        Mission = SaveData.GetClass("Mission", new Mission());
        Load = GetComponent<SceneLoad>();
    }

    // Update is called once per frame
    void Update () {
        //すべてのミッション達成
        if (Mission.allcomplete)
        {
            //クエストクリア
            //一度きり呼ばれる
            if (!IsClear)
            {
                //テキスト作成
                GameObject ClearText = (GameObject)Instantiate(Resources.Load("Prefab/Text"));
                ClearText.transform.SetParent(GameObject.Find("Canvas").transform,false);
                ClearText.GetComponent<Text>().text = "クエスト達成！";

                GameObject TTextO = (GameObject)Instantiate(Resources.Load("Prefab/Text"));
                TTextO.transform.SetParent(GameObject.Find("Canvas").transform, false);
                TTextO.transform.localPosition = new Vector3(0, -45, 0);
                timerText = TTextO.GetComponent<Text>();
                timerText.fontSize = 20;



                IsClear = true;
            }
            
            timer += Time.deltaTime;
            timerText.text = "あと" + ((int)(ReturnTime - timer)).ToString() + "秒で帰還します。";
            if(ReturnTime - timer < 0)
            {
                //最初の状態に戻す
                IsClear = false;
                //プレイヤーの情報セーブ
                player.SaveParam();
                //シーン切り替え
                Load.ChengeScene("Menu");
            }
        }
	}

    public void Check(Enemy e)
    {
        Mission.CheckEnemy(e);
    }
}
