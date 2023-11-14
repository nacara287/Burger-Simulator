using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CookingUI : MonoBehaviour
{
    public Image Fuel;
    public Image Arrow;
    public InventoryViewer InventoryUI;

    public void Start()
    {
        InventoryUI = GetComponent<InventoryViewer>();
    }
    public void CloseInventory()
    {
        var uiManager = UIManager.instance;

        uiManager.cookingUI.gameObject.SetActive(false);
        uiManager.PlayerInventory.transform.GetChild(0).gameObject.SetActive(false);
        Manager.instance.setLockControls(false);
        UIManager.instance.PlayerInventory.SaveInventory();


        GetComponent<InventoryViewer>().SaveInventory();
    }
}
