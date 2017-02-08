using UnityEngine;
using System.Collections;

//クエストの情報
public class QuestInfo : MonoBehaviour {
    //クエストの名前
    [SerializeField]
    private string QuestName;
    public string questname { get { return QuestName; } }
    //説明文
    [SerializeField, TooltipAttribute("クエストの説明文"), TextAreaAttribute(0,14)]
    private string Description;
    public string description { get { return Description; } }
    //ミッション
    private Mission mission;

    void Start()
    {
        //ミッション取得
        mission = GetComponent<Mission>();
    }

    public void CreateQuestInfoObject()
    {
        GameObject questobj = new GameObject();
        questobj.name = "QuestInfo";
        //削除されない
        DontDestroyOnLoad(questobj);
        QuestInfo info = questobj.AddComponent<QuestInfo>();
        info = this;
    }
}
