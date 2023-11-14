using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Water;
using Image = UnityEngine.UI.Image;

public class CookingSystem : MonoBehaviour
{ public float CookSpeed = 5;
    public bool working;
    public bool UIopened;
    public Transform CookItemPos;
 
    InventoryItem FuelItem;

    InventoryInfo Inventory;

    private void Start()
    {
        Inventory=GetComponent<InventoryInfo>();
    }

    private void Update()
    {
        FuelItem = Inventory.Items[0];
 
 

        if (UIopened)
        {
            if (working)
            {
                UIManager.instance.cookingUI.Fuel.color = Color.red;
               
            }
            else
            {

                UIManager.instance.cookingUI.Fuel.color = Color.white;

            }
            


        }

        if (FuelItem.myItem != null)
        {


            working = true;

        }
        else
        {
            working = false;

        }

        if (working)
        {
            
            for(int i=1; i< Inventory.Items.Count;i++) {
                var CookItem = Inventory.Items[i];
                if (CookItem.myItem != null)
                {
                    if (CookItem.myItem.equipmentPrefab.GetComponent<CookableItem>() != null)
                    { 
                        if (CookItem.heat < CookItem.myItem.equipmentPrefab.GetComponent<CookableItem>().CookedHeat)
                        {
                              
                            CookItem.heat += CookSpeed * Time.deltaTime;
                          
                        }

                        else
                        {
                            var cookedobject = new InventoryItem();
                            cookedobject.myItem = CookItem.myItem.equipmentPrefab.GetComponent<CookableItem>().CookedItem;

                            Inventory.Items[i] = cookedobject;


                            
                            UIManager.instance.cookingUI.InventoryUI.LoadInventory();
                            working = false;
                        }

                    }


                }



            }

            FuelItem.Condition -= FuelItem.myItem.equipmentPrefab.GetComponent<FuelItem>().FuelSpend * Time.deltaTime;
            if (UIopened)
            {if (UIManager.instance.cookingUI.InventoryUI.inventorySlots[0].myItem!=null)
                UIManager.instance.cookingUI.InventoryUI.inventorySlots[0].myItem.Condition = FuelItem.Condition;

            }
        }

         


      
        
       
    }
    public void ShowInventory()
    {

        var uiManager = UIManager.instance;
        uiManager.PlayerInventory.transform.GetChild(0).gameObject.SetActive(true);
        uiManager.cookingUI.gameObject.SetActive(true);

        uiManager.cookingUI.InventoryUI.SetInventory(GetComponent<InventoryInfo>());

        Manager.instance.setLockControls(true);
        UIopened = true;
    }

 

}
