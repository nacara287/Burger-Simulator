using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Gadd420;
using Mirror;
using StarterAssets;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerVehicleManager : MonoBehaviour
{
    
    [ShowInInspector]
    public GameObject CurrentVehicle;
    public bool OnVehicle;
    bool canuse;
    // Start is called before the first frame update
    void Start()
    {
        canuse = true;



    }
   
    // Update is called once per frame
    void Update()
    {
        if ( GetComponent<StarterAssetsInputs>().use && OnVehicle && canuse)
        {
            In();

        }


    }
    IEnumerator cooldown()
    {

        yield return new WaitForSeconds(2);
        canuse = true;

    }
    void Bin()
    {
        if (CurrentVehicle != null)
        {
            canuse = false;
          
            transform.SetParent(CurrentVehicle.transform);
          
        
            OnVehicle = true;
         
            CurrentVehicle.GetComponent<MotorBikeManager>().CurrentController = gameObject;
           // GetComponent<ThirdPersonController>().enabled = false;
            GetComponent<CharacterController>().enabled = false;
            GetComponent<Animator>().enabled = false;
            StartCoroutine(cooldown());
        }



    }
    void In()
    {
        if (CurrentVehicle != null  )
        {
            canuse = false;

             OnVehicle = false;
        
            CurrentVehicle.GetComponent<MotorBikeManager>().CurrentController = null;
            // GetComponent<ThirdPersonController>().enabled = false;
            GetComponent<CharacterController>().enabled = true;
            GetComponent<Animator>().enabled = true;
            transform.SetParent(null);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            StartCoroutine(cooldown());
        }



    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Motor"&&GetComponent<StarterAssetsInputs>().use&&!OnVehicle && canuse )
        {
            CurrentVehicle = other.gameObject;
            Bin();

          
        }
        
    }
}
