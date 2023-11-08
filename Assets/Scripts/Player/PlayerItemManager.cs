using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItemManager : MonoBehaviour
{public List<InventorySlot> Slots = new List<InventorySlot>();
    public InventorySlot currentActiveSlot;
    public GameObject CurrObjOnHand;
    [SerializeField]
     Transform HandPosition;
    // Start is called before the first frame update
    void Start()
    {
        currentActiveSlot = Slots[0];
        ChangeColorOfSlots();
    }
    void ChangeColorOfSlots()
    {
        foreach(var Slot in Slots)
        {
            Slot.GetComponent<Image>().color = Color.white;

        }
   
        currentActiveSlot.GetComponent<Image>().color = Color.gray;

    }
    void ClearHand()
    {
        if (CurrObjOnHand != null)
        {
            Destroy(CurrObjOnHand);
            CurrObjOnHand = null;
        }
    }

    void GetSlotObjectToHand()
    {
        ClearHand();

      
                CurrObjOnHand = Instantiate(currentActiveSlot.myItem.myItem.equipmentPrefab, HandPosition);


            
           
            }


    // Update is called once per frame
    void Update()
    {
        if (currentActiveSlot != null)
        {
            if (currentActiveSlot.myItem != null)
            {
                if (currentActiveSlot.myItem.myItem.equipmentPrefab != null)
                {if(CurrObjOnHand==null)
                    GetSlotObjectToHand();

                }
                else
                {
                    ClearHand();

                }

            }
            else
            {
                ClearHand();
            }


        }
          
          

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentActiveSlot = Slots[0];
            ChangeColorOfSlots();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentActiveSlot = Slots[1];
            ChangeColorOfSlots();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentActiveSlot = Slots[2];
            ChangeColorOfSlots();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentActiveSlot = Slots[3];
            ChangeColorOfSlots();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            currentActiveSlot = Slots[4];
            ChangeColorOfSlots();
        }


    }
}
