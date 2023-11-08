using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStats : MonoBehaviour
{
    public float Health=100;
  
    UIManager uiManager;
    // Start is called before the first frame update
    void Start()
    {
        uiManager = UIManager.instance;


    }

    // Update is called once per frame
    void Update()
    { uiManager.HealthBar.fillAmount= Health/100;
      

        if (Input.GetKeyDown(KeyCode.I) && !uiManager.ItemInventory.gameObject.activeSelf)
        {
            if (!uiManager.PlayerInventory.gameObject.activeSelf)
            {
                uiManager.PlayerInventory.gameObject.SetActive(true);
                uiManager.PlayerInventory._inventoryInfo = GetComponent<InventoryInfo>();
                uiManager.PlayerInventory.LoadInventory();
                Cursor.lockState = CursorLockMode.None;

            }
            else
            {
                uiManager.PlayerInventory.SaveInventory();
                uiManager.PlayerInventory.gameObject.SetActive(false);
    
                Cursor.lockState = CursorLockMode.Locked;
            }
          

     
        }
     
    }

    private void OnTriggerStay(Collider collision)
    {
;
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (collision.gameObject != gameObject)
            {
                if (collision.transform.parent.gameObject.GetComponent<InventoryInfo>() != null)
                {
                    if (!uiManager.ItemInventory.gameObject.activeSelf)
                    {
                      
                        uiManager.ItemInventory.gameObject.SetActive(true);
                        uiManager.ItemInventory._inventoryInfo = collision.transform.parent.gameObject.GetComponent<InventoryInfo>();
                        uiManager.ItemInventory.LoadInventory();
                        uiManager.PlayerInventory.gameObject.SetActive(true);
                        uiManager.PlayerInventory._inventoryInfo = GetComponent<InventoryInfo>();
                        uiManager.PlayerInventory.LoadInventory();
                     
                            Cursor.lockState = CursorLockMode.None;
                      
                          
                    }
                    else
                    {
                        uiManager.ItemInventory.SaveInventory();

                        uiManager.PlayerInventory.SaveInventory();
                        uiManager.ItemInventory.gameObject.SetActive(false);
                        uiManager.PlayerInventory.gameObject.SetActive(false);
                        Cursor.lockState = CursorLockMode.Locked;
                    }
                }

            }
   
        }
    }
}
