using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInfo : MonoBehaviour
{
    public string InventoryID;
    public int MaxInventorySize = 10;
    [HideInInspector]
    public int InventorySize = 0;

    private void Awake()
    {
        StartCoroutine(CheckID());
    }

    IEnumerator CheckID()
    {
        yield return new WaitForSeconds(1);
        if (InventoryID == "")
          CreateID();

    }
    string uniqueID()
    {
        DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        int currentEpochTime = (int)(DateTime.UtcNow - epochStart).TotalSeconds;
        int z1 = UnityEngine.Random.Range(0, 1000000);
        int z2 = UnityEngine.Random.Range(0, 1000000);
        string uid = "Inventory"+currentEpochTime  + z1  + z2;
        return uid;
    }
    public void CreateID()
    {
        InventoryID = uniqueID();
        InventorySize = MaxInventorySize;
    }
}
