using UnityEngine;
using System.Collections;
//自作重力
public class Gravity : MonoBehaviour {
    //重力加速度
    private float AddGravity = -9.8f;
    //重力
    private float gravity = 0.0f;
    //接地しているか
    private bool isGround = false;
    //レイのY方向の長さ
    private float Distance = 0.0f;
    //レイ
    private Ray ray;
    //コライダー
    Collider coll;
    // Use this for initialization
    void Start() {
        coll = GetComponent<Collider>();
        
        if (coll)
        {
            //型情報取得
            System.Type type = coll.GetType();

            //型比較
            //四角（たぶん完成）
            if (type == typeof(BoxCollider))
            {
                BoxCollider B = coll as BoxCollider;
                float Sy = Mathf.Abs(transform.lossyScale.y);
                Distance += Mathf.Abs(Mathf.Min(B.center.y * Sy, 0.0f));
                float size = B.size.y * Sy;
                Distance += size / 2.0f;
            }
            //丸(適当)
            else if (type == typeof(SphereCollider))
            {
                SphereCollider S = coll as SphereCollider;
                float Sy = Mathf.Abs(transform.lossyScale.y);
                Distance += Mathf.Abs(Mathf.Min(S.center.y * Sy, 0.0f));
                Vector3 size = new Vector3(Mathf.Abs(transform.lossyScale.x), Mathf.Abs(transform.lossyScale.y), Mathf.Abs(transform.lossyScale.z));
                float max = Mathf.Max(Mathf.Max(size.x, size.y), size.z);
                Distance += S.radius * max;
            }
            //カプセル(適当)
            else if (type == typeof(CapsuleCollider))
            {
                CapsuleCollider C = coll as CapsuleCollider;
                float Sy = Mathf.Abs(transform.lossyScale.y);
                Distance += Mathf.Abs(Mathf.Min(C.center.y * Sy, 0.0f));
                Vector3 size = new Vector3(Mathf.Abs(transform.lossyScale.x), Mathf.Abs(transform.lossyScale.y), Mathf.Abs(transform.lossyScale.z));
                float max = Mathf.Max(Mathf.Max(size.x, size.y), size.z);
                Distance += (C.height * size.y) / 2.0f;
                Distance += C.radius * max;
            }
        }
        AddGravity = Physics.gravity.y;
        ray = new Ray(transform.position, Vector3.down);
    }
	
	// Update is called once per frame
	void Update () {
        isGround = false;
        //レイのポジション更新
        ray.origin = transform.position;
        //重力加速
        gravity += AddGravity * Time.deltaTime;
        RaycastHit hit;
        //レイの当たり判定
        if (Physics.Raycast(ray,out hit, Distance))
        {
            //タグを見る
            if (hit.collider.gameObject.tag == "Ground")
            {
                Vector3 pos = hit.point;
                pos.y += Distance;
                //ポジションに移動
                transform.position = pos;
                isGround = true;
                gravity = 0.0f;
            }
        }
        else
        {
            //移動
            transform.Translate(0.0f, gravity * Time.deltaTime, 0.0f);
        }

    }
}
