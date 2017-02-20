using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
//アセットファイルを作成するウィンドウ
public class CreateAsset : EditorWindow
{
    //ScriptableObject obj;
    //[MenuItem("Assets/Create/test")]
    //static void Open()
    //{
    //    //ウィンドウの作成・取得・表示を行う
    //    GetWindow<CreateAsset>();
    //}

    //void OnGUI()
    //{
    //    obj = (ScriptableObject)EditorGUILayout.ObjectField("obj", obj, typeof(ScriptableObject), false) ;
    //}

    //// 毎フレーム更新
    //void Update()
    //{
        
    //}

    //Jsonファイルからアセットを作成
    [MenuItem("Assets/Create/CreateAssetFromJson")]
    static void CreateAssetFromJson()
    {
        string DataPath = "Assets/Resources/Data/";
        string filename = "WeaponsInfo";
        //Jsonテキストをロード。
        TextAsset textAssets = AssetDatabase.LoadAssetAtPath<TextAsset>(DataPath + filename + ".json");

        //Jsonをデシリアライズ。ScriptableObjectを継承したクラスはデシリアライズできないのでワンクッション入れる。
        WeaponsInfo.Json json = JsonUtility.FromJson<WeaponsInfo.Json>(textAssets.text);
        WeaponsInfo obj = new WeaponsInfo();
        //Jsonをデシリアライズ。
        obj.weapons = json.weapons;

        //ScriptedObjectのシリアライズを行ってUnityのアセット化。
        AssetDatabase.CreateAsset(obj, DataPath + filename + ".asset");
        AssetDatabase.Refresh();
    }
}
