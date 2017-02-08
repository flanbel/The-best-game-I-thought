using UnityEngine;
using System.Collections;
//開始時に自身を破棄するクラス(加算ロードのときに使う)
public class OnStartDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
