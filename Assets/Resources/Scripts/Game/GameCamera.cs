using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

    //感度
    [SerializeField]
    private Vector2 Sensitivity = new Vector2(1.0f, 1.0f);
    //限界角度
    [SerializeField,Range(0,85)]
    private float RimitUP;
    [SerializeField,Range(0, 40)]
    private float RimitDown;

    //対象となるオブジェクト
    [SerializeField]
    private GameObject Target;
    //距離
    [SerializeField]
    private Vector3 Dist;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Rotate();
    }

    void LateUpdate()
    {
        //ターゲットあり。
        if (Target)
        {
            //Dist分距離をとる
            transform.localPosition = Target.transform.TransformDirection(Dist) + Target.transform.localPosition;
        }
    }

    //回転
    void Rotate()
    {
        //マウスの移動量取得
        Vector2 angle = new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));
        Vector3 rot = new Vector3(angle.y * Sensitivity.y, angle.x * Sensitivity.x, 0.0f);
        
        //角度制限
        if(Mathf.Abs(transform.localEulerAngles.x + rot.x) > RimitDown &&           //下限
            Mathf.Abs(transform.localEulerAngles.x + rot.x) < (360 - RimitUP))    //上限
        {
            rot.x = 0;
        }

        transform.localEulerAngles += rot;

        //左右回転
        //transform.Rotate(Vector3.up, angle.x);
        //上下回転
        //transform.Rotate(Vector3.right, angle.y);
    }
}
