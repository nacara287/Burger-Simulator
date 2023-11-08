using Gadd420;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorBikeManager : MonoBehaviour
{
    public GameObject CurrentController;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void CheckController()
    {
        if (CurrentController == null)
        {GetComponent<RB_Controller>().enabled = false;


        }
        else
        {
            GetComponent<RB_Controller>().enabled = true;
        }

    }


    // Update is called once per frame
    void Update()
    {
        CheckController();
    }
}
