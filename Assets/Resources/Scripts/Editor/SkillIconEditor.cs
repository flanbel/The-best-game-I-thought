using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(SkillIcon))]
public class SkillIconEditor : Editor
{
    bool[] folding = { false, false };
    public override void OnInspectorGUI()
    {
        var icon = target as SkillIcon;
        //追加用
        //チェック(親)
        {
            EditorGUI.BeginChangeCheck();
            icon.Parent = (SkillIcon)EditorGUILayout.ObjectField("親追加用", icon.Parent, typeof(SkillIcon), true);

            if (EditorGUI.EndChangeCheck())
            {
                //ラインを作る？
                GameObject line = new GameObject();
                LineRenderer render = line.AddComponent<LineRenderer>();
                //親
                render.SetPosition(0, icon.Parent.transform.position);
                //自分
                render.SetPosition(1, icon.transform.position);
            }
        }
        //チェック(子)
        {
            EditorGUI.BeginChangeCheck();
            icon.Child = (SkillIcon)EditorGUILayout.ObjectField("子追加用", icon.Child, typeof(SkillIcon), true);

            if (EditorGUI.EndChangeCheck())
            {
                //ラインを作る？
                GameObject line = new GameObject();
                LineRenderer render = line.AddComponent<LineRenderer>();
                //自分
                render.SetPosition(0, icon.transform.position);
                //子
                render.SetPosition(1, icon.Child.transform.position);
            }
        }

        if (icon.Parent)
        {
            //配列に追加
            icon.Parents.Add(icon.Parent.gameObject);
            //親の子に追加
            icon.Parent.Childs.Add(icon.gameObject);
            icon.Parent = null;
        }

        if (icon.Child)
        {
            //配列に追加
            icon.Childs.Add(icon.Child.gameObject);
            //子の親に追加
            icon.Child.Parents.Add(icon.gameObject);
            icon.Child = null;
        }

        //List表示
        {
            //親
            List<GameObject> list = icon.Parents;
            short len = (short)icon.Parents.Count;
            // 折りたたみ表示
            if (folding[0] = EditorGUILayout.Foldout(folding[0], "親たち"))
            {
                EditorGUILayout.LabelField("配列サイズ     " + len.ToString());
                // リスト表示
                for (short i = 0; i < len; ++i)
                {
                    EditorGUILayout.BeginHorizontal();
                    list[i] = EditorGUILayout.ObjectField(list[i], typeof(GameObject), true) as GameObject;
                    if (GUILayout.Button("外す"))
                    {
                        list.RemoveAt(i);
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
            //子
            list = icon.Childs;
            len = (short)icon.Childs.Count;
            if (folding[1] = EditorGUILayout.Foldout(folding[1], "子たち"))
            {
                EditorGUILayout.LabelField("配列サイズ     " + len.ToString());
                // リスト表示
                for (short i = 0; i < len; ++i)
                {
                    EditorGUILayout.BeginHorizontal();
                    list[i] = EditorGUILayout.ObjectField(list[i], typeof(GameObject), true) as GameObject;
                    if (GUILayout.Button("外す"))
                    {
                        list.RemoveAt(i);
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
        }

        icon.WidthToDisplay = EditorGUILayout.IntField("幅", icon.WidthToDisplay);
    }
}