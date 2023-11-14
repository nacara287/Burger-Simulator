using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum SlotTag { None,Gun,Placable,Cookable ,Fuel}

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public InventoryViewer Inventory;
    public InventoryItem myItem { get; set; }
    public bool HotBarSlot;
    public SlotTag myTag;
    public bool CheckTag;
   
    public bool CantTake;
    public bool CantPut;
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if (InventoryGlobal.Instance.carriedItem == null) return;

          
            SetItem(InventoryGlobal.Instance.carriedItem);
        }
    }
    

    public void SetItem(InventoryItem item)
    {
        if (!CantTake)
        {if (CheckTag && item.myItem.itemTag != myTag)
                return;

            InventoryGlobal.Instance.carriedItem = null;

            // Reset old slot
            if (item.activeSlot != null)
            {
                
                item.activeSlot.myItem = null;
            }
              


            // Set current slot
          
            myItem = item;
            myItem.activeSlot = this;
            myItem.transform.SetParent(transform);
            myItem.canvasGroup.blocksRaycasts = true;
            UIManager.instance.PlayerInventory.SaveInventory();
            if (UIManager.instance.ItemInventory.gameObject.activeSelf)
                UIManager.instance.ItemInventory.SaveInventory();
            if (UIManager.instance.cookingUI.gameObject.activeSelf)
                UIManager.instance.cookingUI.InventoryUI.SaveInventory();
        }
       

       
    }
}
