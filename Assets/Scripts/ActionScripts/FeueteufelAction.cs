using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionScripts;

//Add to AssetMenu
[CreateAssetMenu(fileName = "FeueteufelAction", menuName = "Action/FeueteufelAction")]
public class FeueteufelAction : Action
{
    public override void PlayCard()
    {
        Debug.Log("FeueteufelAction");
    }
}
