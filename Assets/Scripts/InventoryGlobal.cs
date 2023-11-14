using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryGlobal : MonoBehaviour
{public static InventoryGlobal Instance;
    public  InventoryItem carriedItem;

    [SerializeField] Transform draggablesTransform;
    public void StackGet(InventoryItem item)
    {
        if (carriedItem == null)
        {
            if (item.myItem != null)
            {
                if (item.myItem.Stackable && item.StackCount > 1)
                {

                    var newitem = Instantiate(Manager.instance.itemPrefab, draggablesTransform);
                    newitem.InitializeStack(item.myItem);
                    newitem.StackCount = 1;
                    item.StackCount -= 1;

                    carriedItem = newitem;
                    carriedItem.canvasGroup.blocksRaycasts = false;



                }

            }





        }





    }

    private void Update()
    {
        if (carriedItem == null) return;

        carriedItem.transform.position = Input.mousePosition;
    }
    public void SetCarriedItem(InventoryItem item)
    {
        if (carriedItem != null && !item.activeSlot.CantPut)
        {
            if (carriedItem.myItem.equipmentPrefab == item.myItem.equipmentPrefab && carriedItem.myItem.Stackable && item.StackCount < item.myItem.MaxStackSize && carriedItem.StackCount < carriedItem.myItem.MaxStackSize)
            {

                if (item.StackCount + carriedItem.StackCount > item.myItem.MaxStackSize)
                {
                    carriedItem.StackCount = (item.StackCount + carriedItem.StackCount) - item.myItem.MaxStackSize;
                    item.StackCount = item.myItem.MaxStackSize;

                }
                else
                {
                    item.StackCount += carriedItem.StackCount;

                    Destroy(carriedItem.gameObject);
                    carriedItem = null;
                }



            }
            else
            {
                if (item.activeSlot.myTag != SlotTag.None && item.activeSlot.myTag != carriedItem.myItem.itemTag) return;
                item.activeSlot.SetItem(carriedItem);

                carriedItem = item;
                carriedItem.canvasGroup.blocksRaycasts = false;
                item.transform.SetParent(draggablesTransform);

            }






        }
        else
        {

            carriedItem = item;
            carriedItem.canvasGroup.blocksRaycasts = false;
            item.transform.SetParent(draggablesTransform);
        }
        UIManager.instance.PlayerInventory.SaveInventory();
        if (UIManager.instance.ItemInventory.gameObject.activeSelf)
            UIManager.instance.ItemInventory.SaveInventory();
        if (UIManager.instance.cookingUI.gameObject.activeSelf)
            UIManager.instance.cookingUI.InventoryUI.SaveInventory();
    }

    private void Awake()
    {
        Instance = this;
    }
}
