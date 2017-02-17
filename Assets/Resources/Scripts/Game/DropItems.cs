using UnityEngine;
using System.Collections;

//アイテムをドロップするクラス
public class DropItems : MonoBehaviour
{
    //ドロップの情報
    [System.Serializable]
    public class DropInfo
    {
        //ドロップするアイテム
        [SerializeField]
        private GameObject Item;
        //ドロップする確率(0~100%)
        [SerializeField, Range(0, 100)]
        private float Probability;

        public GameObject item { set { Item = value; } get { return Item; } }
        public float probability { set { Probability = value; } get { return Probability; } }
    }
    //アイテムリスト
    [SerializeField]
    private DropInfo[] DropList;
    

    void Start()
    {
        
    }

    //アイテムをドロップする。
    public void ItemDrop()
    {
        //リスト分まわす
        foreach (var Drop in DropList)
        {
            //出現させる。
            if (Drop.item &&                             //アイテムが設定されている
                Drop.probability >= Random.Range(0, 100))    //確率
            {
                //生成する
                GameObject item = Instantiate(Drop.item);
                //ポジションを同じにする。（仮）
                item.transform.position = transform.position + (new Vector3(0, 3, 0));
                
            }
        }
    }
}