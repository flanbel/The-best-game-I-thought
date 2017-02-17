using UnityEngine;
using System.Collections;
//カメラのほうを向かせるクラス
public class Billboard : MonoBehaviour {
    private static GameObject MainCamera;
    [SerializeField]
    private Vector3 rotate = Vector3.zero;
	// Use this for initialization
	void Start () {
        if (!MainCamera)
        {
            MainCamera = GameObject.Find("GameCamera");
        }
	}
	
	// Update is called once per frame
	void Update () {
        //見る
        transform.LookAt(MainCamera.transform);
        transform.Rotate(Vector3.right, rotate.x);
        transform.Rotate(Vector3.up, rotate.y);
        transform.Rotate(Vector3.forward, rotate.z);

    }
}
