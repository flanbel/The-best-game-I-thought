using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class DataManager : MonoBehaviour {
    //プレイヤーのパラメータ
    [SerializeField]
    private CharacterParameters Parameter;
    public CharacterParameters param { get { return Parameter; } }
    //設定しているパワーオーブ
    [SerializeField]
    private PowerOrb Orb;
    public PowerOrb orb { get { return Orb; } }
    //インベントリ情報
    [SerializeField]
    private Inventory Invents;
    public Inventory invent { get { return invent; } }
    //装備しているもの
    [SerializeField]
    private Equipment Equip;
    public Equipment equip { get { return Equip; } }



    //ファイルパス
    private string DataPath = "";
    void Awake()
    {
        //ファイルパス設定
        DataPath = Application.dataPath + "/Resources/Data/";
        //データ読み取り
        LoadSaveData("PlayerParam.json",out Parameter);
        LoadSaveData("PlayerOrb.json" ,out Orb);
        LoadSaveData("PlayerInventory.json",out Invents);
        LoadSaveData("PlayerEquipment.json", out Equip);
    }
    //セーブデータ読み込み
    void LoadSaveData<T>(string filename,out T obj)
    {
        string JSON = "";

        //パスを指定してインスタンス作成
        FileInfo FI = new FileInfo(DataPath + filename);
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
            //デフォルト値を設定
            obj = default(T);
            return;
        }
        //JSONファイルから読み込み
        JSON = SR.ReadToEnd();
        //閉じる
        SR.Close();

        //文字列からクラス形成
        obj = JsonUtility.FromJson<T>(JSON);
    }
    //JSON形式で保存
    void SaveJsonFile(string filename, object obj)
    {
        //クラスの情報をJSON形式の文字列に変換,第二引数は整理するかどうか。
        string JSON = JsonUtility.ToJson(obj, true);

        //パスを指定してインスタンス作成
        FileInfo FI = new FileInfo(DataPath + filename);
        //書くやつ(読むやつはReader)
        StreamWriter SW;
        //なにか例外スルーが起きるときに使う。
        try
        {
            //なんかFileInfoをつかったやつだと上書きできないので。
            //こっちはファイル作成できないので注意
            SW = new StreamWriter(DataPath + filename, false);
        }
        //エラーでファイルが見つからなかった。
        catch (FileNotFoundException)
        {
            //ファイル作成して書き込むモード
            SW = new StreamWriter(FI.Open(FileMode.OpenOrCreate, FileAccess.Write));
        }
        //ファイルに保存(書き込み)
        SW.Write(JSON);
        //閉じる
        SW.Close();
    }
    //パラメータ情報セーブ
    public void SaveParam(CharacterParameters param)
    {
        Parameter = param;
        SaveJsonFile("PlayerParam.json",Parameter);
    }
    //オーブ情報セーブ
    public void SaveOrb(PowerOrb orb)
    {
        Orb = orb;
        SaveJsonFile("PlayerOrb.json",Orb);
    }
    //持ち物セーブ
    public void SaveInventory()
    {
        SaveJsonFile("PlayerInventory.json", Invents);
    }
}
