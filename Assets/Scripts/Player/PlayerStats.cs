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
      

     
    }

 
}
