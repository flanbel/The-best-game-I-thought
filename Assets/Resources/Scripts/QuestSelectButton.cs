using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
//リクエスト(絶対つける)
[RequireComponent(typeof(Mission))]
[RequireComponent(typeof(QuestInfo))]
public class QuestSelectButton : MonoBehaviour, IPointerEnterHandler
{
    //クエスト情報
    QuestInfo Qinfo;
    //クエスト名を表示するテキスト(右の板の方)
    private Text QuestNameText;
    //クエスト情報を表示するテキスト。
    private Text DescriptionText;

    
    void Start () {
        //クエスト情報取得
        Qinfo = GetComponent<QuestInfo>();
        //クエストネーム設定(ボタン)
        transform.FindChild("QuestName").GetComponent<Text>().text = Qinfo.questname;
        //テキスト取得
        GameObject obj = GameObject.Find("Canvas/InfomationBord/QuestName/Text");
        if (obj)
            QuestNameText = obj.GetComponent<Text>();
        obj = GameObject.Find("Canvas/InfomationBord/Description/Mask/Text");
        if (obj)
            DescriptionText = obj.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    //マウスが入ったときに呼ばれる。
    public void OnPointerEnter(PointerEventData eventData)
    {
        QuestNameText.text = Qinfo.questname;
        DescriptionText.text = Qinfo.description;
    }
}
