using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(SceneLoad))]
public class SceneLoadEditor : Editor
{
    bool folding = true;
    SceneLoad add;
    public override void OnInspectorGUI()
    {
        var SL = target as SceneLoad;
        if (SL.tosend == null)
        {
            SL.ToSend = new List<SceneLoad>();
        }
        //前のシーンの名前
        EditorGUILayout.LabelField(SL.beginname);
        //リストを出す。
        if(folding = EditorGUILayout.Foldout(folding, "同期リスト"))
        {
            for (short i = 0;i<SL.tosend.Count;)
            {
                //相手
                SceneLoad s = SL.tosend[i];
                EditorGUILayout.BeginHorizontal();
                //表示のみ
                EditorGUILayout.ObjectField(s, typeof(SceneLoad),false);
                if (GUILayout.Button("削除"))
                {
                    //お互いのリストから削除
                    //相手のリストから自身を消した
                    s.tosend.Remove(SL);
                    //自身のリストから相手を消す
                    SL.tosend.Remove(s);
                    //ListのRemoveをすると後ろの要素が詰められるのでもう一度同じ添え字で続ける
                    //i++;
                }
                else
                {
                    i++;
                }
                EditorGUILayout.EndHorizontal();
            }

            add = EditorGUILayout.ObjectField("追加", add, typeof(SceneLoad), true) as SceneLoad;
            if (add)
            {
                //
                if (!add.Equals(SL) &&      //自分ではない
                    !SL.tosend.Contains(add)) //同じ要素がないとき(未登録)
                {
                    //お互いのリストに追加
                    SL.tosend.Add(add);
                    add.tosend.Add(SL);
                }
                add = null;
            }
            //if (GUILayout.Button("AllDelete"))
            //{
            //    SL.tosend.Clear();
            //}
        }
    }
}
