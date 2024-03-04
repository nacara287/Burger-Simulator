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

    public Transform CookItemPos;
    public GameObject FirseFX;
     public float Fuel=0;
    float fuelspend = 1;
    InventoryItem FuelItem;

    InventoryInfo Inventory;

    private void Start()
    {
        Inventory=GetComponent<InventoryInfo>();
    }

    private void Update()
    {
        FuelItem = Inventory.Items[0];
 
 

       
       if(FuelItem.myItem != null&&!working)
            { bool havefood=false;
            foreach(var item in Inventory.Items)
            {if(item.myItem!=null)
                if (item.myItem.equipmentPrefab.GetComponent<CookableItem>() != null)
                {
                    havefood = true;
                    break;
                }
                  
            }
            if (!havefood)
                return;
                Fuel = 100;
                FuelItem.StackCount -= 1;

                UIManager.instance.cookingUI.InventoryUI.LoadInventory();
                fuelspend = FuelItem.myItem.equipmentPrefab.GetComponent<FuelItem>().FuelSpend;
                working = true;
            }
     
      

        if (working)
        {
            Fuel -= fuelspend*Time.deltaTime;
            if (Fuel <= 0)
            {
                working = false;return;

            }

           
                for (int i=1; i< Inventory.Items.Count;i++) {
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

        
        }

         


      
        
       
    }
    public void ShowInventory()
    {

        var uiManager = UIManager.instance;
        uiManager.PlayerInventory.transform.GetChild(0).gameObject.SetActive(true);
        uiManager.cookingUI.gameObject.SetActive(true);
        uiManager.cookingUI.cookingSystem = this;
        uiManager.cookingUI.InventoryUI.SetInventory(GetComponent<InventoryInfo>());

        Manager.instance.setLockControls(true);
      
    }

 

}
