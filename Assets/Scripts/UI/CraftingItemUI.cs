using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CraftingItemUI : MonoBehaviour
{
    public Image Icon;
    public Text Name;
    public Item Item;
    public void Initialize(Item item)
    {
      


        Item = item;
        Name.text = item.Name;
        Icon.sprite = item.sprite;
    }
    public void SelectThis()
    {

        UIManager.instance.craftingUI.SelectItem(this);

    }
}
