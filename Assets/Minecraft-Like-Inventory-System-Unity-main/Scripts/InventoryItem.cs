using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text;
using Unity.VisualScripting;

public class InventoryItem : MonoBehaviour, IPointerClickHandler
{
    Image itemIcon;
    public CanvasGroup canvasGroup { get; private set; }

    public Item myItem { get; set; }
    public float Condition = 100;
    public int StackCount=1;
    public int ofset = 0;
    public float heat = 0;
 
    public Text stacktext;
    public Image conditionslider;
    public InventorySlot activeSlot { get; set; }

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        itemIcon = GetComponent<Image>();

    }

    public void Initialize(Item item, InventorySlot parent,int stackcount,float condition)
    {
        itemIcon = GetComponent<Image>();
        activeSlot = parent;
        activeSlot.myItem = this;
        Condition = condition; ;
        myItem = item;
        StackCount = stackcount;
        itemIcon.sprite = item.sprite;
    }
    public void InitializeStack(Item item)
    {
      
        myItem = item;
        itemIcon.sprite = item.sprite;
    }
    void Update()
    {
        if (StackCount == 0)
        {
            Destroy(gameObject);
        }
        if (Condition < 100)
        {
            conditionslider.gameObject.transform.parent.gameObject.SetActive(true);
            conditionslider.fillAmount = Condition/100;
        }
        else
        {
            conditionslider.gameObject.transform.parent.gameObject.SetActive(false);
        }
        if (Condition < 0)
            Destroy(gameObject);
        if (myItem != null)
        {
            if (myItem.Stackable)
                stacktext.text = StackCount.ToString();
            else
                stacktext.text = "";
        }
        else
        {
            stacktext.text = "";
        }
  
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            InventoryGlobal.Instance.SetCarriedItem(this);
        
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            InventoryGlobal.Instance.StackGet(this);

        }
    }
}
