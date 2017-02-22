using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
//装備リスト
public class EquipList : MonoBehaviour {
    //装備の情報
    [SerializeField]
    private Fitment Fit;
    public Fitment fit { set { Fit = value; } }

    void Awake()
    {
        GameObject View = GameObject.Find("ScrollView");
        transform.SetParent(View.transform);
    }

    void Start()
    {
        string displayT = "";
        if (Fit.equip)
            displayT = "E:";
        transform.GetChild(0).GetComponent<Text>().text = displayT + Fit.name;
    }

    public void SendData()
    {
        //情報送信
        GameObject.Find("Status").GetComponent<ShowStatusComp>().fit = Fit;
        GameObject.Find("EquipButton").GetComponent<EquipButton>().fit = Fit;
    }
}
