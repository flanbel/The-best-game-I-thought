using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
//テキストファイルからUnity上でアクセス可能なデータを作成
public class CreateWeaponInfoAsset : EditorWindow
{
    [MenuItem("Assets/Create/WeaponsInfoAsset")]
    static void CreateWeaponsInfoAsset()
    {
        string DataPath = "Assets/Resources/Data/";
        //Jsonテキストをロード。
        TextAsset textAssets = AssetDatabase.LoadAssetAtPath<TextAsset>(DataPath + "WeaponsInfo.json");
        //Jsonをデシリアライズ。ScriptableObjectを継承したクラスはデシリアライズできないのでワンクッション入れる。
        WeaponsInfo.JsonWeapon JsonWeapon = JsonUtility.FromJson<WeaponsInfo.JsonWeapon>(textAssets.text);
        WeaponsInfo weaponsInfo = new WeaponsInfo();
        //Jsonをデシリアライズ。
        weaponsInfo.weapons = JsonWeapon.weapons;
        //ScriptedObjectのシリアライズを行ってUnityのアセット化。
        AssetDatabase.CreateAsset(weaponsInfo, DataPath + "WeaponsInfo.asset");
        AssetDatabase.Refresh();
    }
}
