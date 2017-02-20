using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
//加算ロードしたりアンロードしたりするスクリプト
public class SceneLoad : MonoBehaviour
{
    //開始時にロードする
    //[SerializeField]
    //private bool OnStartLoad = false;
    //現在ロードしているか？
    private bool IsLoad = false;
    //前のシーンの名前保持
    private string BeginSceneName = "";
    public string beginname
    {
        set
        {
            BeginSceneName = value;
            SendName(value);
        }
        get
        {
            return BeginSceneName;
        }
    }
    //情報を送る相手。
    [SerializeField]
    public List<SceneLoad> ToSend;
    public List<SceneLoad> tosend { get { return ToSend; } }

    //送信
    void SendName(string name)
    {
        foreach (var s in ToSend)
        {
            //この時の変更は送られない。あくまで自身が変更された時のみ送られる。
            //無限ループしそうだし。
            s.BeginSceneName = name;
            s.IsLoad = true;
        }
    }

    void Awake()
    {
        //if (OnStartLoad)
        //{
        //    AddSafeLoad(BeginSceneName);
        //}
    }
    //シーンの加算ロード
    public void AddLoad(string scenename)
    {
        beginname = scenename;
        SceneManager.LoadScene(scenename, LoadSceneMode.Additive);
        IsLoad = true;
    }

    //シーンの加算ロード(安全？)
    public void AddSafeLoad(string scenename)
    {
        //前のシーンをアンロード
        if (IsLoad)
        {
            UnLoad(beginname);
        }
        beginname = scenename;

        SceneManager.LoadScene(scenename, LoadSceneMode.Additive);
        IsLoad = true;
    }
    //シーンの破棄
    public void UnLoad(string scenename)
    {
        //前のシーン破棄
        bool c = SceneManager.UnloadScene(scenename);
        Resources.UnloadUnusedAssets();
    }
    //シーン切り替え
    public void ChengeScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
}