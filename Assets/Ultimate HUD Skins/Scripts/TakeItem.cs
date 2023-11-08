using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeItem : MonoBehaviour {

    public Transform objectParent;
    public GameObject itemObject;


    public Image iconObject;


    public Text textObject;

    private void SendItem()
    {
        GameObject go = Instantiate(itemObject, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        go.transform.parent = objectParent;

    }

    public void SetItem(Sprite icon,string Text)
    {
        iconObject.sprite = icon;
        textObject.text = Text;
        SendItem();
    }

    void Update ()
    {
    }
}
