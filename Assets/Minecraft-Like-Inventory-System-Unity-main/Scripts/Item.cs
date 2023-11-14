using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
   
    public string Name;
    public string Description;

    public Sprite sprite;
    public SlotTag itemTag;
    public bool Stackable;
    public int MaxStackSize = 10;
    [Header("If the item can be equipped")]
    public GameObject equipmentPrefab;
}

