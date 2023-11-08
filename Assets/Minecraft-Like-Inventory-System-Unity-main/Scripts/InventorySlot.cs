using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum SlotTag { None, Head, Chest, Legs, Feet,Gun }

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public InventoryItem myItem { get; set; }
    
    public SlotTag myTag;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if(InventoryViewer.carriedItem == null) return;
            if(myTag != SlotTag.None && InventoryViewer.carriedItem.myItem.itemTag != myTag) return;
            SetItem(InventoryViewer.carriedItem);
        }
    }

    public void SetItem(InventoryItem item)
    {
        InventoryViewer.carriedItem = null;

        // Reset old slot
        item.activeSlot.myItem = null;

        // Set current slot
        myItem = item;
        myItem.activeSlot = this;
        myItem.transform.SetParent(transform);
        myItem.canvasGroup.blocksRaycasts = true;

       
    }
}
