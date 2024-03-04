using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : MonoBehaviour
{
    public Transform RequiredItemSpawnPoint;
    public Transform CraftableItemSpawnPoint;
    public InventorySlot CraftItem;
    public List<CraftingItemUI> CraftableItems;
    public GameObject InventorySlotPrefab;
    public GameObject CraftableItemUIPrefab;
    public CraftingItemUI selected;
    public Button CraftButton;

    public CraftingSystem LastSystem;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Craft()
    {
        if (LastSystem != null)
        {
            LastSystem.CraftItem(selected.Item,PlayerItemManager.instance.GetComponent<InventoryInfo>());


        }

    }
    public void Close()
    {
        Manager.instance.setLockControls(false);
        LastSystem.UIOpened = false;
        gameObject.SetActive(false);


    }
    public void SelectItem(CraftingItemUI select)
    {
        selected = select;
            var SelectedItem = selected.Item;


        foreach (Transform item in RequiredItemSpawnPoint)
        {
            Destroy(item.gameObject);


        }

       var craftable= SelectedItem.equipmentPrefab.GetComponent<CraftableItem>();
        if(CraftItem.myItem!=null)
        Destroy(CraftItem.myItem.gameObject);
        CraftItem.myItem = Instantiate(Manager.instance.itemPrefab, CraftItem.transform).GetComponent<InventoryItem>();
        CraftItem.myItem.Initialize(SelectedItem,CraftItem,1,100);

        foreach (var item in craftable.ItemsNeedForCraft)
        { var myit = Manager.instance.database.items.Find(x => x.GetInstanceID() == (int)item.x);
            var obj = Instantiate(InventorySlotPrefab, RequiredItemSpawnPoint);
            obj.GetComponent<InventorySlot>().myItem = Instantiate(Manager.instance.itemPrefab, obj.transform);
            obj.GetComponent<InventorySlot>().myItem.Initialize(myit, obj.GetComponent<InventorySlot>(), (int)item.y,100);
 


        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
