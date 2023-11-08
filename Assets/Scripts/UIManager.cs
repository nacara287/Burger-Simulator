
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{public static UIManager instance;
    public Image HealthBar;
  

    public InventoryViewer PlayerInventory;
    public InventoryViewer ItemInventory;

    private void Awake()
    {if (instance != null)
            Destroy(this);
        instance = this; 
    }
 
}