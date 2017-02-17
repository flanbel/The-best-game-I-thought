using UnityEngine;
using System.Collections;
//時間経過でスケールが変わる
public class ScaleByTime : MonoBehaviour {
    //スケール値のアニメーションカーブ
    [SerializeField]
    private AnimationCurve ScaleCurve;
    //スケールの初期値
    private Vector3 StartScale;
    //経過した時間
    private float time = 0.0f;
	// Use this for initialization
	void Start () {
        StartScale = transform.localScale;
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        //カーブから値を取得
        float scale = ScaleCurve.Evaluate(time);
        //スケーリング
        transform.localScale = StartScale * scale;
	}
}
