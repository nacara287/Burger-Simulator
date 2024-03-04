using EasyBuildSystem.Features.Runtime.Buildings.Manager;
using EasyBuildSystem.Features.Runtime.Buildings.Part;
using EasyBuildSystem.Features.Runtime.Buildings.Placer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

using static InventoryViewer;
using Image = UnityEngine.UI.Image;

public class PlayerItemManager : MonoBehaviour
{
    public static PlayerItemManager instance;
    public List<InventorySlot> Slots = new List<InventorySlot>();
    public InventorySlot currentActiveSlot;
    public GameObject CurrObjOnHand;
    [SerializeField]
     Transform HandPosition;
     ItemDatabase database;
    [SerializeField] InventoryItem itemPrefab;
    [SerializeField] BuildingPlacer placer;
    GameObject RaycastObject;
    // Start is called before the first frame update

    UIManager uiManager;
    Manager manager;
    
    [Serializable]
    public class HandItemsSaveData
    {
      

        public List<ItemData> Items;



    }
    public void Awake()
    {if (instance != null)
            Destroy(gameObject);
        instance = this;


    }
    void Start()
    {
        uiManager = UIManager.instance;
        manager = Manager.instance;
        database = manager.database;
     
        currentActiveSlot = Slots[0];
      
        ChangeColorOfSlots();
    }


    void NoRaycast()
    {

      


        RaycastObject = null;

    }
    public float TakeRange = 3;
  
    void checkRayCast()
    {
        NoRaycast();
        Ray ray = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit))
        {
            if (Vector3.Distance(hit.transform.position, transform.position) < TakeRange)
            {




                if (hit.transform.GetComponent<ItemPrefabInfo>() != null)
                {
                    RaycastObject = hit.transform.gameObject;
                }
              

















            }
        }
    }
    void DropObject()
    {
        if (CurrObjOnHand != null&& currentActiveSlot!=null)
        {
            var obj = CurrObjOnHand.gameObject;
            CurrObjOnHand = null;
            obj.transform.SetParent(null);
          
            obj.GetComponent<Rigidbody>().isKinematic = false;
            obj.GetComponent<ItemPrefabInfo>().StackSize = currentActiveSlot.myItem.StackCount;
   

            GetComponent<InventoryInfo>().RemoveItem(Slots.IndexOf(currentActiveSlot));







            Destroy(currentActiveSlot.myItem.gameObject);

        }
     


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

    void CheckSlotItem()
    {
        if (currentActiveSlot != null)
        {
            if (currentActiveSlot.myItem != null)
            {
                if (currentActiveSlot.myItem.myItem.itemTag == SlotTag.Placable)
                {
                    ClearHand();

                  if(placer.GetBuildMode!= BuildingPlacer.BuildMode.PLACE)
                    {
                        placer.ChangeBuildMode(BuildingPlacer.BuildMode.PLACE);
                        placer.SelectBuildingPart(currentActiveSlot.myItem.myItem.equipmentPrefab.GetComponent<BuildingPart>());

                    }else
                    if (Input.GetMouseButtonDown(0))
                    {
                        placer.PlacingBuildingPart();
                       
                        currentActiveSlot.myItem.StackCount -= 1;
                        if (currentActiveSlot.myItem.StackCount==0)
                        {
                            placer.ChangeBuildMode(BuildingPlacer.BuildMode.NONE);
                            Destroy(currentActiveSlot.myItem.gameObject);
                            currentActiveSlot.myItem = null;

                        }
                     
                    }
                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        placer.RotatePreview();
                       
                    }
                   
                }
                else
                {
                    placer.ChangeBuildMode(BuildingPlacer.BuildMode.NONE);
                    if (currentActiveSlot.myItem.myItem.equipmentPrefab != null)
                    {
                        if (CurrObjOnHand == null)
                            GetSlotObjectToHand();
                      
                    }
                    else
                    {
                        ClearHand();

                    }

                }
             

            }
            else
            {
                placer.ChangeBuildMode(BuildingPlacer.BuildMode.NONE);
                ClearHand();
            }


        }
   

    }

    // Update is called once per frame
    void Update()
    {
        CheckSlotItem();
        checkRayCast();
        if (RaycastObject != null&&!manager.LockControls)
        {
            uiManager.ItemNameText.text = RaycastObject.GetComponent<ItemPrefabInfo>().ItemData.Name+" ("+ RaycastObject.GetComponent<ItemPrefabInfo>().StackSize + ")";
            if (!RaycastObject.GetComponent<ItemPrefabInfo>().onHand)
            {
               
                if (RaycastObject.GetComponent<ItemPrefabInfo>().Buttons.Count > 0)
                {
                    if (!uiManager.E_Button.activeSelf)
                    {
                        uiManager.E_Button.SetActive(true);
                        var but = RaycastObject.GetComponent<ItemPrefabInfo>().Buttons[0];
                        uiManager.E_Button.GetComponent<KeyButtonUI>().Intialize(but.Key, but.buttonevent, but.PushTime,but.Text);

                    }
                    
                } 
               

            }
            else
            {
                uiManager.E_Button.SetActive(false);
              
            }
        }
        else
        {
            uiManager.E_Button.SetActive(false);
            uiManager.ItemNameText.text = null;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            DropObject();

        }


        if (Input.GetKeyDown(KeyCode.I) && !uiManager.ItemInventory.gameObject.activeSelf)
        {
            if (!uiManager.PlayerInventory.gameObject.transform.GetChild(0).gameObject.activeSelf)
            {
                uiManager.PlayerInventory.transform.GetChild(0).gameObject.SetActive(true);
               
                uiManager.PlayerInventory.LoadInventory();

                manager.setLockControls(true);

            }
            else
            {
                
                uiManager.PlayerInventory.transform.GetChild(0).gameObject.SetActive(false);
                UIManager.instance.PlayerInventory.SaveInventory();

                if (UIManager.instance.ItemInventory.gameObject.activeSelf == true)
                    UIManager.instance.ItemInventory.SaveInventory();
                manager.setLockControls(false);
            }
           


        }

        if (!manager.LockControls)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                var currentindex = currentActiveSlot.transform.GetSiblingIndex();
                if (currentindex + 1 < Slots.Count)
                    currentActiveSlot = Slots[currentindex + 1];
                else
                    currentActiveSlot = Slots[0];
                ChangeColorOfSlots();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                var currentindex = currentActiveSlot.transform.GetSiblingIndex();
                if (currentindex - 1 >= 0)
                    currentActiveSlot = Slots[currentindex - 1];
                else
                    currentActiveSlot = Slots[Slots.Count - 1];
                ChangeColorOfSlots();
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
}
