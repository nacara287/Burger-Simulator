using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;
using System.IO;
using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class InventoryViewer : MonoBehaviour
{

    public static InventoryViewer Singleton;



 
 
    public InventoryInfo _inventoryInfo;
    public Text NameText;

    public List<InventorySlot> inventorySlots;


    [SerializeField] Transform draggablesTransform;
        [SerializeField] GameObject InventorySlotPrefab;
    [SerializeField] Transform InventorySlotSpawnPoint;
    public InventoryItem itemPrefab;

    [Header("Item List")]
    [SerializeField] Item[] items;

    [Header("Debug")]
    [SerializeField] Button giveItemBtn;
    Manager manager;

    [Serializable]
    public class ItemData
    {
        public int InstanceID;
        public int stacksize = -1;
        public float condition = 100;
        public int ofset = 0;
    }
    [Serializable]
    public class InventorySaveData
    {
        public string InventoryName;
     
        public List<ItemData> Items;
        
    

    }

 
    void Awake()
    {
        Singleton = this;
   

    }
    private void Start()
    {
        //SpawnInventoryItem();
        

    }


    public void SetInventory(InventoryInfo ýnventoryInfo)
    {
        _inventoryInfo = ýnventoryInfo;
        Intialize();
        LoadInventory();

    }
   public void SaveInventory()
    {
        _inventoryInfo.AddItemsFromSlot(inventorySlots);


    }
  

    public void Intialize()
    {
        NameText.text = _inventoryInfo.InventoryName + "'s Inventory";


        for (int i = 0; i < inventorySlots.Count; i++)
        {



            if (!inventorySlots[i].HotBarSlot)
            {
                Destroy(inventorySlots[i].gameObject);
                inventorySlots.RemoveAt(i);

            }
            else
            {
                if (inventorySlots[i].myItem != null)
                {
                    Destroy(inventorySlots[i].myItem.gameObject);
                    inventorySlots[i].myItem = null;
                }
             
            }







        }
        var count = inventorySlots.Count;
        for (int i = 0; i < _inventoryInfo.InventorySize - count; i++)
        {


            var obj = Instantiate(InventorySlotPrefab, InventorySlotSpawnPoint);
           
            inventorySlots.Add(obj.GetComponent<InventorySlot>());


        }
        foreach(var slot in inventorySlots)
        {
            slot.Inventory = this;

        }

    }
    public void LoadInventory()
    {
       

   



        for (int i=0;i< _inventoryInfo.Items.Count;i++)
        {
            if (_inventoryInfo.Items[i].myItem != null)
            {
                var _item = _inventoryInfo.Items[i].myItem;



                if (inventorySlots[i].myItem != null)
                {
                    Destroy(inventorySlots[i].myItem.gameObject);
                    inventorySlots[i].myItem = null;


                }
                Instantiate(itemPrefab, inventorySlots[i].transform).Initialize(_item, inventorySlots[i], _inventoryInfo.Items[i].StackCount, _inventoryInfo.Items[i].Condition);


            }


        }




        // Check if the slot is empty






    }
    void Update()
    {
        if (manager == null&& Manager.instance!=null)
        {
            manager = Manager.instance;

        
        }
        

    
       
    }

    public void CloseInventory()
    {
        var uiManager = UIManager.instance;

        uiManager.ItemInventory.gameObject.SetActive(false);
        uiManager.PlayerInventory.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        manager.setLockControls(false);
        UIManager.instance.PlayerInventory.SaveInventory();
        if (GetComponent<CookingSystem>() != null)
            GetComponent<CookingSystem>().UIopened = false;
SaveInventory();
    }


    public void SpawnInventoryItem(Item item,int stacksize,float condition)
    {
    

        for (int i = 0; i < inventorySlots.Count; i++)
        {
           
            if (inventorySlots[i].myItem == null)
            {
            
             Instantiate(itemPrefab, inventorySlots[i].transform).Initialize(item, inventorySlots[i], stacksize, condition);
  

                break;
            }

        }
    }

    Item PickRandomItem()
    {
        int random = UnityEngine.Random.Range(0, items.Length);
        return items[random];
    }
}
