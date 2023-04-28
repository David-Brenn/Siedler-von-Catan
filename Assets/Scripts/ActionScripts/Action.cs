using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ActionScripts
{
[System.Serializable]
public abstract class Action : ScriptableObject
{
    public abstract void PlayCard();
}
}
