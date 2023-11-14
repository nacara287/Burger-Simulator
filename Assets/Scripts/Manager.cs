using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance;
    public bool LockControls;
    public InventoryItem itemPrefab;
    public ItemDatabase database;

    private void Awake()
    {if (instance != null)
            Destroy(this);
        instance = this; 
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (LockControls)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
                Cursor.lockState = CursorLockMode.Locked;
        }
        
    }
    public void setLockControls(bool col)
    {
        LockControls = col;


    }
}
