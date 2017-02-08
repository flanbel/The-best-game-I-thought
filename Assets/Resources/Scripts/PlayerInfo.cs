using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour {
    //プレイヤーのパラメータ
    [SerializeField]
    private Character.CharacterParameters PlayerParams;
    public Character.CharacterParameters PParams { get { return PlayerParams; }set { PlayerParams = value; } }
    //設定しているパワーオーブ
    [SerializeField]
    private Player.PowerOrb PlayerOrb;
    public Player.PowerOrb POrb { get { return PlayerOrb; } set { PlayerOrb = value; } }
}
