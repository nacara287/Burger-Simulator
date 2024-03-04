using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemPrefabInfo : MonoBehaviour
{
    public Item  ItemData;
    public List<ItemButton> Buttons=new List<ItemButton>();
    public bool onHand = false;
    public int StackSize = 1;
    // Start is called before the first frame update
    void Start()
    {
        Buttons=GetComponents<ItemButton>().ToList();
        
    }

    // Update is called once per frame
    void Update()
    {if (transform.root != null)
        {
            if (transform.root.GetComponent<PlayerItemManager>() != null)
            {
                onHand = true;

            }
            else
            {
                onHand = false;

            }

        }
        else
        {
            onHand = false;
        }
           
             
      

        
    }
    public void TakeItem()
    {
      
       PlayerItemManager.instance.GetComponent<InventoryInfo>().AddItem(this);
       
        Destroy(gameObject);

    }
}
