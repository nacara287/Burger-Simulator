using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;
using System.IO;
using System;

public class InventoryViewer : MonoBehaviour
{
    public static InventoryViewer Singleton;
    public static InventoryItem carriedItem;
    public ItemDatabase database;
    public InventoryInfo _inventoryInfo;

    [SerializeField] List<InventorySlot> inventorySlots;

    [SerializeField] Transform draggablesTransform;
        [SerializeField] GameObject InventorySlotPrefab;
    [SerializeField] Transform InventorySlotSpawnPoint;
    [SerializeField] InventoryItem itemPrefab;

    [Header("Item List")]
    [SerializeField] Item[] items;

    [Header("Debug")]
    [SerializeField] Button giveItemBtn;

    [Serializable]
    public class ItemData
    {
        public int InstanceID;
        public int stacksize = -1;
        public int ofset = 0;
    }
    [Serializable]
    public class InventorySaveData
    {
        public string InventoryName;
     
        public List<ItemData> Items;
        
    

    }
    string saveFilePath;
    public void SaveInventory()
    {
        var savedata = new InventorySaveData();
        savedata.InventoryName = _inventoryInfo.InventoryID;
        savedata.Items = new List<ItemData>();
    
        foreach(var item in inventorySlots)
        {
            if (item.myItem != null)
            {
                var data = new ItemData();
                data.InstanceID = item.myItem.myItem.GetInstanceID();
                data.stacksize = item.myItem.StackSize;
                data.ofset = item.transform.GetSiblingIndex();
         
                savedata.Items.Add(data);
              
            }
            


        }
     
        string savePlayerData = JsonUtility.ToJson(savedata);
        File.WriteAllText(saveFilePath, savePlayerData);

        Debug.Log("Save file created at: " + saveFilePath);
    }
    void Awake()
    {
       
        Singleton = this;
      
    }
    private void Start()
    {
      //SpawnInventoryItem();


    }

    public void LoadInventory()
    {
        if (_inventoryInfo != null)
            saveFilePath = Application.persistentDataPath + "/" + _inventoryInfo.InventoryID + ".json";
        if (File.Exists(saveFilePath))
        {
            string loadPlayerData = File.ReadAllText(saveFilePath);
            LoadData (JsonUtility.FromJson<InventorySaveData>(loadPlayerData));

           
        }
        else
            Debug.Log("There is no save files to load!");
    }
    public void LoadData(InventorySaveData data)
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
         
           
                Destroy(inventorySlots[i].gameObject);
           
          

        }
        inventorySlots = new List<InventorySlot>();
        for (int i = 0; i < _inventoryInfo.InventorySize; i++)
        {


          var obj=  Instantiate(InventorySlotPrefab,InventorySlotSpawnPoint);
            inventorySlots.Add(obj.GetComponent<InventorySlot>());


        }



        for (int i=0;i< data.Items.Count;i++)
        {
            var _item = database.items.Find(x=>x.GetInstanceID()== data.Items[i].InstanceID);
            if (inventorySlots[i].myItem == null)
            {
                Instantiate(itemPrefab, inventorySlots[data.Items[i].ofset].transform).Initialize(_item, inventorySlots[i]);

            }

        }


       
             
                // Check if the slot is empty
               
          

        


    }
    void Update()
    {

        if (carriedItem == null) return;

        carriedItem.transform.position = Input.mousePosition;
       
    }

    public void SetCarriedItem(InventoryItem item)
    {
        if(carriedItem != null)
        {
            if(item.activeSlot.myTag != SlotTag.None && item.activeSlot.myTag != carriedItem.myItem.itemTag) return;
            item.activeSlot.SetItem(carriedItem);
        }

       

        carriedItem = item;
        carriedItem.canvasGroup.blocksRaycasts = false;
        item.transform.SetParent(draggablesTransform);
       
    }

   
    public void SpawnInventoryItem(Item item = null)
    {
        Item _item = item;
        if(_item == null)
        { _item = PickRandomItem(); }


        for (int i = 0; i < 5; i++)
        {
           
            if (inventorySlots[i].myItem == null)
            {
                Instantiate(itemPrefab, inventorySlots[i].transform).Initialize(_item, inventorySlots[i]);

            }

        }
    }

    Item PickRandomItem()
    {
        int random = UnityEngine.Random.Range(0, items.Length);
        return items[random];
    }
}
