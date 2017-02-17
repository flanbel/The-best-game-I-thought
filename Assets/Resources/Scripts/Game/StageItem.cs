using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
//ステージ上にドロップするアイテム
public class StageItem : MonoBehaviour {
    //初期ベクトル
    [SerializeField]
    private Vector3 StartVector = Vector3.zero;
    AudioSource Souce;
    // Use this for initialization
    void Start () {
        StartVector.x = Random.Range(-1.0f, 1.0f);
        StartVector.z = Random.Range(-1.0f, 1.0f);
        Souce = GetComponent<AudioSource>();
        Souce.clip = Resources.Load("Sounds/Drop") as AudioClip;
        //ドロップ音
        Souce.Play();
    }
	
	// Update is called once per frame
	void Update () {
        //飛びあがりのベクトルが0より大きい
        if (StartVector.magnitude > 0)
        {
            transform.localPosition += StartVector * Time.deltaTime;
            Ray r = new Ray(transform.position, Vector3.down);
            if (Physics.Raycast(r, 2.5f))
            {
                StartVector = Vector3.zero;
            }
        }
	}
}
