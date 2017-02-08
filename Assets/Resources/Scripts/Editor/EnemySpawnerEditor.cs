using UnityEngine;
using UnityEditor;
using System.Collections;
//何のエディターか示す
[CustomEditor(typeof(EnemySpawner))]
public class EnemySpawnerEditor : Editor
{
    bool folding = true;
    EnemySpawner.EnemySpawn Spawn = new EnemySpawner.EnemySpawn();
    //エディター拡張
    public override void OnInspectorGUI()
    {
        var ES = target as EnemySpawner;
        //ラベル(文字)を出す。
        EditorGUILayout.LabelField("スポーンの仕方(時間)");
        ES.Constant = EditorGUILayout.Toggle("等間隔", ES.Constant);
        if (ES.Constant)
        {
            ES.Interval = EditorGUILayout.FloatField("間隔", ES.Interval);
        }

        ES.Loop = EditorGUILayout.Toggle("ループ", ES.Loop);
        if (ES.Loop)
        {
            EditorGUILayout.HelpBox("ループ数は0以下にすると無限ループします。", MessageType.Info);
            ES.LoopNum = EditorGUILayout.IntField("ループ数", ES.LoopNum);
        }
        //折り畳み
        if (folding = EditorGUILayout.Foldout(folding, "スポーンさせるエネミーたち"))
        {
            //箱で囲むための引数
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField("配列サイズ:" + ES.Spawn.Count.ToString());
            //リスト表示
            for (short i = 0; i < ES.Spawn.Count; i++)
            {
                EditorGUILayout.BeginVertical(GUI.skin.box);
                //ボタンとか
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("番号:" + (i + 1).ToString(), GUILayout.Width(100));
                    if (GUILayout.Button("▲"))
                    {
                        if (i > 0)
                        {
                            //退避
                            EnemySpawner.EnemySpawn es = ES.Spawn[i];
                            ES.Spawn.RemoveAt(i);
                            ES.Spawn.Insert(i - 1, es);
                        }
                    }
                    if (GUILayout.Button("▼"))
                    {
                        if (i < ES.Spawn.Count - 1)
                        {
                            //退避
                            EnemySpawner.EnemySpawn es = ES.Spawn[i];
                            ES.Spawn.RemoveAt(i);
                            ES.Spawn.Insert(i + 1, es);
                        }
                    }
                    if (GUILayout.Button("削除"))
                    {
                        //リストから削除する
                        ES.Spawn.Remove(ES.Spawn[i]);
                    }
                    EditorGUILayout.EndHorizontal();
                }
                //シーンのオブジェクトは割り当ててほしくないので最後のフラグはfalse
                ES.Spawn[i].enemy = (Enemy)EditorGUILayout.ObjectField("エネミー", ES.Spawn[i].enemy, typeof(Enemy), false);
                ES.Spawn[i].num = EditorGUILayout.IntField("スポーン数", ES.Spawn[i].num);
                if (!ES.Constant)
                    ES.Spawn[i].spawntime = EditorGUILayout.FloatField("スポーン時間", ES.Spawn[i].spawntime);
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndVertical();
            //配列に追加する
            EditorGUILayout.BeginVertical(GUI.skin.box);
            Spawn.enemy = (Enemy)EditorGUILayout.ObjectField("エネミー", Spawn.enemy, typeof(Enemy), false);
            Spawn.num = EditorGUILayout.IntField("スポーン数", Spawn.num);
            if (!ES.Constant)
                Spawn.spawntime = EditorGUILayout.FloatField("スポーン時間", Spawn.spawntime);
            if (GUILayout.Button("追加"))
            {
                //リストに追加
                ES.Spawn.Add(Spawn);
                //新しいものを作成
                Spawn = new EnemySpawner.EnemySpawn();
            }
            EditorGUILayout.EndVertical();
        }
    }
}
