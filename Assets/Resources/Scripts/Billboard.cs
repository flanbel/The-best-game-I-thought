using UnityEngine;
using System.Collections;
//カメラのほうを向かせるクラス
public class Billboard : MonoBehaviour {
    private static GameObject MainCamera;
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
        transform.Rotate(Vector3.up, 180.0f);

    }
}
