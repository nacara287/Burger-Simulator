using EasyBuildSystem.Features.Runtime.Buildings.Part;
using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using static InventoryViewer;

public class InventoryInfo : MonoBehaviour
{
    public string InventoryID;

    public string InventoryName;

    public int InventorySize = 0;
    public List<InventoryItem> Items;
    string saveFilePath;
    private void Awake()
    {
        StartCoroutine(CheckID());
       
        if (InventoryName == "")
        {
            if (GetComponent<BuildingPart>() != null)
            {
                InventoryName = GetComponent<BuildingPart>().GetSaveData().Name;
            }
            if (GetComponent<PlayerStats>() != null)
            {
                InventoryName = "Player";
            }
        }


    }


    public void SaveInventory()
    {
        saveFilePath = Application.persistentDataPath + "/Inventory-" + InventoryID + ".json";
        var savedata = new InventorySaveData();
        savedata.InventoryName = InventoryID;
        savedata.Items = new List<ItemData>();




        for (int i=0;i<Items.Count ; i++ )
        {
           
            if (Items[i].myItem!= null)
            {
                
                var data = new ItemData();
                data.InstanceID = Items[i].myItem.GetInstanceID();
                data.stacksize = Items[i].StackCount;
                data.condition= Items[i].Condition;
                data.ofset = i;
               
                savedata.Items.Add(data);


            }
          





        }

        string savePlayerData = JsonUtility.ToJson(savedata);
        File.WriteAllText(saveFilePath, savePlayerData);

        Debug.Log("Save file created at: " + saveFilePath);
    }

    private void Start()
    {
     


        ResetInventory();
        LoadInventory();
   
        if (GetComponent<PlayerItemManager>() != null)
        {

            UIManager.instance.PlayerInventory.SetInventory(this);
        }

    }
    void ResetInventory()
    {
        Items = new List<InventoryItem>();
        for (int i = 0; i < InventorySize ; i++)
        {
            var item = new InventoryItem();
            Items.Add(item);

        }
    }

    void ReloadInventoryPlayer()
    {
        if (GetComponent<PlayerItemManager>() != null)
        {

            UIManager.instance.PlayerInventory.LoadInventory();
        }

    }
    public void ShowInventory()
    {
        var uiManager = UIManager.instance;

        uiManager.ItemInventory.gameObject.SetActive(true);
 
        uiManager.ItemInventory.SetInventory(this);
    
        Manager.instance.setLockControls(true);

    }
    public void LoadInventory()
    {
     

      



        saveFilePath = Application.persistentDataPath + "/Inventory-" + InventoryID + ".json";
        if (File.Exists(saveFilePath))
        {
            string loadPlayerData = File.ReadAllText(saveFilePath);
            LoadData(JsonUtility.FromJson<InventorySaveData>(loadPlayerData));


        }
        else
            Debug.Log("There is no save files to load!");
    }
    public void AddItem(ItemPrefabInfo info)
    {
        var inventoryitem = new InventoryItem();
        inventoryitem.myItem = info.ItemData;
        inventoryitem.StackCount = info.StackSize;
        for (int i=0;i< Items.Count; i++)
        {
            if (Items[i].myItem == null)
            {
                Items[i] = inventoryitem;
              
                break;

            }
              
           
        }
  
   SaveInventory();
 ReloadInventoryPlayer();
    }
    public void RemoveItem(int i)
    {
        Items[i].myItem = null;
      

        SaveInventory();
        ReloadInventoryPlayer();
    }
    public void AddItemsFromSlot( List<InventorySlot> inventorySlots) {
        ResetInventory();
      
            foreach (var slot in inventorySlots)
        {
            if (slot.myItem != null)
            {
                var newitem = new InventoryItem();
                newitem.ofset = inventorySlots.IndexOf(slot);
                newitem.myItem = slot.myItem.myItem;
                newitem.Condition = slot.myItem.Condition;
                newitem.StackCount = slot.myItem.StackCount;

             Items[newitem.ofset]=newitem;
            }



        }
        SaveInventory();
       // ReloadInventoryPlayer();
    }


    public void LoadData(InventorySaveData data)
    {




       

        for (int i = 0; i < data.Items.Count; i++)
        {
            var _item = Manager.instance.database.items.Find(x => x.GetInstanceID() == data.Items[i].InstanceID);

            var inventoryitem = new InventoryItem();
            inventoryitem.myItem = _item;
            inventoryitem.Condition = data.Items[i].condition;
            inventoryitem.StackCount = data.Items[i].stacksize;
            inventoryitem.ofset = data.Items[i].ofset;

            Items[inventoryitem.ofset] =inventoryitem;


        }




        // Check if the slot is empty






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
    
    }
}
