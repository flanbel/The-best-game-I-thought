using UnityEngine;
using System.Collections;
//時間が来たら自信を削除するクラス
public class DeleteTimer : MonoBehaviour {
    //寿命
    [SerializeField]
    private double LifeTime = 10.0f;
    public double lifetime { set { LifeTime = value; } }
    private double time = 0.0f;
    //フェードして消えるかどうか
    [SerializeField]
    private bool IsFade = false;
    public bool fade { set { IsFade = value; } }
    //消えるのにかかる時間
    [SerializeField]
    private float FadeTime = 0.0f;
    public float fadetime { set { FadeTime = value; } }
    [SerializeField]
    private Material mat = null;
    

    // Update is called once per frame
    void Update () {
        time += Time.deltaTime;
        if (LifeTime > 0 &&     //LifeTimeが0以下なら死なない
            time >= LifeTime)
        {
            //フェードするよー
            if (IsFade && mat)
            {
                //死ゾ
                if(time >= FadeTime + LifeTime)
                {
                    //自身を削除
                    GameObject.Destroy(this.gameObject);
                }else
                {
                    //経過時間計算
                    double t = time - LifeTime;
                    //割合計算
                    float persent = 1.0f - ((float)t / FadeTime);
                    //アルファ下げるよー
                    Color c = mat.color;
                    c.a = persent;
                    mat.color = c;
                    
                }
            }
            else
            {
                //自身を削除
                GameObject.Destroy(this.gameObject);
            }
        }
    }
}
