
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{public static UIManager instance;
    public Image HealthBar;
    public Text ItemNameText;
 
    public GameObject E_Button;

    public InventoryViewer PlayerInventory;
    public InventoryViewer ItemInventory;
    public CookingUI cookingUI;
    public CraftingUI craftingUI;

    private void Awake()
    {if (instance != null)
            Destroy(this);
        instance = this; 
    }
 
}
