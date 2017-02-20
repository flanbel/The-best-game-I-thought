using UnityEngine;
using UnityEngine.SceneManagement;
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
    //ミッション内容
    [SerializeField]
    private Mission mission;

    public void SaveMission()
    {
        //ミッション情報保存
        SaveData.SetClass("Mission", mission);
        SaveData.Save();
    }
}
