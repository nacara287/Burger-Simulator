using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
   public List<Item> CraftableItems = new List<Item>();
    public bool UIOpened;

    public Item SelectedItem;

 public void CraftItem(Item item, InventoryInfo inventory)
    {
        var craftable = item.equipmentPrefab.GetComponent<CraftableItem>();
        var requireditems = new List<InventoryItem>();
        foreach (var crafti in craftable.ItemsNeedForCraft)
        {
            var myit = Manager.instance.database.items.Find(x => x.GetInstanceID() == (int)crafti.x);
            var invenotryitem = new InventoryItem();
            invenotryitem.myItem = myit;
            invenotryitem.StackCount = (int)crafti.y;

            requireditems.Add(invenotryitem);
        }



        foreach (var itema in requireditems)
        { int kalan = 0;
            kalan = itema.StackCount;
            foreach (var itemin in inventory.Items)
            {
                if (itemin.myItem == itema.myItem)
                {if(kalan- itemin.StackCount >= 0)
                    {
                        kalan -= itemin.StackCount;
                        itemin.StackCount = 0;
                    }
                    else
                    {
                        itemin.StackCount = itemin.StackCount - kalan;
                        kalan = 0;
                       
                    }
                       

                



                }
            }
         

        }


        inventory.AddItemByInfo(item);
        inventory.SaveInventory();


    }
bool CheckIfInventoryHasItem(Item item, InventoryInfo inventory)
    {
        var craftable = item.equipmentPrefab.GetComponent<CraftableItem>();
        var requireditems = new List<InventoryItem>();
        foreach (var crafti in craftable.ItemsNeedForCraft)
        {
            var myit = Manager.instance.database.items.Find(x => x.GetInstanceID() == (int)crafti.x);
            var invenotryitem = new InventoryItem();
            invenotryitem.myItem = myit;
            invenotryitem.StackCount = (int)crafti.y;

            requireditems.Add(invenotryitem);
        }

   
        int y = 0;

        foreach(var itema in requireditems)
        {
            int i = 0;
            foreach(var itemin in inventory.Items)
            {
                if (itemin.myItem == itema.myItem)
                {
                    i += itemin.StackCount;
                }
            }
            if (i >= itema.StackCount)
            {
                y += 1;
            }

        }
        if (y == requireditems.Count)
        {
            return true;

        }

        return false;


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ShowUI()
    {
        var craftui = UIManager.instance.craftingUI;
        Manager.instance.setLockControls(true);
        UIManager.instance.craftingUI.gameObject.SetActive(true);
        foreach (var item in craftui.CraftableItems)
        {
            Destroy(item.gameObject);
        }

        craftui.CraftableItems = new List<CraftingItemUI>();
        craftui.LastSystem = this;
        craftui.selected = null;
     foreach (var item in CraftableItems)
        {
            var spawn = Instantiate(craftui.CraftableItemUIPrefab, craftui.CraftableItemSpawnPoint).GetComponent<CraftingItemUI>();
            spawn.Initialize(item);

            craftui.CraftableItems.Add(spawn);
        }

        UIOpened = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (UIOpened)
        { var craftui = UIManager.instance.craftingUI;
            if(craftui.selected!=null)
            SelectedItem = craftui.selected.Item;
            else
                SelectedItem = null;
            if (SelectedItem != null)
            {
               if( CheckIfInventoryHasItem(SelectedItem, PlayerItemManager.instance.gameObject.GetComponent<InventoryInfo>()))
                {
                    craftui.CraftButton.interactable=true;
                    
                }
                else
                {
                    craftui.CraftButton.interactable = false;
                   
                }

            }
          
        }
        
    }
}
