using UnityEngine;
using System.Collections;
using System.IO;

public class DataManager : MonoBehaviour {
    [SerializeField]
    private PlayerInfo PInfo;
    public PlayerInfo info { get { return PInfo; } }
    //ファイルパス
    private string DataPath = "";
    void Awake()
    {
        //ファイルパス設定
        DataPath = Application.dataPath + "/Resources/Data/";
    }
    void Start () {
        LoadSaveData();
        SaveJsonFile();
	}
    //セーブデータ読み込み
    void LoadSaveData()
    {
        string[] filename = { "PlayerParam.json", "PlayerOrb.json" };
        string[] JSONs = { "", "" };

        for (int i = 0; i < filename.Length; i++)
        {
            //パスを指定してインスタンス作成
            FileInfo FI = new FileInfo(DataPath + filename[i]);
            //読み取るやつ。
            StreamReader SR;
            try
            {
                //読み取り専用で開く
                SR = new StreamReader(FI.Open(FileMode.Open, FileAccess.Read));
            }
            //ファイルが見つからなかったときの例外スルー
            catch (FileNotFoundException)
            {
                return;
            }
            //JSONファイルから読み込み
            JSONs[i] = SR.ReadToEnd();
            //閉じる
            SR.Close();
        }
        //文字列からクラス形成
        PInfo.PParams = JsonUtility.FromJson<Player.CharacterParameters>(JSONs[0]);
        PInfo.POrb = JsonUtility.FromJson<Player.PowerOrb>(JSONs[1]);
    }
    //JSON形式で保存
    void SaveJsonFile()
    {
        //クラスの情報をJSON形式の文字列に変換,第二引数は整理するかどうか。
        string ParamJson = JsonUtility.ToJson(PInfo.PParams,true);
        string OrbJson = JsonUtility.ToJson(PInfo.POrb, true);

        string[] filename = { "PlayerParam.json", "PlayerOrb.json" };
        string[] JSONs = { ParamJson, OrbJson };

        for (int i = 0; i < filename.Length; i++)
        {
            //パスを指定してインスタンス作成
            FileInfo FI = new FileInfo(DataPath + filename[i]);
            //書くやつ(読むやつはReader)
            StreamWriter SW;
            //なにか例外スルーが起きるときに使う。
            try
            {
                //なんかFileInfoをつかったやつだと上書きできないので。
                //こっちはファイル作成できないので注意
                SW = new StreamWriter(DataPath + filename[i], false);
            }
            //エラーでファイルが見つからなかった。
            catch (FileNotFoundException)
            {
                //ファイル作成して書き込むモード
                SW = new StreamWriter(FI.Open(FileMode.OpenOrCreate, FileAccess.Write));
            }
            //ファイルに保存(書き込み)
            SW.Write(JSONs[i]);
            //閉じる
            SW.Close();
        }
    }
    //パラメータ更新
    public void UpdateParam(Character.CharacterParameters param)
    {
        PInfo.PParams = param;
        SaveJsonFile();
    }
    //オーブ更新
    public void UpdateOrb(Player.PowerOrb orb)
    {
        PInfo.POrb = orb;
        SaveJsonFile();
    }
}
