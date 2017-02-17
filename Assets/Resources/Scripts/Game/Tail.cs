using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
//剣の軌跡を作る。
public class Tail : MonoBehaviour
{
    //混ぜる色
    [SerializeField]
    private Color BlendColor = Color.white;
    //生成されるポリゴンの枚数
    [SerializeField]
    private int PolygonNum = 2;
    //取得する間隔
    [SerializeField]
    private int IntervalFrame = 30;
    private int framecount = 0;
    //始点と終点
    private GameObject StartP, EndP;
    //頂点位置を保存するキュー
    private List<Vector3> Vertex = new List<Vector3>();

    MeshFilter mf;
    MeshRenderer render;

    // Use this for initialization
    void Start()
    {
        StartP = transform.FindChild("StartPoint").gameObject;
        EndP = transform.FindChild("EndPoint").gameObject;

        StartP.GetComponent<MeshRenderer>().enabled = false;
        EndP.GetComponent<MeshRenderer>().enabled = false;

        mf = GetComponent<MeshFilter>();
        render = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        framecount++;
        if (framecount >= IntervalFrame)
        {
            framecount = 0;

            if (Vertex.Count / 2 >= PolygonNum)
            {
                //古いやつを消す。
                Vertex.RemoveAt(0);
                Vertex.RemoveAt(0);
            }

            //ポジションを記録
            Vertex.Add(StartP.transform.position);
            Vertex.Add(EndP.transform.position);

            //わかりやすいように出す
            //Instantiate(StartP).transform.position = StartP.transform.position;
            //Instantiate(EndP).transform.position = EndP.transform.position;
        }
        if (Vertex.Count >= 2)
            testesh();

    }

    void testesh()
    {
        Mesh mesh = mf.mesh;

        int meshCount = (Vertex.Count) / 2;                   // 四角メッシュ生成数

        //頂点ポジション
        Vector3[] vertices = new Vector3[(meshCount) * 4];  // 四角なので頂点数は1つのメッシュに付き4つ.
        //頂点UV
        Vector2[] uv = new Vector2[meshCount * 4];
        //頂点カラー
        Color[] color = new Color[meshCount * 4];
        //頂点インデックス
        int[] triangles = new int[(meshCount) * 2 * 3];     // 1つの四角メッシュには2つ三角メッシュが必要. 三角メッシュには3つの頂点インデックスが必要.
        int i;
        // ----- 頂点座標の割り当て -----
        for (i = 0; i < meshCount - 1; i++)
        {
            vertices[i * 4 + 0] = transform.InverseTransformPoint(Vertex[i * 2 + 0]);//始点　右上
            vertices[i * 4 + 1] = transform.InverseTransformPoint(Vertex[i * 2 + 1]);//終点　右下

            vertices[i * 4 + 2] = transform.InverseTransformPoint(Vertex[i * 2 + 2]);//始点　左上
            vertices[i * 4 + 3] = transform.InverseTransformPoint(Vertex[i * 2 + 3]);//終点　左下
        }

        //最後のポリゴンは手動
        //ローカルに作成
        vertices[i * 4 + 0] = transform.InverseTransformPoint(Vertex[i * 2 + 0]);//始点　右上
        vertices[i * 4 + 1] = transform.InverseTransformPoint(Vertex[i * 2 + 1]);//終点　右下
        vertices[i * 4 + 2] = StartP.transform.localPosition;//始点　左上
        vertices[i * 4 + 3] = EndP.transform.localPosition;//終点　左下
        //vertices[0] = transform.InverseTransformPoint(Vertex[0]);
        //vertices[1] = transform.InverseTransformPoint(Vertex[1]);
        //vertices[2] = StartP.transform.localPosition;
        //vertices[3] = EndP.transform.localPosition;


        // UV座標の指定
        //左下が(0,0),右上が(1,1)
        for (i = 0; i < meshCount; i++)
        {
            uv[(i * 4) + 0] = new Vector2(1, 1);
            uv[(i * 4) + 1] = new Vector2(1, 0);
            uv[(i * 4) + 2] = new Vector2(0, 1);
            uv[(i * 4) + 3] = new Vector2(0, 0);
        }
        float c = Vertex.Count*2;
        for (i = 0; i < c-2; i += 2)
        {
            color[i] = new Color(1, 1, 1, (1.0f / c) * i);
            color[i + 1] = new Color(1, 1, 1, (1.0f / c) * i);
        }
        //StartとEndは手動
        color[i] = new Color(1, 1, 1, (1.0f / c) * i);
        color[i + 1] = new Color(1, 1, 1, (1.0f / c) * i);

        // 頂点インデックスの指定
        int positionIndex = 0;
        for (i = 0; i < meshCount; i++)
        {
            triangles[positionIndex++] = (i * 4) + 1;
            triangles[positionIndex++] = (i * 4) + 3;
            triangles[positionIndex++] = (i * 4) + 2;

            triangles[positionIndex++] = (i * 4) + 2;
            triangles[positionIndex++] = (i * 4) + 0;
            triangles[positionIndex++] = (i * 4) + 1;
        }

        mesh.vertices = vertices;
        mesh.colors = color;
        mesh.uv = uv;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        mf.sharedMesh = mesh;

    }
}