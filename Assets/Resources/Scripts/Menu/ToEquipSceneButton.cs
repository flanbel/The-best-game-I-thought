using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
//装備シーンへ切り替えするクラス
public class ToEquipSceneButton : MonoBehaviour {
    //装備するTYPE指定
    [SerializeField]
    private Fitment.FITTYPE FitType = Fitment.FITTYPE.NONE;
    private Fitment Fit;
    bool Call;
    IEnumerator addEquip;
    void Start()
    {
        // IEnumeratorを取得する
        //これでコルーチンを呼び出さないと望んだものと異なる結果になる
        addEquip = AddEquipScene();
        //セーブデータから装備情報取得
        Equipment E = SaveData.GetClass(SaveDataName.PlayerEquipment.ToString(), new Equipment());
        //装備情報取得
        switch (FitType)
        {
            case Fitment.FITTYPE.WEAPON:
                Fit = E.weapon;
                break;
            case Fitment.FITTYPE.ARMOR:
                Fit = E.armor;
                break;
            default:
                break;
        }
        //ボタンの名前設定
        transform.GetChild(0).GetComponent<Text>().text = "E:" + Fit.name;
    }

    void Update()
    {
        if (Call)
        {
            //コルーチン呼び出し
            StartCoroutine(addEquip);
        }
    }

    //コルーチンを呼び出すためのフラグ設定
    public void CallCoroutine()
    {
        Call = true;
    }
	
    //対応したシーンを加算呼び出し
    IEnumerator AddEquipScene()
    {
        //加算読み込みしてから
        //LoadSceneは1フレームまたないとだめ！・・・だめ！
        SceneManager.LoadScene("EquipMenu", LoadSceneMode.Additive);
        //ここで一旦終了
        yield return 0;
        //自身をアンロード
        SceneManager.UnloadScene("FitmentMenu");

        //オブジェクト作成
        GameObject type = new GameObject("FitType");
        //コンポーネント追加
        DisplayFitment display = type.AddComponent<DisplayFitment>();
        display.FitType = FitType;
        display.CreateDisplayFitment();
        Call = false;
    }
}
