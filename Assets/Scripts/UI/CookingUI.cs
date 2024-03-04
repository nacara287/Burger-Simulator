using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CookingUI : MonoBehaviour
{
    public Image Fuel;
   
    public InventoryViewer InventoryUI;
    public Image FuelSlider;
    public CookingSystem cookingSystem;
    public void Start()
    {
        InventoryUI = GetComponent<InventoryViewer>();
    }
    private void Update()
    {
        if (cookingSystem != null)
        {
          
            FuelSlider.fillAmount = cookingSystem.Fuel / 100;

        }
        
    }

    public void CloseInventory()
    {
        var uiManager = UIManager.instance;

        uiManager.cookingUI.gameObject.SetActive(false);
        uiManager.PlayerInventory.transform.GetChild(0).gameObject.SetActive(false);
        Manager.instance.setLockControls(false);
        UIManager.instance.PlayerInventory.SaveInventory();
        cookingSystem = null;

        GetComponent<InventoryViewer>().SaveInventory();
    }
}
