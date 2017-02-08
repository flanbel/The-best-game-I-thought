using UnityEngine;
using System.Collections;
//アイテムをドロップするクラス
public class DropItems : MonoBehaviour
{
    //ドロップするアイテム
    [SerializeField]
    private GameObject DropItem;
    //ドロップする確率(0~100%)
    [SerializeField, Range(0, 100)]
    private float Probability;

    public GameObject drop { set { DropItem = value; } }
    public float probability { set { Probability = value; } }

    //アイテムをドロップする。
    public void ItemDrop()
    {
        //とりあえず出現させるだけ。
        if (DropItem &&                             //アイテムが設定されている
            Probability >= Random.Range(0, 100))    //確率
        {
            GameObject item = Instantiate(DropItem);
            //ポジションを同じにする。
            item.transform.position = transform.position;
        }
    }
}