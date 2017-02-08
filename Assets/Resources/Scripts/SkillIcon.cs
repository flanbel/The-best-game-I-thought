using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[ExecuteInEditMode]
public class SkillIcon : MonoBehaviour
{
    public SkillIcon Parent;
    public SkillIcon Child;
    //親たち
    public List<GameObject> Parents;
    //子たち
    public List<GameObject> Childs;
    //子の表示する幅
    public int WidthToDisplay;
}