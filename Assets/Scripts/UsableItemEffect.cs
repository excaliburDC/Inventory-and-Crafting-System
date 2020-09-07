using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UsableItemEffect : ScriptableObject
{
    public abstract void UseEffect(UsableItem parentItem, Character character);

    public abstract string GetDescription();
}
