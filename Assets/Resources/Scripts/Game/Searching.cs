using UnityEngine;
using System.Collections;

public class Searching : MonoBehaviour {
    //索敵角度
    [SerializeField,Range(0,180)]
    private float SearchAngle = 30.0f;
    //長さ
    [SerializeField]
    private float Length = 1.0f;
    //発見したか
    private bool IsFound = false;
    public bool isFound { get { return IsFound; } }
    GameObject player;
    Ray ray;
    //プレイヤーへのベクトル
    Vector3 ToPlayerVec;
    // Use this for initialization
    void Start () {
        ray = new Ray(transform.position, transform.forward);
        player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
        //プレイヤーへのベクトルを求める
        ToPlayerVec = player.transform.position - transform.position;
        ToPlayerVec.y = 0.0f;

        SearchPlayer();

        //デバッグ
        if (false)
        {
            ray.origin = transform.position;
            ray.direction = transform.forward;


            //各ベクトルを求める
            Quaternion q = Quaternion.AngleAxis(SearchAngle, Vector3.up);
            var r = q * ray.direction;
            q = Quaternion.AngleAxis(-SearchAngle, Vector3.up);
            var l = q * ray.direction;

            //いろいろ線を描画
            Debug.DrawRay(ray.origin, ray.direction * Length, Color.red, 0.0f, false);
            Debug.DrawRay(ray.origin, r * Length, Color.blue, 0.0f, false);
            Debug.DrawRay(ray.origin, l * Length, Color.blue, 0.0f, false);
            Debug.DrawRay(ray.origin, ToPlayerVec, Color.yellow, 0.0f, false);
        }
    }

    //索敵
    void SearchPlayer()
    {
        //ベクトルのなす角を求める。
        Quaternion ToPlayerQ = Quaternion.FromToRotation(transform.forward, ToPlayerVec.normalized);


        Vector3 up = Vector3.up;
        float angleY = 0.0f;
        //Y軸に何度回転しているか取得
        ToPlayerQ.ToAngleAxis(out angleY, out up);
        //180.0度以内に収める
        angleY = Mathf.Min(180.0f, angleY);
        //索敵角度より小さいかどうか
        //かつ　距離が近いかどうか
        IsFound = (angleY <= SearchAngle && ToPlayerVec.magnitude <= Length);
    }
}
